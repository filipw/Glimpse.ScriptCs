using Common.Logging.Log4Net;
using log4net.Core;

namespace Glimpse.ScriptCs
{
    internal class CodeConfigurableLog4NetLogger : Log4NetLogger
    {
        protected internal CodeConfigurableLog4NetLogger(ILoggerWrapper log)
            : base(log)
        {
        }
    }
}