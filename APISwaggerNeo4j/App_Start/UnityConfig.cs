using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using APISwaggerNeo4j.Services;
using APISwaggerNeo4j.Logging;
using APISwaggerNeo4j.Repository;
using System.Web.Mvc;
using System.Web.Http;

namespace APISwaggerNeo4j
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        //#region Unity Container
        //private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        //{
        //    var container = new UnityContainer();
        //    RegisterTypes(container);
        //    return container;
        //});

        ///// <summary>
        ///// Gets the configured Unity container.
        ///// </summary>
        //public static IUnityContainer GetConfiguredContainer()
        //{
        //    return container.Value;
        //}
        //#endregion

        ///// <summary>Registers the type mappings with the Unity container.</summary>
        ///// <param name="container">The unity container to configure.</param>
        ///// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        ///// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        //public static void RegisterTypes(IUnityContainer container)
        //{
        //    // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
        //    // container.LoadConfiguration();

        //    // TODO: Register your types here
        //    container.RegisterType<IDomainService, DomainServiceImpl>();
        //    container.RegisterType<ILogger, DatabaseLogger>();
        //    container.RegisterType<ILogger, EventViewerLogger>();
        //    container.RegisterType<IDomainRepository, DomainRepositoryImpl>();

        //    //DependencyResolver.SetResolver(new DependencyResolver(container));

        //}

        /// <summary>
        /// Registering IoC's
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IDomainService, DomainServiceImpl>();
            container.RegisterType<ILogger, DatabaseLogger>();
            container.RegisterType<ILogger, EventViewerLogger>();
            container.RegisterType<IDomainRepository, DomainRepositoryImpl>();

            DependencyResolver.SetResolver(new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}
