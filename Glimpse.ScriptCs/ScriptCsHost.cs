using System.Web;
using ScriptCs;
using log4net;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;
using LogLevel = ScriptCs.Contracts.LogLevel;


namespace Glimpse.ScriptCs
{
    public class ScriptCsHost
    {
        public ScriptServices Root { get; private set; }

        public ScriptCsHost()
        {
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            var commonLogger = new CodeConfigurableLog4NetLogger(logger);

            var scriptServicesBuilder =
                new ScriptServicesBuilder(new ScriptConsole(), commonLogger).LogLevel(LogLevel.Info)
                    .Repl(false)
                    .ScriptEngine<RoslynScriptInMemoryEngine>();

            /* to enable dynamic redirects, which I am not sure are needed for this use case.
             
            var appDomainResolver = scriptServicesBuilder.InitializationServices.GetAppDomainAssemblyResolver();
            var assemblies = System.IO.Directory.EnumerateFiles(HttpContext.Current.Server.MapPath("bin"), "*.dll");
            appDomainResolver.AddAssemblyPaths(assemblies);
             
             */

            Root = scriptServicesBuilder.Build();
        }
    }
}