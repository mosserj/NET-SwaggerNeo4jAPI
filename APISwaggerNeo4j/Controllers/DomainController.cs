using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APISwaggerNeo4j.ModelDTO;
using APISwaggerNeo4j.Services;
using APISwaggerNeo4j.Mapping;
using System.Web.Http.Cors;
using APISwaggerNeo4j.Logging;

namespace APISwaggerNeo4j.Controllers
{
    /// <summary>
    /// test title
    /// </summary>
    public class DomainController : ApiController
    {
        private IDomainService service;
        /// <summary>
        /// Constructor 
        /// </summary>
        public DomainController()
        {
            //IoC maybe unity?
            //new DatabaseLogger(); We could specify different logging for the service via Dependency Inversion principle
            //Probably better off using log4net 
            service = new DomainServiceImpl(new EventViewerLogger());
        }

        /// <summary>
        /// Get domain by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/domain/{name}/")]
        public IHttpActionResult Get(string name) 
        {
            try
            {
                return Ok(DomainMapper.ToDomainDTO(service.Get(name)));
            }
            catch(HttpException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode)ex.ErrorCode)
                {
                    ReasonPhrase = ex.Message
                });
            }
        }

        /// <summary>
        /// Add Domain
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [HttpPost]
        [Route("api/v1/domain/add")]
        public IHttpActionResult Post(DomainDTO domain)
        {
            try
            {
                return Ok(DomainMapper.ToDomainDTO(service.Add(DomainMapper.ToDomain(domain))));
            }
            catch (HttpException ex)
            {
                var errorResponse = Request.CreateErrorResponse((HttpStatusCode)ex.GetHttpCode(), ex.Message);
                throw new HttpResponseException(errorResponse);
            }
        }

        /// <summary>
        /// Clear Database and set constraints
        /// </summary>
        /// <returns></returns>
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/v1/domain/clear")]
        public IHttpActionResult ClearDatabase()
        {
            try
            {
                service.ClearDb();
                return Ok();
            }
            catch (HttpException ex)
            {
                var errorResponse = Request.CreateErrorResponse((HttpStatusCode)ex.GetHttpCode(), ex.Message);
                throw new HttpResponseException(errorResponse);
            }
        }

        /// <summary>
        /// Add random 100k domains and ip's with random relationships
        /// </summary>
        /// <returns></returns>
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/v1/domain/create")] //add an input to change 100k
        public IHttpActionResult CreateRandomDomains()
        {
            try
            {
                //this needs to be async and maybe even talk back to the client as domains are created. For now I'm just going 
                //to create 100k domains
                service.CreateDomains();
                return Ok();
            }
            catch (HttpException ex)
            {
                var errorResponse = Request.CreateErrorResponse((HttpStatusCode)ex.GetHttpCode(), ex.Message);
                throw new HttpResponseException(errorResponse);
            }
        }
    }
}
