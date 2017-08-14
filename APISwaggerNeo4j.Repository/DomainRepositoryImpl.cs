using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4j.Driver.V1;
using APISwaggerNeo4j.Models;
using Neo4jClient.Cypher;
using Neo4jClient;

namespace APISwaggerNeo4j.Repository
{
    public class DomainRepositoryImpl
    {
        GraphClient client;

        public DomainRepositoryImpl()
        {
            // need to IOC this and pull properties from web config
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "net_user");

            client.Connect();

            CreateBaseNodes();
        }

        public Domain Get(string name)
        {
            List<Domain> domains = new List<Domain>();

            using (var driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "net_user")))
            using (var session = driver.Session())
            {
                var result = session.Run("MATCH (n:Domain) WHERE n.name = '" + name + "' RETURN n.name AS name, n.ip AS ip");

                foreach (var record in result)
                {
                    domains.Add(new Domain(record["name"].As<string>(), record["ip"].As<string>()));
                }

                if (domains.Count == 0)
                {
                    throw new HttpException(404, "Not found");
                }
                else
                {
                    return domains.First();
                }
            }
        }

        public Domain Add(Domain domain)
        {
            try
            {
                //creates domain if doesnt exist
                client.Cypher.Merge("(d:Domain { name: '" + domain.Name + "' })").ExecuteWithoutResults();


                //add ip to domain.. ip is a unique field
                var result = client.Cypher.Merge("(d: Domain { name: '" + domain.Name + "' })")
                    .With("(d)")
                    .Match("(d)")
                    .CreateUnique("(d) < - [:hasIP] - (i: IP { ip: '" + domain.IP + "'})")
                
                .Return(i => i.As<String>()).Results;

                //need to check if already exists. The query handles checking this but we need to notify the user

                return domain;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                //For now suppressing the error for trying to create a domain that already exists
                return domain;
            }
        }

        public void Delete(Domain domain)
        {
          
        }

        /// <summary>
        /// Clear NEO4J Database
        /// </summary>
        /// <param name="client"></param>
        public void ClearDb()
        {
            client.Cypher
                .Match("(n)")
                .DetachDelete("n")
                .ExecuteWithoutResults();

            client.Cypher.CreateUniqueConstraint("d:Domain", "d.name").ExecuteWithoutResults();
            client.Cypher.CreateUniqueConstraint("i:IP", "i.ip").ExecuteWithoutResults();

            //CREATE CONSTRAINT ON(n: Domain) ASSERT n.name IS UNIQUE;
            //CREATE CONSTRAINT ON(n: IP) ASSERT n.ip IS UNIQUE;
        }

        private void CreateBaseNodes()
        {

            //client.Cypher.CreateUniqueConstraint("d:Domain", "c.UserId").ExecuteWithoutResults();
            //client.Cypher.CreateUniqueConstraint("c:User", "c.UserId").ExecuteWithoutResults();
        }
    }
}