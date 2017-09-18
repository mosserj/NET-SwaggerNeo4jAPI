# microsoft-SwaggerNeo4jAPI

//.Net swagger api - http://localhost/swagger/ui/index#/Domain
(Take a look at the attached images neo4j, swagger)
Cors, neo4jDriver, neo4jClient, webapi

This was a quick exercise to introduce myself to neo4j graphical database. Each Domain has a name and IP. Each IP has to be unique but 
the domain name can have multiple IP's associated to it. The API Endpoints handles adding new domains, clearing the database, and
kicking off a service that adds random 100k domains with random relationships.

//service that creates 100k domains
//http://localhost/api/v1/domain/create/
This is cool to watch but eventually breaks the javascript 3d gui. Maybe we can add more groupings to improve performance

Have a look at my Neo4jNodeJSClient which leverages a javascript neo4jDriver for loading the graph using 3dJs with 
searching ability. The client also uses this applications swagger API to handle adding new Domains, Clearing the database, and kicking 
off the job that creates 100k domains.

//Remember to load neo4j locally on windows and create a few constraints to start with. The code should handle creating these if 
they do not exist.

CREATE CONSTRAINT ON (n:Domain) ASSERT n.name IS UNIQUE;
CREATE CONSTRAINT ON (n:IP) ASSERT n.ip IS UNIQUE;
