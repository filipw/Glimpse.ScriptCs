using Glimpse.Core.Extensibility;
using ScriptCs.Contracts;

namespace Glimpse.ScriptCs
{
    public class GlimpsePack : IScriptPackContext
    {
        public ITabContext Context { get; private set; }

        public GlimpsePack(ITabContext ctx)
        {
            Context = ctx;
        }
    }
}