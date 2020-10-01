using System;
using System.IO;
using System.CommandLine.DragonFruit;

namespace AssemblyModder
{
	class Program
	{
		/// <summary>
		/// Modifies symbol access in given assembly of given symbols to public
		/// </summary>
		/// <param name="inputAssembly">Assembly to modify</param>
		/// <param name="outputAssembly">Assembly to save</param>
		/// <param name="symbol">Symbol to make public</param>
		static void Main(FileInfo inputAssembly, FileInfo outputAssembly, string symbol)
		{
			Console.WriteLine($"{inputAssembly} -> publish {symbol} -> {outputAssembly}");
		}
	}
}
