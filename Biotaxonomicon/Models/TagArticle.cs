using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    public class TagArticle
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int ArticleId { get; set; }
        
        public void Create()
        {
            var insertCmd = $"insert into tags_articles(tag_id, article_id) values('{TagId}', '{ArticleId}') returning id";
            var cmd = new NpgsqlCommand(insertCmd, DBConnect.Connection);
            Id = (int)cmd.ExecuteScalar();
            cmd.Dispose();
        }
        public static void DeleteConnectedWithArticle(int articleId)
        {
            var sqlCmd = $"delete from tags_articles where article_id={articleId}";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public static void DeleteConnectedWithTag(int tagId)
        {
            var sqlCmd = $"delete from tags_articles where tag_id={tagId}";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
    }
}
