using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Quizz_Survay_Application.Models
{
    public class DapperORM
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();

        public static void ExecuteWithoutReturn(string procedurename, DynamicParameters parameters = null)
        {
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                con.Execute(procedurename, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public static T ExecuteReturnScalar<T>(String procedurename, DynamicParameters parameters = null)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                var res = con.Query<T>(procedurename, parameters, commandType: System.Data.CommandType.StoredProcedure).Single();
                /*return (T)Convert.ChangeType(res, typeof(T));*/

                return res;
            }
        }

        public static IEnumerable<T> ReturnList<T>(string procedurename, DynamicParameters parameters = null)
        {
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                var res = con.Query<T>(procedurename, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public static T ReturnIdByOutput<T>(string procedurename, DynamicParameters parameters = null)
        {
            T res;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                con.Execute(procedurename, parameters, commandType: CommandType.StoredProcedure);
                res = parameters.Get<T>("@out");
            }

            return res;
        }
    }
}