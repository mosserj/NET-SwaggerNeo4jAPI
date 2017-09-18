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
    public interface IDomainRepository
    {
        Domain Get(string name);

        Domain Add(Domain domain);

        void Delete(Domain domain);

        /// <summary>
        /// Clear NEO4J Database
        /// </summary>
        /// <param name="client"></param>
        void ClearDb();
    }
}