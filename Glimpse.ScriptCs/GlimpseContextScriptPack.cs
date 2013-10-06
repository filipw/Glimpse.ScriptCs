using Glimpse.Core.Extensibility;
using ScriptCs.Contracts;

namespace Glimpse.ScriptCs
{
    public class GlimpseContextScriptPack : IScriptPack
    {
        private readonly GlimpsePack _ctx;

        public GlimpseContextScriptPack(ITabContext ctx)
        {
            _ctx = new GlimpsePack(ctx);
        }

        public void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("Glimpse.ScriptCs");
            session.ImportNamespace("System.Web");
            session.ImportNamespace("Glimpse.AspNet.Extensions");
        }

        public IScriptPackContext GetContext()
        {
            return _ctx;
        }

        public void Terminate()
        {
            
        }
    }
}