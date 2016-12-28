using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace GITS.Hrms.Library.Utility
{
    public class MathExpression
    {
        private static string ClassName = "Parser";
        private static string MethodName = "Eval";

        // the object cache
        private Hashtable objectCache;

        public MathExpression()
        {
            // create object cache
            objectCache = new Hashtable();
        }

        private object GetObject(string classExpression)
        {
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters();
            
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            parameters.ReferencedAssemblies.Add("system.dll");

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, classExpression);

            if (results.Errors != null && results.Errors.Count > 0)
                throw new ExecutionEngineException("Error in parsing", new Exception(results.Errors[0].ErrorText));

            if (results.CompiledAssembly == null)
                throw new NullReferenceException("Error in parsing");

            return results.CompiledAssembly.CreateInstance(ClassName);            
        }

        private string GetClassExpression(string mathExpression)
        {
            string matchingExpression = "[A-Za-z0-9_]+";
            string replacingExpression = "var$&";
            string processedMathExpression = Regex.Replace(mathExpression, matchingExpression, replacingExpression);
            Regex regex = new Regex(matchingExpression);
            StringBuilder classExpression = new StringBuilder();
            
            classExpression.Append("using System;");
            classExpression.Append("class ");
            classExpression.Append(ClassName);
            classExpression.Append("{ public double ");
            classExpression.Append(MethodName);
            classExpression.Append("(");

            MatchCollection matches = regex.Matches(processedMathExpression);
            IList<String> matchedStringList = new List<String>();
            foreach (Match match in matches)
            {
                if (!matchedStringList.Contains(match.Value))
                {
                    classExpression.Append("double ");
                    classExpression.Append(match.ToString());
                    classExpression.Append(",");

                    matchedStringList.Add(match.Value);
                }
            }

            classExpression.Remove(classExpression.Length - 1, 1);
            classExpression.Append("){ return ");
            classExpression.Append(processedMathExpression);
            classExpression.Append(";}}");

            return classExpression.ToString();
        }

        /// <summary>
        /// Evaluates a Mathematical expression
        /// </summary>
        /// <param name="mathExpression">the mathematical expression [x*(y+z)]</param>
        /// <param name="values">values of expression operands (x=2, y=5, z=9 etc.)</param>
        /// <returns>evaluated value</returns>
        public double GetValue(string mathExpression, params object[] values)
        {
            try
            {
                // retrive object from cache
                Object parser = objectCache[mathExpression];

                if (parser == null)
                {
                    // object was not cached
                    // so creat new object
                    parser = GetObject(GetClassExpression(mathExpression));

                    // and cache the new object for later use
                    objectCache[mathExpression] = parser;
                }

                // get the mehtod to be called
                MethodInfo method = parser.GetType().GetMethod(MethodName);

                // call the method, evaluate expression & return value
                return DBUtility.ToDouble(method.Invoke(parser, values));
            }
            catch (NullReferenceException nre)
            {
                throw nre;
            }
            catch (ExecutionEngineException eee)
            {
                throw eee;
            }
            catch (Exception ex)
            {
                throw new ExecutionEngineException("Error in parsing", ex);
            }
        }
    }
}