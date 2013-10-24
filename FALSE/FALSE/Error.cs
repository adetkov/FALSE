using System;

namespace FALSE
{
	public class Error
	{
		public Error(String message, Int32 pos)
		{
			Message = message;
			Position = pos;
			Num = _num++;
		}

		public string Message
		{
			get;
			private set;
		}

        public int Position
		{
			get;
			private set;
		}

		private Int32 Num
		{
			get;
			set;
		}

		public void Print(String program)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("ERROR #{0}", Num);
			Console.ForegroundColor = ConsoleColor.Gray;

			if (Position != -1)
			{
				Console.Write(program.Substring(0, Position));
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write(program[Position]);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine(program.Substring(Position + 1, program.Length - Position - 1));
			}
			else
			{
				Console.Write(program);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("(!)");
				Console.ForegroundColor = ConsoleColor.Gray;
			}

			Console.WriteLine("MESSAGE: {0}", Message);
			Console.WriteLine();
		}

		private static Int32 _num = 0;
	}
}
