﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TW.Vault.Features
{
    public class ScriptCompiler
    {
        private static Regex RequireRegex = new Regex(@"^([ \t]*).*\/\/\#\s*REQUIRE\s+([^\n\r]+)", RegexOptions.Multiline | RegexOptions.Compiled);
        private static Regex CVarRegex = new Regex(@"`?%V<([^>]+)>`?", RegexOptions.Multiline | RegexOptions.Compiled);

        public delegate String ResolveDependencyDelegate(String fileName);
        public delegate void CircularDependencyDelegate(IEnumerable<String> dependencyChain);
        public delegate void MissingCVarDelegate(String varName);

        public ResolveDependencyDelegate DependencyResolver;
        public event CircularDependencyDelegate OnCircularDependency;
        public event MissingCVarDelegate OnMissingCVar;

        public Dictionary<String, String> CompileTimeVars { get; set; }

        public void InitCommonVars()
        {
            CompileTimeVars = new Dictionary<string, string>
            {
                { "HOSTNAME", Configuration.Initialization.ServerHostname },
            };
        }

        public String Compile(String scriptName) => CompileWithDependencies(scriptName, new List<String>(), new List<string>());

        private String BakeCompileVars(String scriptContents)
        {
            return CVarRegex.Replace(scriptContents, match =>
            {
                var varName = match.Groups[1].Value;
                if (CompileTimeVars.ContainsKey(varName))
                {
                    return CompileTimeVars[varName];
                }
                else
                {
                    OnMissingCVar?.Invoke(varName);
                    return "";
                }
            });
        }

        private String CompileWithDependencies(String scriptName, List<String> resolvedDependencies, List<String> workingDependencies)
        {
            String scriptContents = DependencyResolver(scriptName);
            if (scriptContents == null)
                return null;

            String scriptWithBakedVars = BakeCompileVars(scriptContents);

            bool canContinue = true;

            String compilationResult = RequireRegex.Replace(scriptWithBakedVars, match =>
            {
                if (!canContinue)
                    return "";

                var spacing = match.Groups[1].Value;
                var dependentFileName = match.Groups[2].Value;
                if (resolvedDependencies.Contains(dependentFileName))
                    return "";

                if (workingDependencies.Contains(dependentFileName))
                {
                    OnCircularDependency?.Invoke(workingDependencies.Concat(new[] { dependentFileName }).ToArray());
                    canContinue = false;
                    return "";
                }

                workingDependencies.Add(dependentFileName);
                String compiledDependency = CompileWithDependencies(dependentFileName, resolvedDependencies, workingDependencies);
                workingDependencies.Remove(dependentFileName);

                if (compiledDependency == null)
                {
                    canContinue = false;
                    return "";
                }

                resolvedDependencies.Add(dependentFileName);

                var scriptLines = compiledDependency.Split('\n');
                var formattedDependency = new StringBuilder();

                foreach (String line in scriptLines)
                {
                    formattedDependency.Append(spacing);
                    formattedDependency.AppendLine(line.Trim('\r', '\n'));
                }

                return formattedDependency.ToString();
            });

            return compilationResult;
        }
    }
}
