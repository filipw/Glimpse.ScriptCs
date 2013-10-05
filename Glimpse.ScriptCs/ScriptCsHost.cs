using ScriptCs;
using ScriptCs.Contracts;
using log4net;

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
                                                                            .InMemory(true)
                                                                            .Repl(false);
            Root = scriptServicesBuilder.Build();
        }
    }
}