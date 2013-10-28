using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FALSE;
using FALSECompiler;

namespace Tests
{
    public class Test
    {
        private readonly List<Tuple<string, string>> _cases = new List<Tuple<string, string>>();
        private readonly string _name;

        public Test(string name)
        {
            _name = name;
        }

        public Test AddCase(string program, string output)
        {
            _cases.Add(new Tuple<string, string>(program, output));
            return this;
        }

        public bool Run()
        {
            return _cases.Aggregate(true, (f, c) => f & TestCase(c.Item1, c.Item2));
        }

        private bool TestCase(string program, string output)
        {
            var tokens = Lexer.Tokenize(program);
            var c = new Compiler();
            c.Compile("test", 1024, new ProgramContext(tokens));

            try
            {
                var processInfo = new ProcessStartInfo
                    {
                        FileName = "test.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    };

                using (var process = new Process())
                {
                    process.StartInfo = processInfo;
                    process.Start();

                    var result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit(1000);
                    if (result == output) return true;

                    Console.WriteLine("{0} failed: case {1}, expected {2} got {3}", _name, program, output, result);
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} failed: case {1}, reason {2}", _name, program, ex.Message);
                return false;
            }
        }
    }
}