using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APISwaggerNeo4j.Models;
using APISwaggerNeo4j.ModelDTO;
using APISwaggerNeo4j.Validation;

namespace APISwaggerNeo4j.Mapping
{
    /// <summary>
    /// Map Business models to DTO
    /// </summary>
    public static class DomainMapper
    {
        /// <summary>
        /// Validate and map the DTO
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static Domain ToDomain(DomainDTO domain)
        {
            //validation
            try
            {
                domain = CleanUpDomain(domain);

                if (String.IsNullOrEmpty(domain.IP) || String.IsNullOrEmpty(domain.Name))
                {
                    throw new Exception("Missing IP or Domain Name"); 
                }

                if (!DomainValidation.IsValidDomainName(domain.Name)){
                    throw new Exception("Invalid domain name format");
                }

                if (!DomainValidation.ValidateIPv4(domain.IP)){
                    throw new Exception("Invalid IP format");
                }
            }
            catch(Exception ex)
            {
                throw new HttpException(500, ex.Message, ex.InnerException);
            }

            // Map
            return new Domain(domain.IP, domain.Name);
        }

        /// <summary>
        /// Validate and map the DTO
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static DomainDTO ToDomainDTO(Domain domain)
        {
            // Map
            return new DomainDTO(domain.IP, domain.Name);
        }

        private static DomainDTO CleanUpDomain(DomainDTO domain)
        {
            // add future cleanups.. maybe http?

            domain.Name = domain.Name.Replace("www.", "");
            return domain;
        }
    }

}