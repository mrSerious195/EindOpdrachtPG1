using KlantenSimulatorBL.Domein;
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
        private string _connectionString;
        public KlantenSimulatieDBRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveKlantenSimulatie(Simulatie sim)
        {
            const string sqlStatementZoekBedrijf = "select id from bedrijven where naam like @naam";
            const string sqlStatementSimulatie = "Insert into simulaties(dataset_id,bedrijf_id,simulatie_tijd,seed,kans_op_letter_in_huisnr,maximum_HuisNr,Aantal_Mogelijke_Letters_In_HuisNr,minimumleeftijd,maximumleeftijd) output INSERTED.ID values (@DatasetID,@BedrijfID,@simTime,@seed,@kansOpHuisNrLetter,@MaxHuisNr,@AantalHuisNrLetters,@minLeefrijd,@MaxLeeftijd)";
            const string sqlStatementSimulatieStadLimitatie = "Insert into Landen(simulatie_id,stad_id) values (@simulatieID,@stadID)";


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
    }
}
