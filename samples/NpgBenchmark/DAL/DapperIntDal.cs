using Dapper;
using NpgBenchmark.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark.DAL
{
    public class DapperIntDal
    {
        static public string connectionString = ConnectionProvider.ConnectionString;
        static public Guid InsertGuid(MockGuidDB data)
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            string sql = $"insert into mock_guid (id, first_name, last_name, email, gender, ip_address) values (@id, @first_name, @last_name, @email, @gender, @ip_address) returning id;";
            return connection.ExecuteScalar<Guid>(sql, data);
        }

        static public int InsertInt(MockIntDB data)
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            string sql = $"insert into mock_serial (first_name, last_name, email, gender, ip_address) values (@first_name, @last_name, @email, @gender, @ip_address) returning id;";
            return connection.ExecuteScalar<int>(sql, data);
        }

        static public IEnumerable<MockGuidDB> SearchName(string name)
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            string sql = $"select* from mock_guid where 1 = 1  and first_name ~* '{name}' LIMIT 100;";
            return connection.Query<MockGuidDB>(sql);
        }
    }
}
