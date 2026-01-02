using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDLDatabank
{
    public class OpdrachtGeversDBRepo : IOpdrachtGeverRepo
    {

        private string _connectionString;
        public OpdrachtGeversDBRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<string> GeefAlleOpdrachtGevers()
        {
            const string sqlStatementZoekOpdrachtGevers = "select b.naam from bedrijven b ";

            List<string> ret = new();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatementZoekOpdrachtGevers;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add((string)reader["naam"]);
                    }
                    reader.Close();
                }
            }
            return ret;
        }
        public void VoegOpdrachtGeverToe(string naam)
        {
            const string sqlStatement = "insert into bedrijven (naam) values (@naam)";

            List<string> ret = new();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@naam", naam);
                    cmd.ExecuteNonQuery();
                }
            }
           
        }
    }
}
