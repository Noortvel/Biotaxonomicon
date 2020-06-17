using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon
{
    public static class DBConnect
    { 
        public static NpgsqlConnection Connection
        {
            get;
            private set;
        }
        public static void Open(string connectInfo)
        {
            Connection = new NpgsqlConnection(connectInfo);

            Connection.Open();
        }
        public static void Close()
        {
            Connection.Close();
        }
    }
}
