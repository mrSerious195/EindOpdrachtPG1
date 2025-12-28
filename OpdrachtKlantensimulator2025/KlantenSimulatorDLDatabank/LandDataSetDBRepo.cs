using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace KlantenSimulatorDLDatabank
{
    public class LandDataSetDBRepo : ILandDataSetRepo
    {
        private string _connectionString;
        public LandDataSetDBRepo(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public void SaveLandDataSet(LandDataSet landData)
        {
            const string sqlStatementHasLand = "select id from Landen where naam like (@voorwaarde)";
            const string sqlStatementLand = "Insert into Landen(naam) output INSERTED.ID values (@naam)";
            const string sqlStatementDataset = "Insert into land_datasets(land_id,naam,moment_aanmaak,Zijn_achternamen_neutraal) output INSERTED.ID values (@landId,@naam,@momentAanmaak,@achternamenNeutraal)";
            const string sqlStatementLookupCityID = "select id, naam from steden where dataset_id = @datasetid ";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    int? landID = null;
                    using (SqlCommand cmdHasLand = con.CreateCommand())
                    {
                        using (SqlCommand cmdLand = con.CreateCommand())
                        {
                            cmdLand.Transaction = transaction;
                            cmdHasLand.Transaction = transaction;

                            cmdHasLand.CommandText = sqlStatementHasLand;
                            cmdLand.CommandText = sqlStatementLand;

                            string landNaam = landData.Land.Naam;
                            cmdHasLand.Parameters.AddWithValue("@voorwaarde", landNaam);
                            IDataReader reader = cmdHasLand.ExecuteReader();
                        
                            
                            if (reader.Read())
                            {
                            landID = (int)reader["id"];
                           
                            reader.Close();
                        }
                        else
                        {
                            reader.Close();
                            cmdLand.Parameters.AddWithValue("@naam", landNaam);
                            landID = (int)cmdLand.ExecuteScalar();
                        }
                            landData.Land.Id=landID;
                        

                    }
                    }
                    int datasetID;
                    using (SqlCommand cmdDataset = con.CreateCommand())
                    {
                        cmdDataset.Transaction = transaction;
                    cmdDataset.CommandText = sqlStatementDataset;
                    cmdDataset.Parameters.AddWithValue("@landId", landID);
                    cmdDataset.Parameters.AddWithValue("@naam", landData.NaamDataSet);
                    cmdDataset.Parameters.AddWithValue("@achternamenNeutraal", landData.ZijnAchternamenVMHetzelfde);
                    cmdDataset.Parameters.AddWithValue("@momentAanmaak", DateTime.Now);
                    datasetID= (int)cmdDataset.ExecuteScalar();
                }

                using (SqlBulkCopy bulkNamen = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transaction))
                {

                    DataTable NaamTabel = new DataTable("namen");
                    NaamTabel.Columns.Add("dataset_id", typeof(int));
                    NaamTabel.Columns.Add("naam", typeof(string));
                    NaamTabel.Columns.Add("frequency", typeof(int));
                    NaamTabel.Columns.Add("naam_type", typeof(string));
                    NaamTabel.Columns.Add("gender", typeof(char));
                    List<List<NaamInfo>> naamlijsten;
                    if (landData.ZijnAchternamenVMHetzelfde)
                    {
                        naamlijsten = [landData.VoornamenM, landData.VoornamenV, landData.AchternamenM];
                    }
                    else
                    {
                        naamlijsten = [landData.VoornamenM, landData.VoornamenV, landData.AchternamenM,landData.AchternamenV];
                    }
                        foreach (List<NaamInfo> naamlijst in naamlijsten)
                        {
                        char genderasChar;
                        GramaticaalGeslacht geslacht = naamlijst.First().Geslacht;
                        switch (geslacht)
                        {
                            case GramaticaalGeslacht.Man: genderasChar = 'M'; break;
                            case GramaticaalGeslacht.Vrouw: genderasChar = 'V'; break;
                            case GramaticaalGeslacht.Onzijdig: genderasChar = 'O'; break;
                            default: throw new Exception($"DatasetDBRepo hasn't implemented {geslacht}");
                        }
                        foreach (NaamInfo n in naamlijst) {
                           
                                NaamTabel.Rows.Add(datasetID, n.Naam, n.Frequency, n.NaamSoort.ToString(), genderasChar);
                            }

                        }
                    bulkNamen.ColumnMappings.Add("dataset_id", "dataset_id");
                    bulkNamen.ColumnMappings.Add("naam", "naam");
                    bulkNamen.ColumnMappings.Add("frequency", "frequency");
                    bulkNamen.ColumnMappings.Add("naam_type", "naam_type");
                    bulkNamen.ColumnMappings.Add("gender", "gender");
                    bulkNamen.DestinationTableName = "namen";
                    bulkNamen.WriteToServer(NaamTabel);
                }


                using (SqlBulkCopy bulkSteden = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transaction))
                {

                    DataTable stadTabel = new DataTable("steden");
                    stadTabel.Columns.Add("dataset_id", typeof(int));
                    stadTabel.Columns.Add("naam", typeof(string));
                    HashSet<string> steden = new();
                    foreach (Straat s in landData.Straten)
                    {
                        string stadnaam = s.Stad.Naam;
                        if (!steden.Contains(stadnaam))
                        {
                            steden.Add(stadnaam);
                            stadTabel.Rows.Add(datasetID, stadnaam);
                        }

                    }
                    bulkSteden.ColumnMappings.Add("dataset_id", "dataset_id");
                    bulkSteden.ColumnMappings.Add("naam", "naam");
                    bulkSteden.DestinationTableName = "steden";
                    bulkSteden.WriteToServer(stadTabel);
                }

             
                Dictionary<string, int> StadToId = new();
                using(SqlCommand cmdLookupCityID = con.CreateCommand())
                {
                    cmdLookupCityID.CommandText = sqlStatementLookupCityID;
                    cmdLookupCityID.Transaction = transaction;
                    cmdLookupCityID.Parameters.AddWithValue("@datasetid", datasetID);
                    IDataReader reader = cmdLookupCityID.ExecuteReader();
                    while (reader.Read())
                    {
                        StadToId[(string)reader["naam"]] = (int)reader["id"];
                    }
                    reader.Close();
                }
               

                using (SqlBulkCopy bulkstraten = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transaction))
                {
                    DataTable straatTabel = new DataTable("straten");
                    straatTabel.Columns.Add("stad_id", typeof(int));
                    straatTabel.Columns.Add("naam", typeof(string));
             
                    foreach (Straat s in landData.Straten)
                    {
                        if (StadToId.TryGetValue(s.Stad.Naam, out int stadId))
                        {
                            straatTabel.Rows.Add(stadId, s.Naam);
                           
                        }
                        else
                        {
                            throw new Exception($"Stad {s.Stad.Naam} not in dictionary steden");
                        }
                       

                    }
                    bulkstraten.ColumnMappings.Add("stad_id", "stad_id");
                    bulkstraten.ColumnMappings.Add("naam", "naam");
                    bulkstraten.DestinationTableName = "straten";
                    bulkstraten.WriteToServer(straatTabel);

                }
                transaction.Commit();
                }
                catch (Exception ex)
                {
                transaction.Rollback();
                    throw ex;
            }
        }

        }


        public LandDataSet GetLandDataSet(int datasetId)
        {
            LandDataSet dataset;
            const string SqlStatementLand = "select id, naam from landen where id = @LandId";
            const string SqlStatementDataset = "select land_id,Zijn_achternamen_neutraal, naam from land_datasets where id = @datasetID";
            const string SqlStatementStad = "select id,naam from steden where dataset_id = @datasetID";
            // const string SqlStatementDataset = "Insert into dataset(land_id,naam,moment_aanmaak,Zijn_achternamen_neutraal) output INSERTED.ID values (@landId,@naam,@momentAanmaak,@achternamenNeutraal)";
            const string SqlStatementStraat = "select str.id as id,str.stad_id as stad_id,str.naam as naam from straten str join steden st on st.id=str.stad_id where st.dataset_id = @datasetID";
            const string SqlStatementNaam = "select id, naam,frequency,naam_type,gender from namen where dataset_id = @datasetID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                
                con.Open();
                int LandID;
                bool ZijnAchternamenNeutraal;
                string DatasetNaam;
                using (SqlCommand sqlcmdDataset = con.CreateCommand())
                {
                    sqlcmdDataset.CommandText = SqlStatementDataset;
                    sqlcmdDataset.Parameters.AddWithValue("@datasetID", datasetId);
                    IDataReader reader = sqlcmdDataset.ExecuteReader();
                    reader.Read();
                    LandID = (int)reader["land_id"];
                    ZijnAchternamenNeutraal = (bool)reader["Zijn_achternamen_neutraal"];
                    DatasetNaam = (string)reader["naam"];
                    reader.Close();

                }
                Land land;
                using (SqlCommand sqlcmdLand = con.CreateCommand())
                {
                    sqlcmdLand.CommandText = SqlStatementLand;;
                    sqlcmdLand.Parameters.AddWithValue("@LandId", LandID);
                    IDataReader reader = sqlcmdLand.ExecuteReader();
                    reader.Read();
                    land = new((int)reader["id"], (string)reader["naam"]);
                    reader.Close();
                }
               
                Dictionary<int, Stad> IdToSteden = new();
                using (SqlCommand sqlcmdstad = con.CreateCommand())
                {
                    sqlcmdstad.CommandText = SqlStatementStad;
                    sqlcmdstad.Parameters.AddWithValue("@datasetID", datasetId);
                    IDataReader reader = sqlcmdstad.ExecuteReader();
                    while (reader.Read())
                    {
                        Stad stad = new((int)reader["id"], land, (string)reader["naam"]);
                        IdToSteden[stad.Id.Value]= stad;
                    }
                    reader.Close();
                }
                List<Straat> straten=new();
                using (SqlCommand sqlcmdstraat = con.CreateCommand())
                {
                    sqlcmdstraat.CommandText = SqlStatementStraat;
                    sqlcmdstraat.Parameters.AddWithValue("@datasetID", datasetId);
                    IDataReader reader = sqlcmdstraat.ExecuteReader();
                    while (reader.Read())
                    {
                        Straat straat = new((int)reader["id"], IdToSteden[(int)reader["stad_id"]], (string)reader["naam"]);
                        straten.Add(straat);
                    }
                    reader.Close();
                }

                

                using (SqlCommand sqlcmdNaam = con.CreateCommand())
                {
                    sqlcmdNaam.CommandText = SqlStatementNaam;
                    sqlcmdNaam.Parameters.AddWithValue("@datasetID", datasetId);
                    IDataReader reader = sqlcmdNaam.ExecuteReader();
                    List<NaamInfo> voornamenM = new();
                    List<NaamInfo> voornamenV = new();
                    if (ZijnAchternamenNeutraal)
                    {
                        List<NaamInfo> AchterNamen = new();
                        while (reader.Read()) {
                            GramaticaalGeslacht geslacht = CharToGramaticaalgeslacht(((string)reader["gender"])[0]);
                           
                            NaamSoort naamsoort = (NaamSoort)Enum.Parse(typeof(NaamSoort),(string)reader["naam_type"]);
                            List<NaamInfo> naamlijst;

                            switch((geslacht, naamsoort))
                            {
                                case (GramaticaalGeslacht.Man, NaamSoort.Voornaam): naamlijst = voornamenM; break;
                                case (GramaticaalGeslacht.Vrouw, NaamSoort.Voornaam): naamlijst = voornamenV; break;
                                case (GramaticaalGeslacht.Onzijdig, NaamSoort.AchterNaam): naamlijst = AchterNamen; break;
                                default: throw new Exception($"DatasetDBRepo.GetLandDataSet: een naam kan niet een ( {geslacht.ToString()} ,{naamsoort.ToString()} ) zijn als achternamen neutraal zijn");
                            }
                            NaamInfo naam = new((int)reader["id"], (string)reader["naam"], (int)reader["frequency"], geslacht, naamsoort);
                            naamlijst.Add(naam);

                        }

                        dataset = new LandDataSet(datasetId, ZijnAchternamenNeutraal, DatasetNaam, land, voornamenM, voornamenV, AchterNamen, AchterNamen, straten);
                    }
                    else
                    {
                        List<NaamInfo> AchterNamenM = new();
                        List<NaamInfo> AchterNamenV = new();

                        while (reader.Read())
                        {
                            GramaticaalGeslacht geslacht = CharToGramaticaalgeslacht(((string)reader["gender"])[0]);

                            NaamSoort naamsoort = (NaamSoort)Enum.Parse(typeof(NaamSoort), (string)reader["naam_type"]);
                            List<NaamInfo> naamlijst;

                            switch ((geslacht, naamsoort))
                            {
                                case (GramaticaalGeslacht.Man, NaamSoort.Voornaam): naamlijst = voornamenM; break;
                                case (GramaticaalGeslacht.Vrouw, NaamSoort.Voornaam): naamlijst = voornamenV; break;
                                case (GramaticaalGeslacht.Man, NaamSoort.AchterNaam): naamlijst = AchterNamenM; break;
                                case (GramaticaalGeslacht.Vrouw, NaamSoort.AchterNaam): naamlijst = AchterNamenV; break;
                                default:
                                    throw new Exception($"DatasetDBRepo.GetLandDataSet: een naam kan niet een ( {geslacht.ToString()} ,{naamsoort.ToString()} ) zijn als achternamen niet neutraal zijn");
                            }
                            NaamInfo naam = new((int)reader["id"], (string)reader["naam"], (int)reader["frequency"], geslacht, naamsoort);
                            naamlijst.Add(naam);

                        }
                        dataset = new LandDataSet(datasetId, ZijnAchternamenNeutraal, DatasetNaam, land, voornamenM, voornamenV, AchterNamenM, AchterNamenV, straten);

                    }
                    reader.Close();
                }


            }

            return dataset;
        }



        private GramaticaalGeslacht CharToGramaticaalgeslacht(char geslachtaschar)
        {
            switch (geslachtaschar)
            {
                case 'M': return GramaticaalGeslacht.Man;
                case 'V': return GramaticaalGeslacht.Vrouw;
                case 'O': return GramaticaalGeslacht.Onzijdig;
                default: throw new Exception($"DatasetDBRepo.Chartogramaticaalgeslacht \'{geslachtaschar}\' is niet geimplementeerd");
            }
        }



    }
}
