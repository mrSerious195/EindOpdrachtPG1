using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
        public async Task<List<string>> GeefAlleOpdrachtGevers()
        {
            const string sqlStatementZoekOpdrachtGevers = "select b.naam from bedrijven b ";

            List<string> ret = new();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatementZoekOpdrachtGevers;
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ret.Add((string)reader["naam"]);
                        }
                        reader.Close();
                    }
                    }
                }
            return ret;
        }
        public async Task VoegOpdrachtGeverToe(string naam)
        {
            const string sqlStatement = "insert into bedrijven (naam) values (@naam)";

            List<string> ret = new();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
               await con.OpenAsync();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    cmd.Parameters.AddWithValue("@naam", naam);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
           
        }
    }
}
