using System;
using System.Collections.Generic;

namespace mol
{
	public class Interpreter
	{
		public  List<Token> TokensList;
        public  Dictionary<string, Value> ScriptVariables;
        private Parser  parser;
        public  Lexer   TheLexer;
        public  int CurrentStatement;
        public  Stack<int> ReturnStack;
        public  bool    Aborted;

		public Interpreter (string _source)
		{
			parser = new Parser (_source);
			TokensList = parser.GetTokenList();
            TheLexer = new Lexer(TokensList);
            ReturnStack = new Stack<int>();
            Aborted = false;
            ScriptVariables = new Dictionary<string,Value>();
		}

        
		public int Run()
		{
            try {
                CurrentStatement = 0;
                Statement[] statements = TheLexer.Statements.ToArray();
                while((!Aborted) && (CurrentStatement <= (statements.Length -1)))
                {
                    int this_statement = CurrentStatement;
                    CurrentStatement++;
                    statements[this_statement].execute(this);
                }
    			return 0;
            } catch (Exception e)
            {
                Console.WriteLine("Error Found:" + e.ToString());
                return -1;
            }
		}


	}
}

