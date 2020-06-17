using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string LatinName { get; set; }
        public string RussianName { get; set; }

        public void Create()
        {
            var insertCmd = $"insert into tags(latin_name, russian_name) values('{LatinName}', '{RussianName}') returning id";
            var cmd = new NpgsqlCommand(insertCmd, DBConnect.Connection);
            var reded = cmd.ExecuteScalar();
            cmd.Dispose();
            Id = (int)reded;
        }
        public void Update()
        {
            var sqlCmd = $"update tags set latin_name='{LatinName}', russian_name='{RussianName}' where id='{Id}';";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public void Delete()
        {
            Models.TagArticle.DeleteConnectedWithTag(Id);
            var sqlCmd = $"delete from tags where id='{Id}';";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public static List<Tag> GetAll()
        {
            List<Tag> tags = new List<Tag>();
            var sqlCmd = "select id, latin_name, russian_name from tags;";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var article = new Models.Tag();
                article.Id = reader.GetInt32(0);
                article.LatinName = reader.GetString(1);
                article.RussianName = reader.GetString(2);
                tags.Add(article);
            }
            reader.Close();
            cmd.Dispose();
            return tags;
        }
        public static Tag GetById(int id)
        {
            var sqlCmd = $"select latin_name, russian_name from tags where id = {id};";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            var reader = cmd.ExecuteReader();
            var tag = new Models.Tag();
            while (reader.Read())
            {
                tag.Id = id;
                tag.LatinName = reader.GetString(0);
                tag.RussianName = reader.GetString(1);
            }
            reader.Close();
            cmd.Dispose();
            if(tag.LatinName == null)
            {
                tag = null;
            }
            return tag;
        }
    }
}
