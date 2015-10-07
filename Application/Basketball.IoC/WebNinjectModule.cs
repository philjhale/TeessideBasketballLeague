using System;
using System.Data.Entity.Infrastructure;
using Basketball.Data.Connection;
using Ninject.Modules;
using Basketball.Data;
using Basketball.Common.BaseTypes.Interfaces;
using Ninject.Parameters;

namespace Basketball.IoC
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContext>().To<BasketballContext>().InRequestScope();

            // TODO Fix this because it doesn't work. I couldn't figure out how to bind the static parameter
            //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Basketball"].ConnectionString;
            //if (string.IsNullOrEmpty(connectionString))
            //    throw new Exception("The connection string for Basketball could not be found in the configuration, please make sure you have set this");

            //Bind<IDbConnectionFactory>().To<CachedContextConnectionFactory>().InRequestScope().WithParameter(new PropertyValue("nameOrConnectionString", connectionString));
            Bind<IDbConnectionFactory>().To<CachedContextConnectionFactory>().InRequestScope();
        }
    }
}
