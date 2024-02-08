using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace _1FinalCapstone
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Specify the path to the libwkhtmltox library
            var libraryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libwkhtmltox.dll");

            // Manually resolve the assembly
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assemblyName = new AssemblyName(args.Name).Name;
                var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".dll");

                if (File.Exists(assemblyPath))
                {
                    try
                    {
                        return Assembly.LoadFrom(assemblyPath);
                    }
                    catch (Exception ex)
                    {
                        // Log or output the exception details for debugging
                        Console.WriteLine($"Error loading assembly: {assemblyName}. Exception: {ex.Message}");
                    }
                }

                return null;
            };

            // Continue with the existing initialization logic
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
