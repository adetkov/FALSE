using System;
using System.Linq;

namespace Tests
{
    internal class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            var tests = new[]
                {
                    new Test("Load number")
                        .AddCase("100.", "100")
                        .AddCase("0.", "0")
                        .AddCase("1 2 3.", "3")
                        .AddCase("1.2.3.", "123"),

                    new Test("Load and write char")
                        .AddCase("'a,", "a")
                        .AddCase("'!'o'l'l'e'H,,,,,,", "Hello!"),

                    new Test("Write text")
                        .AddCase(@"""Hello!""", "Hello!")
                        .AddCase(@"""""", "")
                        .AddCase(@"""test\ntest""", "test\ntest"),

                    new Test("Negate")
                        .AddCase("10_.", "-10")
                        .AddCase("10__.", "10")
                        .AddCase("0_.", "0"),

                    new Test("Add")
                        .AddCase("10 1+.", "11")
                        .AddCase("1 0+.", "1")
                        .AddCase("0 2+.", "2")
                        .AddCase("1_1+.", "0"),

                    new Test("Substract")
                        .AddCase("1 2-.", "1")
                        .AddCase("2 1-.", "-1")
                        .AddCase("2 2-.", "0")
                        .AddCase("0 10-.", "10")
                        .AddCase("10 0-.", "-10"),

                    new Test("Multiply")
                        .AddCase("2 2*.", "4")
                        .AddCase("1 6*.", "6")
                        .AddCase("6 1*.", "6")
                        .AddCase("0 9*.", "0")
                        .AddCase("9 0*.", "0")
                        .AddCase("1_1_*.", "1"),

                    new Test("Divide")
                        .AddCase("2 4/.", "2")
                        .AddCase("3 4/.", "1")
                        .AddCase("1 6/.", "6")
                        .AddCase("4 0/.", "0")
                        .AddCase("5 5/.", "1"),

                    new Test("Logical not")
                        .AddCase("1~.", "0")
                        .AddCase("0~.", "1"),

                    new Test("Equal")
                        .AddCase("10 10=.", "1")
                        .AddCase("9 10=.", "0"),

                    new Test("Logical and")
                        .AddCase("0 0&.", "0")
                        .AddCase("1 1&.", "1")
                        .AddCase("1 0&.", "0")
                        .AddCase("0 1&.", "0"),

                    new Test("Logical or")
                        .AddCase("0 0|.", "0")
                        .AddCase("1 1|.", "1")
                        .AddCase("1 0|.", "1")
                        .AddCase("0 1|.", "1"),

                    new Test("Greater")
                        .AddCase("1 2>.", "1")
                        .AddCase("1_2>.", "1")
                        .AddCase("30 10>.", "0")
                        .AddCase("4 4>.", "0"),

                    new Test("Duplicate")
                        .AddCase("2$..", "22"),

                    new Test("Drop")
                        .AddCase("1 2 3%..", "21"),

                    new Test("Variables")
                        .AddCase("10a:3 4+a;-.", "3")
                        .AddCase("1a:2t:3z:z;t;a;...", "123"),

                    new Test("Functions")
                        .AddCase("[2.]", "")
                        .AddCase("[2.]!", "2")
                        .AddCase("[2 3+]!3*.", "15")
                        .AddCase("[1]![2 3-]!+.", "2")
                        .AddCase(@"[""Hello""]a:[a;!"", World!""]!", "Hello, World!")
                };

            if (tests.Aggregate(true, (s, t) => s & t.Run()))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Succeeded!");
            }

            Console.ReadKey(true);
        }
    }
}