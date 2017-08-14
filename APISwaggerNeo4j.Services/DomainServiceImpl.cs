using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APISwaggerNeo4j.Models;
using APISwaggerNeo4j.Repository;

namespace APISwaggerNeo4j.Services
{
    public class DomainServiceImpl : IDomainService
    {
        public Domain Get(string name)
        {
            //IoC maybe unity?
            DomainRepositoryImpl repo = new DomainRepositoryImpl();
            try
            {
                return repo.Get(name);
            }
            //change this to not found etc
            catch (HttpException ex)
            {
                // Add logging
                throw new HttpException(ex.ErrorCode, ex.Message, ex.InnerException);
            }
        }

        public Domain Add(Domain domain)
        {
            //IoC maybe unity?
            DomainRepositoryImpl repo = new DomainRepositoryImpl();
            try
            {
                return repo.Add(domain);
            }
            //change this to not found etc
            catch (HttpException ex)
            {
                // Add logging
                throw new HttpException(ex.ErrorCode, ex.Message, ex.InnerException);
            }
        }

        public void ClearDb()
        {
            //IoC maybe unity?
            DomainRepositoryImpl repo = new DomainRepositoryImpl();

            repo.ClearDb();
        }

        public void CreateDomains()
        {
            //TODO
            //This has an error in it. You need to click it a few times here and there =/

            //IoC maybe unity?
            DomainRepositoryImpl repo = new DomainRepositoryImpl();
            try
            {
                Random random = new Random();
                List<String> tempDomains = new List<string>();
                for(int d = 1; d <= 100000; d ++)
                {
                    String name = "domain" + d + ".com";
                    if (random.Next(1, 101) <= 70 && tempDomains.Count() > 0)
                    {
                        // if falls within the 70% chance or reusing a domain then grab a random domain name from the temp names
                        name = tempDomains[random.Next(1, tempDomains.Count())];
                    }
                    else
                    {
                        tempDomains.Add(name);
                    }

                    Domain domain = new Domain(GetRandomUniqueIP(), name);
                    repo.Add(domain);
                }
            }
            //change this to not found etc
            catch (HttpException ex)
            {
                // Add logging
                throw new HttpException(ex.ErrorCode, ex.Message, ex.InnerException);
            }
        }

        private String GetRandomUniqueIP()
        {
            //by chance we could get a duplicate but the neo4j will reject this.. For now I'll keep it this way
            //need a better way to insure unique id's for the future
            var random = new Random();
            return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
        }
    }
}