using System.ServiceModel;
using Ninject.Modules;
using Basketball.Data;
using Basketball.Common.BaseTypes.Interfaces;

namespace Basketball.IoC
{
    public class WcfNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // http://stackoverflow.com/a/7740269/299048
            Bind<IDbContext>().To<WcfBasketballContext>().InScope(c => OperationContext.Current);
        }
    }
}
