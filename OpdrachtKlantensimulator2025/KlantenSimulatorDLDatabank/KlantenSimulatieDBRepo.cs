using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDLDatabank
{
   
    public class KlantenSimulatieDBRepo : IKlantenSimulatieRepo
    {
        private const int SQLBULKBATCHSIZE = 1000;
        private string _connectionString;
        public KlantenSimulatieDBRepo(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulaties()
        {
            List<SimpeleSimulatieInfoDTO> ret = new();
            const string sqlStatementZoekBedrijf = "select s.id as id, b.naam as opdrachtgever, ld.naam as dataset, l.naam as land, count(k.id) as hoeveel_klanten, ld.moment_aanmaak as simTime from simulaties s join bedrijven b on s.bedrijf_id=b.id join land_datasets ld on s.dataset_id=ld.id join landen l on ld.land_id = l.id left join simklanten k on k.simulatie_id=s.id group by  s.id,b.naam, ld.naam, l.naam, ld.moment_aanmaak;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText= sqlStatementZoekBedrijf;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        SimpeleSimulatieInfoDTO dto = new SimpeleSimulatieInfoDTO((int)reader["id"],(string)reader["opdrachtgever"], (int)reader["hoeveel_klanten"], (string)reader["dataset"], (string)reader["land"], (DateTime)reader["simTime"]);
                        ret.Add(dto);
                    }
                    reader.Close();
                }
                }
            return ret;
        }

        public void SaveKlantenSimulatie(Simulatie sim)
        {
            const string sqlStatementZoekBedrijf = "select id from bedrijven where naam like @naam";
            const string sqlStatementSimulatie = "Insert into simulaties(dataset_id,bedrijf_id,simulatie_tijd,seed,kans_op_letter_in_huisnr,maximum_HuisNr,Aantal_Mogelijke_Letters_In_HuisNr,minimumleeftijd,maximumleeftijd) output INSERTED.ID values (@DatasetID,@BedrijfID,@simTime,@seed,@kansOpHuisNrLetter,@MaxHuisNr,@AantalHuisNrLetters,@minLeefrijd,@MaxLeeftijd)";
            const string sqlStatementSimulatieStadLimitatie = "Insert into simulatie_stad_limitaties(simulatie_id,stad_id) values (@simulatieID,@stadID)";


            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {

                    int BedrijfId;
                    using (SqlCommand cmdZoekBedrijf = con.CreateCommand())
                    {
                        cmdZoekBedrijf.CommandText = sqlStatementZoekBedrijf;
                        cmdZoekBedrijf.Transaction = transaction;
                        cmdZoekBedrijf.Parameters.AddWithValue("@naam", sim.OpdrachtGever);
                        IDataReader reader = cmdZoekBedrijf.ExecuteReader();

                        reader.Read();

                        BedrijfId = (int)reader["id"];
                        reader.Close();


                    }
                    int simulatieId;
                    using (SqlCommand cmdMaakSim = con.CreateCommand())
                    {
                        cmdMaakSim.CommandText = sqlStatementSimulatie;
                        cmdMaakSim.Transaction = transaction;
                        //(@DatasetID,@BedrijfID,@simTime,@seed,@kansOpHuisNrLetter,@MaxHuisNr,@AantalHuisNrLetters,@minLeefrijd,@MaxLeeftijd)";
                        cmdMaakSim.Parameters.AddWithValue("@DatasetID", sim.DatasetID);
                        cmdMaakSim.Parameters.AddWithValue("@BedrijfID", BedrijfId);
                        cmdMaakSim.Parameters.AddWithValue("@simTime", sim.TijdVanGeneratie);
                        cmdMaakSim.Parameters.AddWithValue("@seed", sim.Seed);
                        cmdMaakSim.Parameters.AddWithValue("@kansOpHuisNrLetter", sim.PercentageAdressenMetNummers);
                        cmdMaakSim.Parameters.AddWithValue("@MaxHuisNr", sim.MaxHuisnummer);
                        cmdMaakSim.Parameters.AddWithValue("@AantalHuisNrLetters", sim.AantalMogelijkeLettersInHuisNr);
                        cmdMaakSim.Parameters.AddWithValue("@minLeefrijd", sim.MinimumLeeftijd);
                        cmdMaakSim.Parameters.AddWithValue("@MaxLeeftijd", sim.MaximumLeeftijd);
                        simulatieId = (int)cmdMaakSim.ExecuteScalar();

                    }

                    using (SqlCommand cmdMaakSimStadLimitatie = con.CreateCommand())
                    {
                        cmdMaakSimStadLimitatie.CommandText = sqlStatementSimulatieStadLimitatie;
                        cmdMaakSimStadLimitatie.Transaction = transaction;
                        foreach (Stad stad in sim.LimitatieTotSteden)
                        {
                            cmdMaakSimStadLimitatie.Parameters.AddWithValue("@simulatieID", simulatieId);
                            cmdMaakSimStadLimitatie.Parameters.AddWithValue("@stadID", stad.Id);
                            cmdMaakSimStadLimitatie.ExecuteNonQuery();
                            cmdMaakSimStadLimitatie.Parameters.Clear();
                        }
                    }

                      using(SqlBulkCopy sqlKlantenCopy = new(con,SqlBulkCopyOptions.Default,transaction))
                      {
                       // sqlKlantenCopy.BatchSize = SQLBULKBATCHSIZE;
                          DataTable klantenTabel = new DataTable("simklanten");
                        klantenTabel.Columns.Add("simulatie_id", typeof(int));
                        klantenTabel.Columns.Add("geboortedatum", typeof(DateTime));
                        klantenTabel.Columns.Add("geslacht", typeof(char));
                        klantenTabel.Columns.Add("voornaam", typeof(string));
                        klantenTabel.Columns.Add("achternaam", typeof(string));
                        klantenTabel.Columns.Add("HuisNr", typeof(string));
                        klantenTabel.Columns.Add("straat_id", typeof(int));
                          foreach(Klant k in sim.Klanten)
                        {
                            char geslacht = k.Geslacht.ToString()[0];
                            klantenTabel.Rows.Add(simulatieId,k.GeboorteDatum, geslacht,k.VoorNaam,k.Familienaam,k.Adress.HuisNr,k.Adress.Straat.Id);
                        }
                        sqlKlantenCopy.ColumnMappings.Add("simulatie_id", "simulatie_id");
                        sqlKlantenCopy.ColumnMappings.Add("geboortedatum", "geboortedatum");
                        sqlKlantenCopy.ColumnMappings.Add("geslacht", "geslacht");
                        sqlKlantenCopy.ColumnMappings.Add("voornaam", "voornaam");
                        sqlKlantenCopy.ColumnMappings.Add("achternaam", "achternaam");
                        sqlKlantenCopy.ColumnMappings.Add("HuisNr", "HuisNr");
                        sqlKlantenCopy.ColumnMappings.Add("straat_id", "straat_id");
                        sqlKlantenCopy.DestinationTableName = "simklanten";
                        sqlKlantenCopy.WriteToServer(klantenTabel);


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


        public DetailedSimulatieDTO GeefDetailedInfoVanSimulatie(int simulatie_id)
        {
            DetailedSimulatieDTO ret;
            const string sqlStatementStadLimitaties = "select s.naam from steden s join simulatie_stad_limitaties ssl on s.id=ssl.stad_id where ssl.simulatie_id=@simulatie_id";
            const string sqlStatementRestData = "select s.id as id,\r\nb.naam as opdrachtgever,\r\nld.naam as dataset,\r\nl.naam as land,\r\ncount(k.id) as hoeveel_klanten,\r\nld.moment_aanmaak as simTime,\r\ns.seed as seed,\r\ns.minimumleeftijd as MinLeeftijd,\r\ns.maximumleeftijd as MaxLeeftijd,\r\ns.Aantal_Mogelijke_Letters_In_HuisNr as AantalMogelijkeLettersInHuisNr,\r\ns.kans_op_letter_in_huisnr as KansOpLettersInHuisNr,\r\nAVG(DATEDIFF(DAY, k.geboortedatum, s.simulatie_tijd)/ 365.2425)   as avgLeeftijdAanMaak,\r\nAVG(DATEDIFF(DAY, k.geboortedatum, GETDATE())/ 365.2425)  as avgLeeftijdNu\r\nfrom simulaties s \r\n\tjoin bedrijven b on s.bedrijf_id=b.id \r\n\tjoin land_datasets ld on s.dataset_id=ld.id \r\n\tjoin landen l on ld.land_id = l.id \r\n\tleft join simklanten k on k.simulatie_id=s.id \r\n    where s.id = @simulatie_id\r\n\tgroup by  s.id,b.naam, ld.naam, l.naam, ld.moment_aanmaak,s.seed,\r\ns.minimumleeftijd,\r\ns.maximumleeftijd,\r\ns.Aantal_Mogelijke_Letters_In_HuisNr,\r\ns.kans_op_letter_in_huisnr;";
/*select s.id as id,
b.naam as opdrachtgever,
ld.naam as dataset,
l.naam as land,
count(k.id) as hoeveel_klanten,
ld.moment_aanmaak as simTime,
s.seed as seed,
s.minimumleeftijd as MinLeeftijd,
s.maximumleeftijd as MaxLeeftijd,
s.Aantal_Mogelijke_Letters_In_HuisNr as AantalMogelijkeLettersInHuisNr,
s.kans_op_letter_in_huisnr as KansOpLettersInHuisNr,
AVG(DATEDIFF(DAY, k.geboortedatum, s.simulatie_tijd)/ 365.2425)   as avgLeeftijdAanMaak,
AVG(DATEDIFF(DAY, k.geboortedatum, GETDATE())/ 365.2425)  as avgLeeftijdNu
from simulaties s 
	join bedrijven b on s.bedrijf_id=b.id 
	join land_datasets ld on s.dataset_id=ld.id 
	join landen l on ld.land_id = l.id 
	left join simklanten k on k.simulatie_id=s.id 
    where s.id = @simulatie_id
	group by  s.id,b.naam, ld.naam, l.naam, ld.moment_aanmaak,s.seed,
s.minimumleeftijd,
s.maximumleeftijd,
s.Aantal_Mogelijke_Letters_In_HuisNr,
s.kans_op_letter_in_huisnr;
*/
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<string> LimitatieSteden=new();
                using (SqlCommand cmdstadLimiet = con.CreateCommand())
                {
                    cmdstadLimiet.CommandText = sqlStatementStadLimitaties;
                    cmdstadLimiet.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    IDataReader reader = cmdstadLimiet.ExecuteReader();
                    while (reader.Read())
                    {
                        LimitatieSteden.Add(Convert.ToString(reader["naam"]));
                    }
                    reader.Close();
                }

                    using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatementRestData;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    IDataReader reader = cmd.ExecuteReader(); 
                    reader.Read();

                    ret = new DetailedSimulatieDTO(
                    Convert.ToInt32(reader["id"]),
                    Convert.ToString(reader["opdrachtgever"]),
                    Convert.ToInt32(reader["hoeveel_klanten"]),
                    Convert.ToString(reader["dataset"]),
                    Convert.ToString(reader["land"]),
                    Convert.ToDateTime(reader["simTime"]),
                    Convert.ToInt32(reader["seed"]),
                    Convert.ToInt32(reader["MinLeeftijd"]),
                    Convert.ToInt32(reader["MaxLeeftijd"]),
                    Convert.ToInt32(reader["AantalMogelijkeLettersInHuisNr"]),
                    Convert.ToSingle(reader["KansOpLettersInHuisNr"]),
                    Convert.ToSingle(reader["avgLeeftijdAanMaak"]),
                    Convert.ToSingle(reader["avgLeeftijdNu"]),
                    LimitatieSteden
                    ); 


                    reader.Close();
                }
            }
            return ret;
        }



        public List<StadMetAantalKlantenDTO> GeefStadKlantenDistributieVanSimulatie(int simulatie_id)
        {
            List<StadMetAantalKlantenDTO> ret = new();
            const string sqlStatement = "select ste.naam, COUNT(k.id) as klanten from straten stra join simklanten k on stra.id = k.straat_id join steden ste on stra.stad_id = ste.id where k.simulatie_id=@simulatie_id group by ste.naam;";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StadMetAantalKlantenDTO dto = new(Convert.ToString(reader["naam"]), Convert.ToInt32(reader["klanten"]));
                        ret.Add(dto);
                    }
                    reader.Close();
                }
            }
            return ret;
        }

        public List<NaamMetAantalKlantedDTO> GeefNaamKlantenDistributieVanSimulatie(int simulatie_id)
        {
            List<NaamMetAantalKlantedDTO> ret = new();
            const string sqlStatement = "select k.voornaam as naam, @voornaam as naam_type, count(k.id) as klanten from simklanten k where k.simulatie_id=@simulatie_id group by k.voornaam union select k2.achternaam as naam, @achternaam as naam_type, count(k2.id) as klanten from simklanten k2 where k2.simulatie_id=@simulatie_id group by k2.achternaam;";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    cmd.Parameters.AddWithValue("@voornaam", NaamSoort.Voornaam.ToString());
                    cmd.Parameters.AddWithValue("@achternaam", NaamSoort.AchterNaam.ToString());
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NaamSoort naamsoort = (NaamSoort)Enum.Parse(typeof(NaamSoort), (string)reader["naam_type"]);
                        NaamMetAantalKlantedDTO dto = new(Convert.ToString(reader["naam"]), naamsoort, Convert.ToInt32(reader["klanten"]));
                        ret.Add(dto);
                    }
                    reader.Close();
                }
            }
            return ret;
        }


        private Geslacht CharTogeslacht(char geslachtAsChar)
        {
            switch (geslachtAsChar)
            {
                case 'M': return Geslacht.Man;
                case 'V': return Geslacht.Vrouw;
                default: throw new Exception("KlantenSimulatieRepo.CharTogeslacht {geslachtAsChar} is invalid");
            }

        }

        public KlantenDTO InfoJongsteKlant(int simulatie_id)
        {
            KlantenDTO ret;

            const string sqlStatement = "select TOP 1 k.geslacht as geslacht, \r\nk.Voornaam as Voornaam,\r\nk.achternaam as Achternaam,\r\nstra.naam as Straat,\r\nste.naam as Stad,\r\nl.naam as Land,\r\nk.huisNr as HuisNr,\r\nk.geboorteDatum as geboorteDatum\r\nfrom simklanten k \r\njoin straten stra on k.straat_id=stra.id\r\njoin steden ste on stra.stad_id=ste.id\r\njoin land_datasets d on ste.dataset_id = d.id\r\njoin landen l on d.land_id=l.id\r\nwhere k.simulatie_id = @simulatie_id\r\norder by geboortedatum desc"; 
                /*
select TOP 1 k.geslacht as geslacht, 
k.Voornaam as Voornaam,
k.achternaam as Achternaam,
stra.naam as Straat,
ste.naam as Stad,
l.naam as Land,
k.huisNr as HuisNr,
k.geboorteDatum as geboorteDatum
from simklanten k 
join straten stra on k.straat_id=stra.id
join steden ste on stra.stad_id=ste.id
join land_datasets d on ste.dataset_id = d.id
join landen l on d.land_id=l.id
where k.simulatie_id = @simulatie_id
order by geboortedatum desc
            */

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Geslacht geslacht = CharTogeslacht(Convert.ToChar(reader["geslacht"]));
                    ret = new KlantenDTO(Convert.ToString(reader["Voornaam"]), Convert.ToString(reader["Achternaam"]),geslacht,
                        Convert.ToString(reader["HuisNr"]), Convert.ToString(reader["Straat"]), Convert.ToString(reader["Stad"]), Convert.ToString(reader["Land"]), Convert.ToDateTime(reader["geboorteDatum"]));
                       
                    reader.Close();
                }
            }

            return ret;
        }

        public KlantenDTO InfoOudsteKlant(int simulatie_id)
        {
            KlantenDTO ret;

            const string sqlStatement = "select TOP 1 k.geslacht as geslacht, \r\nk.Voornaam as Voornaam,\r\nk.achternaam as Achternaam,\r\nstra.naam as Straat,\r\nste.naam as Stad,\r\nl.naam as Land,\r\nk.huisNr as HuisNr,\r\nk.geboorteDatum as geboorteDatum\r\nfrom simklanten k \r\njoin straten stra on k.straat_id=stra.id\r\njoin steden ste on stra.stad_id=ste.id\r\njoin land_datasets d on ste.dataset_id = d.id\r\njoin landen l on d.land_id=l.id\r\nwhere k.simulatie_id = @simulatie_id\r\norder by geboortedatum asc";
            /*
select TOP 1 k.geslacht as geslacht, 
k.Voornaam as Voornaam,
k.achternaam as Achternaam,
stra.naam as Straat,
ste.naam as Stad,
l.naam as Land,
k.huisNr as HuisNr,
k.geboorteDatum as geboorteDatum
from simklanten k 
join straten stra on k.straat_id=stra.id
join steden ste on stra.stad_id=ste.id
join land_datasets d on ste.dataset_id = d.id
join landen l on d.land_id=l.id
where k.simulatie_id = @simulatie_id
order by geboortedatum asc
        */

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Geslacht geslacht = CharTogeslacht(Convert.ToChar(reader["geslacht"]));
                    ret = new KlantenDTO(Convert.ToString(reader["Voornaam"]), Convert.ToString(reader["Achternaam"]), geslacht,
                        Convert.ToString(reader["HuisNr"]), Convert.ToString(reader["Straat"]), Convert.ToString(reader["Stad"]), Convert.ToString(reader["Land"]), Convert.ToDateTime(reader["geboorteDatum"]));

                    reader.Close();
                }
            }

            return ret;
        }





    }
}
