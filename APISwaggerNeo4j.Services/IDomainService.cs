using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APISwaggerNeo4j.Models;
using APISwaggerNeo4j.Repository;

namespace APISwaggerNeo4j.Services
{
    public interface IDomainService
    {
        Domain Get(string name);


        Domain Add(Domain domain);


        void ClearDb();


        void CreateDomains();
    }
}