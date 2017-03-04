using System;
using System.IO;

namespace mol
{
	class MainClass
	{
	    static int Main ()
		{
            String[] args = Environment.GetCommandLineArgs();
            //Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments));           

			if (args.Length < 2) {
         		Console.WriteLine ("Usage: mol script_file");
				return -1;
			}
			string script_file = args [1];
			if (!File.Exists (script_file)) {
				Console.WriteLine ("Script file:" + script_file + ".Does not exists");
				return -1;
			}
			

            //string script_file = "/home/sanotto/Laboratorio/CSharp/mol/mol/Samples/Test1.mol";
			string source  = File.ReadAllText(script_file);
			Interpreter interpreter = new Interpreter(source);
            return interpreter.Run ();
		}
	}
}
