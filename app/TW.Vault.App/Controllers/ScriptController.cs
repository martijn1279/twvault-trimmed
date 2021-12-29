using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TW.Vault.Scaffold;

namespace TW.Vault.Controllers
{
    // [Route("script")]
    [EnableCors("AllOrigins")]
    public class ScriptController : BaseController
    {
        ASPUtil asputil;

        public ScriptController(IWebHostEnvironment environment, IServiceScopeFactory scopeFactory, VaultContext context, ILoggerFactory loggerFactory) : base(context, scopeFactory, loggerFactory)
        {
            asputil = new ASPUtil(environment);
        }

        // [HttpGet("{*name}", Name = "GetCompiledObfuscatedScript")]
        public IActionResult GetCompiledObfuscated(String name)
        {
            if (asputil.UseProductionScripts)
            {
                var contents = ResolveFileContents(name);
                if (contents != null)
                    return Content(contents, "application/javascript");
                else
                    return NotFound();
            }

            String errorString = null, notFoundString = null;
            String scriptContents = MakeCompiled(name, (e) => errorString = e, (n) => notFoundString = n);

            if (errorString != null)
                return StatusCode(500, errorString);

            if (notFoundString != null)
                return NotFound();

            return Content(scriptContents, "application/javascript");
        }

        private String ResolveFileContents(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            if (asputil.UseProductionScripts)
            {
                var path = asputil.GetObfuscatedPath(name);
                if (System.IO.File.Exists(path))
                    return System.IO.File.ReadAllText(path);
                else
                    return null;
            }

            var resolvedPath = asputil.GetFilePath(name);

            if (System.IO.File.Exists(resolvedPath))
                return System.IO.File.ReadAllText(resolvedPath);
            else
                return null;
        }

        private String MakeCompiled(String name, Action<String> onError, Action<String> onNotFound)
        {
            var scriptCompiler = new Features.ScriptCompiler();
            scriptCompiler.InitCommonVars();

            List<String> failedFiles = new List<string>();

            List<String> missingCVars = new List<string>();
            IEnumerable<String> circularDependencyChain = null;
            scriptCompiler.DependencyResolver = (fileName) =>
            {
                String contents = ResolveFileContents(fileName);
                if (contents == null)
                    failedFiles.Add(fileName);
                return contents;
            };
            scriptCompiler.OnCircularDependency += (chain) => circularDependencyChain = new[] { name }.Concat(chain);
            scriptCompiler.OnMissingCVar += (varname) => missingCVars.Add(varname);

            String scriptContents = scriptCompiler.Compile(name);

            if (missingCVars.Count > 0)
            {
                onError?.Invoke("Error compiling script due to missing CVar values for: " + String.Join(", ", missingCVars));
                return null;
            }

            if (circularDependencyChain != null)
            {
                onError?.Invoke("Error compiling script due to circular dependency: " + String.Join(" -> ", circularDependencyChain.ToArray()));
                return null;
            }

            if (failedFiles.Any())
            {
                if (failedFiles.Contains(name))
                    onNotFound?.Invoke(name);
                else
                    onError?.Invoke("Error compiling script since dependencies [" + String.Join(", ", failedFiles) + "] could not be resolved");

                return null;
            }

            String originalUrl = Request.Headers["X-Original-Url"].FirstOrDefault();
            if (originalUrl == null)
                originalUrl = Request.Path.Value;

            if (originalUrl.Contains("?"))
                originalUrl = originalUrl.Substring(0, originalUrl.IndexOf('?'));

            return scriptContents;
        }
    }
}
