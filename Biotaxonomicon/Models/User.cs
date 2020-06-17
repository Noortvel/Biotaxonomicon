using Biotaxonomicon.Models.Exeptions;
using Biotaxonomicon.StringResources;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public void Create()
        {
            var logingCountSql = $"select count(login) from users where login = '{Login}'";
            var logingCountCmd = new NpgsqlCommand(logingCountSql, DBConnect.Connection);
            if ((long)logingCountCmd.ExecuteScalar() > 0)
            {
                logingCountCmd.Dispose();
                throw new InfoException(StringResources.StringResources.Instance.Login.AlredyExist);
            }
            logingCountCmd.Dispose();


            var nickCountSql = $"select count(nick) from users where nick = '{Nick}'";
            var nickCountCmd = new NpgsqlCommand(nickCountSql, DBConnect.Connection);
            if ((long)nickCountCmd.ExecuteScalar() > 0)
            {
                nickCountCmd.Dispose();
                throw new InfoException(StringResources.StringResources.Instance.Nick.AlredyExist);
            }
            nickCountCmd.Dispose();

            var insertSql = $"insert into users(nick, login, passwordhash) values('{Nick}', '{Login}', '{PasswordHash}') returning id";
            var insertCmd = new NpgsqlCommand(insertSql, DBConnect.Connection);
            var reded = insertCmd.ExecuteScalar();
            insertCmd.Dispose();
            Id = (int)reded;
        }
        /// <summary>
        /// Fill model
        /// </summary>
        /// <returns>-1 if failed</returns>
        public int SelectLoginPassword()
        {
            var sql = $"select id from users where login = '{Login}' and passwordhash='{PasswordHash}'";
            var cmd = new NpgsqlCommand(sql, DBConnect.Connection);
            var rdr = cmd.ExecuteReader();
            int idsCount = 0;
            int lastId = -1;
            while (rdr.Read())
            {
                lastId = rdr.GetInt32(0);
                idsCount++;
            }
            rdr.Close();
            cmd.Dispose();
            if (idsCount == 1)
            {
                return lastId;
            }
            return -1;
        }
        public void GetUserLoginNickById()
        {
            var sql = $"select login, nick from users where id='{Id}'";
            var cmd = new NpgsqlCommand(sql, DBConnect.Connection);
            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Login = rdr.GetString(0);
                Nick = rdr.GetString(1);
            }
            rdr.Close();
            cmd.Dispose();
        }
    }
}
