using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Glimpse.AspNet.Extensibility;
using Glimpse.Core.Extensibility;
using ScriptCs.Contracts;

namespace Glimpse.ScriptCs
{
    public class GlimpseContext : IScriptPackContext
    {
        public ITabContext Context { get; private set; }

        public GlimpseContext(ITabContext ctx)
        {
            Context = ctx;
        }
    }

    public class GlimpseContextScriptPack : IScriptPack
    {
        private readonly GlimpseContext _ctx;

        public GlimpseContextScriptPack(ITabContext ctx)
        {
            _ctx = new GlimpseContext(ctx);
        }

        public void Initialize(IScriptPackSession session)
        {
            session.AddReference("System.Web");
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

    public class ScriptCsTab : AspNetTab
    {
        private string LoadCode(string path)
        {
            var code = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, path);
            return code;
        }

        public override object GetData(ITabContext context)
        {
            var glimpseResult = new List<object[]>();
            var code = File.ReadAllText(LoadCode("glimpse.csx"));

            glimpseResult.Add(new object[] {"Executed code", code});

            var host = new ScriptCsHost();
            host.Root.Executor.Initialize(new[] { LoadCode("bin\\Glimpse.Core.dll"), LoadCode("bin\\Glimpse.AspNet.dll"), LoadCode("bin\\Glimpse.ScriptCs.dll"), LoadCode("bin\\ScriptCs.Contracts.dll") }, new[] { new GlimpseContextScriptPack(context),  });
            var result = host.Root.Executor.ExecuteScript(code, new string[0]);
            host.Root.Executor.Terminate();

            if (result.CompileExceptionInfo != null) glimpseResult.Add(new object[] { "Compilation exception", result.CompileExceptionInfo.SourceException.Message });;
            if (result.ExecuteExceptionInfo != null) glimpseResult.Add(new object[] { "Execution exception", result.ExecuteExceptionInfo.SourceException.Message }); ;

            glimpseResult.Insert(0, new[] { "Script result", result.ReturnValue });
            return glimpseResult;
        }

        public override string Name
        {
            get { return "ScriptCs"; }
        }
    }
}
