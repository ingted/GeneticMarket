using System;
using System.Collections;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.Resources;
using System.IO;

namespace GeneticMarket.Base.Helper
{
    public static class RuntimeHelper
    {
        //static RuntimeHelper()
        //{
        //    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        //}

        public static RuntimeResult CompileCode(string code,List<string> referenceAssemblies)
        {
            RuntimeResult result = new RuntimeResult();
            Type interfaceType = typeof(IStrategyRefiner);
            CompilerParameters CompilerParams = new CompilerParameters();

            CompilerParams.GenerateInMemory = true;
            CompilerParams.TreatWarningsAsErrors = false;
            CompilerParams.GenerateExecutable = false;
            CompilerParams.CompilerOptions = "/optimize";

            string[] references = { "System.dll", "mscorlib.dll", "System.Xml.dll"};
            CompilerParams.ReferencedAssemblies.AddRange(references);

            foreach (string assemblyName in referenceAssemblies)
            {
                CompilerParams.ReferencedAssemblies.Add(assemblyName);
            }

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams, code);

            if (compile.Errors.HasErrors)
            {
                string text = "Compile error: ";

                foreach (CompilerError ce in compile.Errors)
                {
                    text += "rn" + ce.ToString();
                }
                result.Success = false;
                result.ErrorMessage = text;
                return result;
            }

            result.Success = false;

            foreach (Type t in compile.CompiledAssembly.GetTypes())
            {
                if (interfaceType.IsAssignableFrom(t))
                {
                    result.CreatedObject = compile.CompiledAssembly.CreateInstance(t.FullName);
                    result.Success = true;
                }
            }

            return result;
        }


        //static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    string dllName = new AssemblyName(args.Name).Name + ".dll";

        //    //args.Name
        //    if (RuntimeManager.GetInstance().CommonLibraries.Contains(dllName))
        //    {
        //        string path = Path.Combine(FileUtils.AppFolder, Def.CommonLibFolder);
        //        path = Path.Combine(path, dllName);

        //        return Assembly.LoadFile(path);
        //    }

        //    return null;
        //}
    }

    public interface IStrategyRefiner
    {
        bool CheckRefine(int winningPositionCount, int loosingPositionCount,
            int totalLoss, int totalWin, int largestLoss,
            int largestWin, int closedPositionCount, int openPositionCount,
            double reputation);
    }

    //sample test class
    //public class test : IStrategyRefiner
    //{
    //    public bool CheckRefine(int winningPositionCount, int loosingPositionCount,
    //        int totalLoss, int totalWin, int largestLoss,
    //        int largestWin, int closedPositionCount, int openPositionCount,
    //        Dictionary<string, double> reputation)
    //    {
    //        if (totalLoss > 10)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}

    public class RuntimeResult
    {
        public bool Success = true;
        public string ErrorMessage = "";
        public object CreatedObject = null;
    }
}
