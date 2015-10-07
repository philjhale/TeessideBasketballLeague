using Ninject;
using Ninject.Extensions.Conventions;

namespace Basketball.IoC
{
    public class TblNinjectKernel
    {
        public static IKernel CreateWebKernel()
        {
            var kernel = new StandardKernel(new WebNinjectModule());

            return ScanAssemblies(kernel);
        }

        public static IKernel CreateWcfKernel()
        {
            var kernel = new StandardKernel(new WcfNinjectModule());

            return ScanAssemblies(kernel);
        }

        public static IKernel ScanAssemblies(IKernel kernel)
        {
            var scanner = new AssemblyScanner();
            scanner.FromAssembliesMatching("Basketball.Service.dll"); // Services classes here
            scanner.FromAssembliesMatching("Basketball.Data.dll");  // Repositories here
            scanner.BindWithDefaultConventions();

            kernel.Scan(scanner);

            return kernel;
        }
    }
}
