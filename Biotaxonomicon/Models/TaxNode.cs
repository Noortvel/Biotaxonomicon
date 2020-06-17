using Biotaxonomicon.Models.Exeptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    [Serializable]
    public class TaxNode
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int ParentNodeId { get; set; }
        public int LeftBound { get; set; }
        public int RightBound { get; set; }

        public void Create(int parentArticleId)
        {
            var sql = $"select id from tax_nodes where {parentArticleId}=article_id";
            var cmd1 = new NpgsqlCommand(sql, DBConnect.Connection);
            int? pid = (int?)cmd1.ExecuteScalar();
            cmd1.Dispose();
            if (pid != null)
            {
                ParentNodeId = pid.Value;
            }
            else
            {
                throw new InfoException(StringResources.StringResources.Instance.TaxNodeParentWithTagDontExist);
            }
            var sqlin = $"insert into tax_nodes(parent, article_id) values('{ParentNodeId}', '{ArticleId}') returning id";
            var cmd2 = new NpgsqlCommand(sqlin, DBConnect.Connection);
            int? id = (int?)cmd2.ExecuteScalar();
            if(id != null)
            {
                Id = id.Value;
                cmd2.Dispose();
            }
            else
            {
                cmd2.Dispose();
                throw new InfoException(StringResources.StringResources.Instance.ErrorCreate);
            }

        }
        public void Update(int parentArticleId)
        {
            var sql = $"select id from tax_nodes where {parentArticleId}=article_id";
            var cmd1 = new NpgsqlCommand(sql, DBConnect.Connection);
            int? pid = (int?)cmd1.ExecuteScalar();
            cmd1.Dispose();
            if (pid != null)
            {
                ParentNodeId = pid.Value;
            }
            else
            {
                throw new InfoException(StringResources.StringResources.Instance.TaxNodeParentWithTagDontExist);
            }
            var sqlCmd = $"update tax_nodes set parent={pid}, article_id={ArticleId} where id={Id}";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public static TaxNode GetById(int id)
        {
            var sqlCmd = $"select article_id, parent from tax_nodes where id = {id};";
            var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
            var reader = cmd.ExecuteReader();
            var node = new Models.TaxNode();
            int count = 0;
            while (reader.Read())
            {
                count++;
                node.Id = id;
                node.ArticleId = reader.GetInt32(0);

                var obj = reader.GetValue(1);
                var nullable = obj as DBNull;
                if(nullable == null)
                {
                    node.ParentNodeId = (int)obj;
                }
                else
                {
                    node.ParentNodeId = -1;
                }
            }
            reader.Close();
            cmd.Dispose();
            if (count == 0)
            {
                node = null;
            }
            return node;
        }
        public static List<TaxNode> GetAllNodes()
        {
            List<TaxNode> nodes = new List<TaxNode>();
            //id=2- biota
            var sqltxt = $"SELECT t.* FROM tax_nodes t JOIN tax_nodes t2 ON t.left_bound >= t2.left_bound AND t.right_bound <= t2.right_bound WHERE t2.id = 2";

            var cmd = new NpgsqlCommand(sqltxt, DBConnect.Connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var node = new Models.TaxNode();
                node.Id = reader.GetInt32(0);
                var parent = reader.GetValue(1);

                var parentNull = parent as System.DBNull;

                node.ParentNodeId = parentNull != null ? 0 : (int)parent;
                node.LeftBound = reader.GetInt32(2);
                node.RightBound = reader.GetInt32(3);
                node.ArticleId = reader.GetInt32(4);
                nodes.Add(node);
            }
            reader.Close();
            cmd.Dispose();
            return nodes;
        }
    }
}
