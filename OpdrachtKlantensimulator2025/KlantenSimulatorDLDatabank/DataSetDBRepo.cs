using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace KlantenSimulatorDLDatabank
{
    public class DataSetDBRepo : ILandDataSetRepo
    {
        private string _connectionString;
        public DataSetDBRepo(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public void SaveLandDataSet(LandDataSet landData)
        {
            const string SqlStatementHasLand = "select id from Land where naam like (@voorwaarde)";
            const string SqlStatementLand = "Insert into Land(naam) output INSERTED.ID values (@naam)";
            const string SqlStatementDataset = "Insert into dataset(land_id,naam,moment_aanmaak,Zijn_achternamen_neutraal) output INSERTED.ID values (@landId,@naam,@momentAanmaak,@achternamenNeutraal)";
           

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

                            cmdHasLand.CommandText = SqlStatementHasLand;
                            cmdLand.CommandText = SqlStatementLand;

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
                    cmdDataset.CommandText = SqlStatementDataset;
                    cmdDataset.Parameters.AddWithValue("@landId", landID);
                    cmdDataset.Parameters.AddWithValue("@naam", landData.NaamDataSet);
                    cmdDataset.Parameters.AddWithValue("@achternamenNeutraal", landData.ZijnAchternamenVMHetzelfde);
                    cmdDataset.Parameters.AddWithValue("@momentAanmaak", DateTime.Now);
                    datasetID= (int)cmdDataset.ExecuteScalar();
                }

                using (SqlBulkCopy bulkNamen = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transaction))
                {

                    DataTable NaamTabel = new DataTable("naamdata");
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
                    bulkNamen.DestinationTableName = "naamdata";
                    bulkNamen.WriteToServer(NaamTabel);
                }


                using (SqlBulkCopy bulkSteden = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transaction))
                {

                    DataTable stadTabel = new DataTable("stad");
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
                    bulkSteden.DestinationTableName = "stad";
                    bulkSteden.WriteToServer(stadTabel);
                }

                const string sqlStatementLookupCityID = "select id, naam from stad where dataset_id = @datasetid ";
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
                    DataTable straatTabel = new DataTable("straat");
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
                    bulkstraten.DestinationTableName = "straat";
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
            const string SqlStatementLand = "select id, naam from land where id = @LandId";
            const string SqlStatementDataset = "select land_id,Zijn_achternamen_neutraal, naam from dataset where id = @datasetID";
            const string SqlStatementStad = "select id,naam from stad where dataset_id = @datasetID";
            // const string SqlStatementDataset = "Insert into dataset(land_id,naam,moment_aanmaak,Zijn_achternamen_neutraal) output INSERTED.ID values (@landId,@naam,@momentAanmaak,@achternamenNeutraal)";


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
                Console.WriteLine($"{land.Id}, {land.Naam}");
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
               }

            return null;
        }
    }
}
