using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    [Serializable]
    public class Article
    {
        public int Id { get; set; }
        public string HeaderLatin { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastEditedTime { get; set; }
        public int UserId { get; set; }
        
        public void Create(int userId, List<int> tagsIds)
        {
            UserId = userId;
            var insertCmd = $"insert into articles(latin_header, russian_header, body, author_user_id) values('{HeaderLatin}', '{Header}', '{Body}', '{UserId}') returning id";
            var cmd = new NpgsqlCommand(insertCmd, DBConnect.Connection);
            var reded = cmd.ExecuteScalar();
            Id = (int)reded;
            cmd.Dispose();
            foreach (var x in tagsIds)
            {
                var tagArticle = new TagArticle { ArticleId = Id, TagId = x };
                tagArticle.Create();
            }
        }
        public void Update(List<int> tagsIds)
        {
            var insertCmd = $"update articles set latin_header='{HeaderLatin}', russian_header='{Header}', body='{Body}', date_last_edit=clock_timestamp() where id='{Id}';";
            var cmd = new NpgsqlCommand(insertCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public void Delete()
        {
            TagArticle.DeleteConnectedWithArticle(Id);
            var sqlCmd = $"delete from articles where id = {Id}";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public static Article GetById(int id)
        {
            
            var sqlCmd = $"select id, latin_header, russian_header, author_user_id, date_created, date_last_edit, body from articles where id={id};";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            var reader = cmd.ExecuteReader();
            Article rArticle = null;
            while (reader.Read())
            {
                var article = new Models.Article();
                article.Id = reader.GetInt32(0);
                article.HeaderLatin = reader.GetString(1);
                article.Header = reader.GetString(2);
                article.UserId = reader.GetInt32(3);
                article.CreatedTime = reader.GetTimeStamp(4).ToDateTime();
                article.LastEditedTime = reader.GetTimeStamp(5).ToDateTime();
                article.Body = reader.GetString(6);
                rArticle = article;
            }
            reader.Close();
            cmd.Dispose();
            return rArticle;
        }
        public static List<Article> GetAll()
        {
            List<Article> articles = new List<Article>();
            var sqlCmd = "select id, latin_header, russian_header, author_user_id, date_created, date_last_edit, body from articles;";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var article = new Models.Article();
                article.Id = reader.GetInt32(0);
                article.HeaderLatin = reader.GetString(1);
                article.Header = reader.GetString(2);
                article.UserId = reader.GetInt32(3);
                article.CreatedTime = reader.GetTimeStamp(4).ToDateTime();
                article.LastEditedTime = reader.GetTimeStamp(5).ToDateTime();
                article.Body = reader.GetString(6);
                articles.Add(article);
            }
            reader.Close();
            cmd.Dispose();
            return articles;
        }
    }
}
