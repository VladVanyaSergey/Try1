using GraphDB.Models.Ontology;
using Neo4j.Driver.V1;
using System.Collections.Generic;

namespace GraphDB
{
    public class TransactionManager
    {
        private Connection connection;

        public TransactionManager(Connection connection)
        {
            this.connection = connection;
        }

        public Neo4jUnwrapedResult CreateEntity(Entity item)
        {
            return item.Properties.Count != 0 ? 
                new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.CreateEntityQuery(item), item.Properties))) : 
                new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.CreateEntityWithoutQuery(item))));
        }

        public Neo4jUnwrapedResult FindEntity(Entity item)
        {
            return item.Properties.Count != 0 ? 
                new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.FindEntityQuery(item), item.Properties))) : 
                new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.FindEntityQuery(item.Labels))));                    //Checked
        }

        public Neo4jUnwrapedResult CountQuery(Dictionary<string, object> param)
        {            
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.CountQuery(param))));
        }

        public Neo4jUnwrapedResult CountQuery(string[] label)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.CountQuery(label))));                           //Checked
        }

        public Neo4jUnwrapedResult CreateLink(Link link)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.CreateLinkQuery(link), link.Properties)));      //Checked
        }

        public Neo4jUnwrapedResult GetEntityById(long id)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.GetEntityById(id))));                           //Checked
        }

        public Neo4jUnwrapedResult FindLink(Link link)
        {
            return link.Properties.Count != 0 ? 
                new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.FindLinkQuery(link), link.Properties))) : 
                new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.FindLinkQuery(link.Type))));                        //Checked
        }

        public Neo4jUnwrapedResult GetLinkById(long id)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.GetLinkById(id))));                               //Checked
        }

        public Neo4jUnwrapedResult UpdateEntity(Entity item)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.UpdateEntityQuery(item), item.Properties)));      //Checked
        }

        public Neo4jUnwrapedResult GetEntitiesByClass(string[] labels)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.GetEntityByClass(labels))));                     //Checked
        }

        public Neo4jUnwrapedResult GetLinksByClass(string label)
        {
            return new Neo4jUnwrapedResult(Execute(new Statement(StoredQueries.GetLinksByClass(label))));                       //Checked
        }


        private IStatementResult Execute(Statement transaction)
        {
            var rlt = connection.Session.Run(transaction);
            return rlt;
        }
    }
}
