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
using ScriptCs;

namespace Glimpse.ScriptCs
{
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

            var filepath = LoadCode("glimpse.csx");

            if (!File.Exists(filepath))
            {
                glimpseResult.Add(new[] { "Script result", "No code found" });
                return glimpseResult;
            }

            var code = File.ReadAllText(filepath);
            glimpseResult.Add(new object[] {"Executed code", code});

            var host = new ScriptCsHost();
            host.Root.Executor.Initialize(new[] { "System.Web" }, new[] { new GlimpseContextScriptPack(context),  });
            host.Root.Executor.AddReferenceAndImportNamespaces(new[] { typeof(ITabContext), typeof(AspNetTab), typeof(ScriptCsTab), typeof(IScriptExecutor) });
            
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
