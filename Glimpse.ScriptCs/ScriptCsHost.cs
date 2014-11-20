using Common.Logging;
using ScriptCs;
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

            var scriptServicesBuilder =
                new ScriptServicesBuilder(new ScriptConsole(), logger).LogLevel(LogLevel.Info)
                                                                            .Cache(false)
                                                                            .Repl(false)
                                                                            .ScriptEngine<RoslynScriptEngine>();
            Root = scriptServicesBuilder.Build();
        }
    }
}