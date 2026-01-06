using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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


        public async Task<List<SimpeleSimulatieInfoDTO>> GeefSimpeleInfoVanSimulatiesAsync()
        {
            List<SimpeleSimulatieInfoDTO> ret = new();
            const string sqlStatementZoekBedrijf = "select s.id as id, b.naam as opdrachtgever, ld.naam as dataset, l.naam as land, count(k.id) as hoeveel_klanten, s.simulatie_tijd as simTime from simulaties s join bedrijven b on s.bedrijf_id=b.id join land_datasets ld on s.dataset_id=ld.id join landen l on ld.land_id = l.id left join simklanten k on k.simulatie_id=s.id group by  s.id,s.simulatie_tijd,b.naam, ld.naam, l.naam, ld.moment_aanmaak;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatementZoekBedrijf;
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SimpeleSimulatieInfoDTO dto = new SimpeleSimulatieInfoDTO((int)reader["id"], (string)reader["opdrachtgever"], (int)reader["hoeveel_klanten"], (string)reader["dataset"], (string)reader["land"], (DateTime)reader["simTime"]);
                            ret.Add(dto);
                        }
                        reader.Close();
                    }
                }
                }
            return ret;
        }


        public async Task<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulatie(int simulatie_id)
        {
            SimpeleSimulatieInfoDTO ret;
            const string sqlStatementZoekBedrijf = "select s.id as id, b.naam as opdrachtgever, ld.naam as dataset, l.naam as land, count(k.id) as hoeveel_klanten, s.simulatie_tijd as simTime from simulaties s join bedrijven b on s.bedrijf_id=b.id join land_datasets ld on s.dataset_id=ld.id join landen l on ld.land_id = l.id left join simklanten k on k.simulatie_id=s.id where s.id=@simulatie_id group by  s.id,s.simulatie_tijd,b.naam, ld.naam, l.naam, ld.moment_aanmaak;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatementZoekBedrijf;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();

                        ret = new SimpeleSimulatieInfoDTO((int)reader["id"], (string)reader["opdrachtgever"], (int)reader["hoeveel_klanten"], (string)reader["dataset"], (string)reader["land"], (DateTime)reader["simTime"]);
                         
                        
                        reader.Close();
                    }
                }
            }
            return ret;
        }
        public async Task<int> SaveKlantenSimulatie(Simulatie sim)
        {
            const string sqlStatementZoekBedrijf = "select id from bedrijven where naam like @naam";
            const string sqlStatementSimulatie = "Insert into simulaties(dataset_id,bedrijf_id,simulatie_tijd,seed,kans_op_letter_in_huisnr,maximum_HuisNr,Aantal_Mogelijke_Letters_In_HuisNr,minimumleeftijd,maximumleeftijd) output INSERTED.ID values (@DatasetID,@BedrijfID,@simTime,@seed,@kansOpHuisNrLetter,@MaxHuisNr,@AantalHuisNrLetters,@minLeefrijd,@MaxLeeftijd)";
            const string sqlStatementSimulatieStadLimitatie = "Insert into simulatie_stad_limitaties(simulatie_id,stad_id) values (@simulatieID,@stadID)";


            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {

                    int BedrijfId;
                    using (SqlCommand cmdZoekBedrijf = con.CreateCommand())
                    {
                        cmdZoekBedrijf.CommandText = sqlStatementZoekBedrijf;
                        cmdZoekBedrijf.Transaction = transaction;
                        cmdZoekBedrijf.Parameters.AddWithValue("@naam", sim.OpdrachtGever);
                        using (DbDataReader reader = await cmdZoekBedrijf.ExecuteReaderAsync())
                        {
                            reader.ReadAsync();

                            BedrijfId = (int)reader["id"];
                        }
                  


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
                            await cmdMaakSimStadLimitatie.ExecuteNonQueryAsync();
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
                        await sqlKlantenCopy.WriteToServerAsync(klantenTabel);


                    }





                   await transaction.CommitAsync();
                    return simulatieId;
                }
                catch (Exception ex)
                {

                    await transaction.RollbackAsync();
                    throw ex;
                }
                
            }
        }


        public async Task<DetailedSimulatieDTO> GeefDetailedInfoVanSimulatieAsync(int simulatie_id)
        {
            DetailedSimulatieDTO ret;
            const string sqlStatementStadLimitaties = "select s.naam from steden s join simulatie_stad_limitaties ssl on s.id=ssl.stad_id where ssl.simulatie_id=@simulatie_id";
            const string sqlStatementRestData = "select s.id as id,\r\n            b.naam as opdrachtgever,\r\n            ld.id as LandDatasetid,\r\n            ld.naam as dataset,\r\n            l.naam as land,\r\n            count(k.id) as hoeveel_klanten,\r\n            ld.moment_aanmaak as simTime,\r\n            s.seed as seed,\r\n            s.minimumleeftijd as MinLeeftijd,\r\n            s.maximumleeftijd as MaxLeeftijd,\r\n            s.Aantal_Mogelijke_Letters_In_HuisNr as AantalMogelijkeLettersInHuisNr,\r\n            s.kans_op_letter_in_huisnr as KansOpLettersInHuisNr,\r\n            AVG(DATEDIFF(DAY, k.geboortedatum, s.simulatie_tijd)/ 365.2425)   as avgLeeftijdAanMaak,\r\n            AVG(DATEDIFF(DAY, k.geboortedatum, GETDATE())/ 365.2425)  as avgLeeftijdNu\r\n            from simulaties s \r\n                join bedrijven b on s.bedrijf_id=b.id \r\n                join land_datasets ld on s.dataset_id=ld.id \r\n                join landen l on ld.land_id = l.id \r\n                left join simklanten k on k.simulatie_id=s.id \r\n                where s.id = @simulatie_id\r\n                group by  s.id,b.naam, ld.naam, l.naam, ld.moment_aanmaak,s.seed,\r\n            s.minimumleeftijd,\r\n            s.maximumleeftijd,\r\n            s.Aantal_Mogelijke_Letters_In_HuisNr,\r\n            s.kans_op_letter_in_huisnr\r\n            ,ld.id";
            /*select s.id as id,
            b.naam as opdrachtgever,
            ld.id as LandDatasetid,
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
            s.kans_op_letter_in_huisnr
            ,ld.id
            */
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                List<string> LimitatieSteden=new();
                using (SqlCommand cmdstadLimiet = con.CreateCommand())
                {
                    cmdstadLimiet.CommandText = sqlStatementStadLimitaties;
                    cmdstadLimiet.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmdstadLimiet.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            LimitatieSteden.Add(Convert.ToString(reader["naam"]));
                        }
                       
                    }
                    }

                    using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatementRestData;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();

                        ret = new DetailedSimulatieDTO(
                        Convert.ToInt32(reader["id"]),
                          Convert.ToInt32(reader["LandDatasetid"]),
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
            }
            return ret;
        }



        public async Task<List<StadMetAantalKlantenDTO>> GeefStadKlantenDistributieVanSimulatieAsync(int simulatie_id)
        {
            List<StadMetAantalKlantenDTO> ret = new();
            const string sqlStatement = "select ste.naam, COUNT(k.id) as klanten from straten stra join simklanten k on stra.id = k.straat_id join steden ste on stra.stad_id = ste.id where k.simulatie_id=@simulatie_id group by ste.naam;";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            StadMetAantalKlantenDTO dto = new(Convert.ToString(reader["naam"]), Convert.ToInt32(reader["klanten"]));
                            ret.Add(dto);
                        }
                        reader.Close();
                    }
                }
            }
            return ret;
        }

        public async Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieVoornamenVanSimulatie(int simulatie_id,Geslacht geslacht)
        {
            List<NaamMetAantalKlantedDTO> ret = new();
            const string sqlStatement = "select k.voornaam as naam,\r\ncount(k.id) as klanten\r\nfrom simklanten k \r\nwhere k.simulatie_id=@simulatie_id and k.geslacht=@geslachtchar\r\ngroup by k.voornaam;";
            /*
             select k.voornaam as naam,
count(k.id) as klanten
from simklanten k 
where k.simulatie_id=@simulatie_id and k.geslacht=@geslachtchar
group by k.voornaam;
             */

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    char geslachtChar = geslacht.ToString()[0];
                    cmd.Parameters.AddWithValue("@geslachtchar", geslachtChar);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                          //  NaamSoort naamsoort = (NaamSoort)Enum.Parse(typeof(NaamSoort), (string)reader["naam_type"]);
                            NaamMetAantalKlantedDTO dto = new(Convert.ToString(reader["naam"]), Convert.ToInt32(reader["klanten"]));
                            ret.Add(dto);
                        }
                       
                    }
                }
            }
            return ret;
        }

        public async Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieAchternamenVanSimulatie(int simulatie_id)
        {
            List<NaamMetAantalKlantedDTO> ret = new();
            const string sqlStatement = "select k.achternaam as naam,\r\ncount(k.id) as klanten\r\nfrom simklanten k \r\nwhere k.simulatie_id=@simulatie_id\r\ngroup by k.achternaam;";
                /*
             select k.achternaam as naam,
count(k.id) as klanten
from simklanten k 
where k.simulatie_id=@simulatie_id
group by k.achternaam;
             */

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {

                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                   
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            //  NaamSoort naamsoort = (NaamSoort)Enum.Parse(typeof(NaamSoort), (string)reader["naam_type"]);
                            NaamMetAantalKlantedDTO dto = new(Convert.ToString(reader["naam"]), Convert.ToInt32(reader["klanten"]));
                            ret.Add(dto);
                        }

                    }
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

        public async Task<KlantDTO> InfoJongsteKlant(int simulatie_id)
        {
            KlantDTO ret;

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
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        Geslacht geslacht = CharTogeslacht(Convert.ToChar(reader["geslacht"]));
                        ret = new KlantDTO(Convert.ToString(reader["Voornaam"]), Convert.ToString(reader["Achternaam"]), geslacht,
                            Convert.ToString(reader["HuisNr"]), Convert.ToString(reader["Straat"]), Convert.ToString(reader["Stad"]), Convert.ToString(reader["Land"]), Convert.ToDateTime(reader["geboorteDatum"]));

                        reader.Close();

                    }
                }
            }

            return ret;
        }

        public async Task<KlantDTO> InfoOudsteKlant(int simulatie_id)
        {
            KlantDTO ret;

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
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        Geslacht geslacht = CharTogeslacht(Convert.ToChar(reader["geslacht"]));
                        ret = new KlantDTO(Convert.ToString(reader["Voornaam"]), Convert.ToString(reader["Achternaam"]), geslacht,
                            Convert.ToString(reader["HuisNr"]), Convert.ToString(reader["Straat"]), Convert.ToString(reader["Stad"]), Convert.ToString(reader["Land"]), Convert.ToDateTime(reader["geboorteDatum"]));

                        reader.Close();

                    }
                }
            }

            return ret;
        }


        public async Task<List<Klant>> GeefKlanten(int simulatie_id)
        {
            List<Klant> ret = new();
            Land land;
            const string sqlLandCmd = "select l.id,l.naam from landen l\r\njoin land_datasets d on d.land_id = l.id\r\njoin simulaties s on s.dataset_id=d.id\r\nwhere s.id = @simulatie_id;";
        /*
select l.id,l.naam from landen l
join land_datasets d on d.land_id = l.id
join simulaties s on s.dataset_id=d.id
where s.id = @simulatie_id;
        */
           const string sqlKlandCmd = " select k.id as id\r\n,k.voornaam as voornaam\r\n,k.achternaam as achternaam\r\n,k.geboortedatum as geboortedatum\r\n,k.geslacht as geslacht\r\n,k.HuisNr as huisNr, \r\nsra.id as straatId,\r\nsra.naam as straatnaam,\r\nste.id as stadId,\r\nste.naam as stadnaam\r\nfrom simklanten k\r\njoin straten sra on sra.id=straat_id \r\njoin steden ste on sra.stad_id=ste.id\r\nwhere k.simulatie_id = @simulatie_id;";
                /*
             select k.id as id
,k.voornaam as voornaam
,k.achternaam as achternaam
,k.geboortedatum as geboortedatum
,k.geslacht as geslacht
,k.HuisNr as huisNr, 
sra.id as straatId,
sra.naam as straatnaam,
ste.id as stadId,
ste.naam as stadnaam
from simklanten k
join straten sra on sra.id=straat_id 
join steden ste on sra.stad_id=ste.id
where k.simulatie_id = @simulatie_id;
             */

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlLandCmd;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();

                        land = new(Convert.ToInt32(reader["id"]), Convert.ToString(reader["naam"]));
                    }
                }

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlKlandCmd;
                    cmd.Parameters.AddWithValue("@simulatie_id", simulatie_id);
                    Dictionary<int, Stad> steden = new();
                    Dictionary<int, Straat> straten = new();
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string voornaam = Convert.ToString(reader["voornaam"]);
                            string achternaam = Convert.ToString(reader["achternaam"]);
                            DateTime geboortedatum = Convert.ToDateTime(reader["geboortedatum"]);
                            string huisNr = Convert.ToString(reader["huisNr"]);
                            int straatId = Convert.ToInt32(reader["straatId"]);
                            Geslacht geslacht = CharTogeslacht(Convert.ToChar(reader["geslacht"]));
                            Straat straat;
                            Klant klant;
                            if (!straten.TryGetValue(straatId,out straat))
                            {
                                string straatnaam = Convert.ToString(reader["straatnaam"]);
                                int stadId = Convert.ToInt32(reader["stadId"]);
                                Stad stad;
                                if (!steden.TryGetValue(stadId, out stad))
                                {
                                    string stadnaam = Convert.ToString(reader["stadnaam"]);
                                    stad = new(stadId, land, stadnaam);
                                    steden.Add(stadId,stad);
                                }

                                straat = new(straatId, stad, straatnaam);
                                straten.Add(straatId, straat);

                            }
                            Adress adress = new Adress(huisNr, straat);
                            klant = new(id, voornaam, achternaam, adress, geslacht, geboortedatum);




                            ret.Add( klant);
                        }

                        
                    }
                }
                return ret;
            }
        }




    }
}
