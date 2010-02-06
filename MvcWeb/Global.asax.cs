using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Stuffoo.Core.DataAccess;
using Stuffoo.Core.Models;
using Module = Autofac.Module;

namespace Stuffoo.MvcWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IContainerProviderAccessor
    {
         static IContainerProvider _containerProvider;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new NHibernateModule());
            _containerProvider = new ContainerProvider(builder.Build());
            ControllerBuilder.Current.SetControllerFactory(
                new AutofacControllerFactory(_containerProvider));

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

        }

        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }
    }

    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.RegisterGeneric(typeof(NHibernateRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerMatchingLifetimeScope(WebLifetime.Request);

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession())
                .InstancePerMatchingLifetimeScope(WebLifetime.Request);

            const string connectionString = 
                @"data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|Stuffoo.mdf;User Instance=true";

            builder.Register( c => RegisterNHibernate(connectionString))
                .SingleInstance();
        }

        private static ISessionFactory RegisterNHibernate(string connectionString)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(connectionString))
                .Mappings(
                    m =>
                        m.FluentMappings
                        .AddFromAssemblyOf<Stuff>())
                .BuildSessionFactory();
        }
    }

}