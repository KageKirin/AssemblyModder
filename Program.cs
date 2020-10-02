using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.IO;
using System.CommandLine.DragonFruit;
using Mono.Cecil;

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
		/// <param name="internalsVisibleToAssembly">Assembly to make internals visible to using "[InternalsVisibleTo]"</param>
		static void Main(FileInfo inputAssembly, FileInfo outputAssembly, string symbol, string internalsVisibleToAssembly)
		{
			Console.WriteLine($"{inputAssembly} -> publish {symbol} -> {outputAssembly}");

			ModuleDefinition module = ModuleDefinition.ReadModule(inputAssembly.FullName, new ReaderParameters());
			foreach (TypeDefinition type in module.Types)
			{
				Console.WriteLine($"{type.FullName} ({type.Attributes})");

				if (type.FullName == symbol)
				{
					Console.WriteLine($"overwriting {type.FullName} ({type.Attributes})");
					//type.IsNotPublic = false;
					//type.IsPublic = true;

					var customAttr = new CustomAttribute(
						module.Import(
							typeof(InternalsVisibleToAttribute).GetConstructor(
								new[] {typeof(string)}
							)
						)
					);

					customAttr.ConstructorArguments.Add(new CustomAttributeArgument(module.TypeSystem.String, internalsVisibleToAssembly));
					module.Assembly.CustomAttributes.Add(customAttr);
					Console.WriteLine($"{type.FullName} ({type.Attributes})");
				}
			}

			module.Write(outputAssembly.FullName, new WriterParameters());
		}
	}
}
