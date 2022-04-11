using System;
using System.Collections.Generic;

namespace Waterfall
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Waterfall( new List<Func<string, string>>
            {
                SourceA,
                SourceC,
                SourceB
            },"hello");


            Console.WriteLine(result);
        }

        private static T1 Waterfall<T1,T2>(IEnumerable<Func<T2, T1>> waterfallFunctions, T2 parameter)
        {
            var waterfallExceptions = new List<Exception>();
            foreach (var waterfallFunction in waterfallFunctions)
            {
                try
                {
                    var functionResult = waterfallFunction(parameter);
                    return functionResult;
                }
                catch (ExpectedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                   waterfallExceptions.Add(e);
                }
            }
            throw new AggregateException(waterfallExceptions);
        }

        static string SourceA(string input)
        {
            throw new Exception("Some random error");
        }

        static string SourceB(string input)
        {
            throw new ExpectedException();
        }

        static string SourceC(string input)
        {
            return "OK";
        }




    }

    class ExpectedException : Exception
    {
    }
}
