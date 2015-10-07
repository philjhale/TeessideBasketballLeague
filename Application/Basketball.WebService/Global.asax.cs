using Basketball.IoC;
using Ninject;
using Ninject.Extensions.Wcf;

namespace Basketball.WebService
{
    public class Global : NinjectWcfApplication
    {
        protected override IKernel CreateKernel()
        {
            return TblNinjectKernel.CreateWcfKernel();
        }
    }
}