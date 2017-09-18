using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APISwaggerNeo4j.Models;
using APISwaggerNeo4j.Repository;
using APISwaggerNeo4j.Logging;

namespace APISwaggerNeo4j.Services
{
    public class DomainServiceImpl : IDomainService
    {
        private ILogger _logger;
        private IDomainRepository _repo;

        public DomainServiceImpl(ILogger logger, IDomainRepository repo)
        {
            this._repo = repo;
            this._logger = logger;
        }

        public Domain Get(string name)
        {
            //IoC maybe unity?
            DomainRepositoryImpl repo = new DomainRepositoryImpl();
            try
            {
                return _repo.Get(name);
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
            try
            {
                return _repo.Add(domain);
            }
            catch (HttpException ex)
            {
                _logger.Log(ex.Message);
                throw new HttpException(ex.ErrorCode, ex.Message, ex.InnerException);
            }
        }

        public void ClearDb()
        {
            _repo.ClearDb();
        }

        public void CreateDomains()
        {
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
                    _repo.Add(domain);
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