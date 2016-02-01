using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using QTP.Infra;

namespace QTP.DBAccess
{
    public class CRUD
    {
        public static string ConnectionString;

        public static TStrategy GetTStrategy(string id, NLog log)
        {
            TStrategy t = new TStrategy();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(string.Format("SELECT * FROM Strategy WHERE Id={0}", id), connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        t.Id = (int)reader["Id"];
                        t.Name = (string)reader["Name"];
                        t.GMID = (string)reader["GMID"];
                        t.RiskMClass = (string)reader["RiskMClass"];
                        t.PoolId = (int)reader["PoolId"];
                        t.Args = (string)reader["Args"];
                        t.MDMode = (int)reader["MDMode"];
                    }
                    command.Connection.Close();
                }

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Login", connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        t.UserName = (string)reader["UserName"];
                        t.Password = (string)reader["Password"];
                    }
                    command.Connection.Close();
                }

                t.Instruments = GetTInstruments(t.PoolId);
                t.Positions = GetTPositions(t.Id);
            }
            catch 
            {
                log.WriteError("Read TStrategies");
            }

            return t;
        }

        private static List<TInstrument> GetTInstruments(int poolId)
        {
            List<TInstrument> list = new List<TInstrument>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM PoolInstrument WHERE PoolId={0}", poolId), connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TInstrument ins = new TInstrument();
                    ins.Exchange = ((string)reader["Exchange"]).Trim();
                    ins.InstrumentId = ((string)reader["InstrumentId"]).Trim();
                    ins.MinPosition = (int)reader["MinPosition"];
                    ins.MonitorClass = (string)reader["MonitorClass"];
                    list.Add(ins);
                }
                command.Connection.Close();
            }

            return list;
        }
                
        private static List<TPosition> GetTPositions(int id)
        {
            List<TPosition> list = new List<TPosition>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM Position WHERE StrategyId={0}", id), connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TPosition pos = new TPosition();

                    pos.Exchange = ((string)reader["Exchange"]).Trim();
                    pos.InstrumentId = ((string)reader["InstrumentId"]).Trim();
                    pos.Volumn = (int)reader["Volumn"];
                    pos.StopLossStyle = (int)reader["StopLossStyle"];
                    pos.InPrice = (double)reader["InPrice"];

                    list.Add(pos);
                }
                command.Connection.Close();
            }

            return list;
        }
    }
}
