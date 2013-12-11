using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FALSE
{
    public static class Lexer
    {
        private static readonly Dictionary<String, String> EscapeMapping = new Dictionary<String, String>
        {
            { "\\\"", "\"" },
            { @"\\", "\\" },
            { @"\a", "\a" },
            { @"\b", "\b" },
            { @"\f", "\f" },
            { @"\n", "\n" },
            { @"\r", "\r" },
            { @"\t", "\t" },
            { @"\v", "\v" },
            { @"\0", "\0" }
        };

        public static IEnumerable<Token> Tokenize(String ex)
        {
            var program = new List<Token>(ex.Length);
            IState currentState = States.Common;  
            int prevPtr = -1;

            for (int ptr = 0; ptr <= ex.Length; ptr++)
            {
                var fullState = currentState.Process(ptr == ex.Length ? (char)0 : ex[ptr]);
                currentState = fullState.Item1;
                var token = fullState.Item2;

                if (token != null)
                {
                    if (token.Type == OpCode.LoadConst) ptr--;
                    token.Position = ptr;

                    if (token.Type != OpCode.Error)
                        token.Arg = ex.Substring(prevPtr + 1, ptr - prevPtr);

                    prevPtr = ptr;
                    program.Add(token);
                }
            }

            PostProcess(program);

            return program;
        }

        private static void PostProcess(IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case OpCode.LoadConst:
                        token.Arg = int.Parse((string)token.Arg);
                        break;
                    case OpCode.LoadChar:
                        token.Arg = ((String)token.Arg)[1];
                        break;
                    case OpCode.PrintStr:
                        var strBld = new StringBuilder((string)token.Arg);
                        foreach (var pair in EscapeMapping)
                            strBld.Replace(pair.Key, pair.Value);
                        token.Arg = strBld.Length > 2 ? strBld.ToString(1, strBld.Length - 2) : "";
                        break;
                    case OpCode.LoadVar:
                    case OpCode.SaveVar:
                        token.Arg = ((string)token.Arg)[0] - 'a';
                        break;
                }
            }
        }
    }
}
