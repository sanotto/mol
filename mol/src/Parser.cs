using System;
using System.Collections.Generic;

namespace mol
{
	public class Parser
	{
		private enum ParserStates {	DEFAULT,
				    	    		WORD,
                                    NUMBER,
        							STRING,
        							COMMENT,
        							OPERATOR};
		
		

		private string char_tokens="\n=+-*/<>&|(),";

		private TokenTypes[] char_tokens_types={TokenTypes.LINE, 
        										TokenTypes.EQUALS,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.OPERATOR,
        										TokenTypes.LEFT_PAREN,
        										TokenTypes.RIGHT_PAREN,
                                                TokenTypes.COLON};

        private string[] built_in_functions =   {   "sqr",
                                                    "sin",
                                                    "cos",
                                                    "int",
                                                    "round",
                                                    "trim",
                                                    "ltrim",
                                                    "rtrim",
                                                    "subst"};

		private string source;

		public Parser (string _source)
		{
			source = _source;
		}

		public List<Token> GetTokenList()
        {
            List<Token> tokens = new List<Token>();

            ParserStates state = ParserStates.DEFAULT;
            string current_token = "";
            int source_lenght = source.Length;
            int line_number =1;
            for (int i = 0; i < source_lenght; i++)
            {
                char current_char = source[i];
                char next_char = ' ';
                int next_char_pointer = i + 1;
                if (next_char_pointer < source_lenght)
                {
                    next_char = source[next_char_pointer];
                }

                if (current_char.Equals('\n'))
                    line_number++;

                switch (state)
                {
                    case ParserStates.DEFAULT:
                        if (current_char.Equals('-') && Char.IsDigit(next_char))
                        {
                            current_token += current_char;
                            state = ParserStates.NUMBER;
                            break;
                        }
                        int index_of = char_tokens.IndexOf(current_char);
                        if (index_of >= 0)
                        {
                            string oper = current_char.ToString() ;
                            string two_chars_operator = current_char.ToString() + next_char.ToString();
                           
                            if (two_chars_operator.Equals("<>") ||
                                two_chars_operator.Equals("<=") ||
                                two_chars_operator.Equals(">="))
                            {
                                oper = two_chars_operator;
                                char[] chars = source.ToCharArray();
                                chars[next_char_pointer] = ' ';
                                source = new string(chars);
                            }
                            tokens.Add(new Token(oper, char_tokens_types[index_of]));
                        }
                        else if (Char.IsLetter(current_char))
                        {
                            current_token += current_char;
                            state = ParserStates.WORD;
                        }
                        else if (   (Char.IsDigit(current_char)) || 
                                    ((current_char.Equals('-')) && (Char.IsDigit(next_char) ))
                                )
                        {
                            current_token += current_char;
                            state = ParserStates.NUMBER;
                        }
                        else if (current_char.Equals('"'))
                        {
                            state = ParserStates.STRING;
                        }
                        else if (current_char.Equals('\''))
                        {
                            state = ParserStates.COMMENT;
                        }
                        else if (current_char.Equals('#'))
                        {
                            state = ParserStates.COMMENT;
                        }

                        break;                
                    case ParserStates.WORD:
                        if (Char.IsLetterOrDigit(current_char))
                        {
                            current_token += current_char;
                        }
                        else if (current_char.Equals(":"))
                        {
                            tokens.Add(new Token(current_token, TokenTypes.LABEL));
                            current_token = "";
                            state = ParserStates.DEFAULT;
                            i--;
                        }
                        else if (Array.IndexOf(built_in_functions, current_token) >= 0)
                        {
                            tokens.Add(new Token(current_token, TokenTypes.FNCTION));
                            current_token = "";
                            state = ParserStates.DEFAULT;
                            i--;
                        }
                        else
                        {
                            tokens.Add(new Token(current_token, TokenTypes.WORD));
                            current_token = "";
                            state = ParserStates.DEFAULT;
                            i--;
                        }
                        break;
                    case ParserStates.NUMBER:
                        if ((Char.IsDigit(current_char)) || (current_char.Equals('.')))
                        {
                            if ( (current_char.Equals('.')) && (current_token.IndexOf('.')>=0))
                            {
                                    throw new Exception("Bad Formed Number:[" + current_token + "] at line:"+line_number);
                            }
                            if ( (current_char.Equals(".")) && (!Char.IsDigit(next_char)))
                            {
                                throw new Exception("Bad Formed Number:[" + current_token + "] at line:" + line_number);
                            }

                            current_token += current_char;
                        }
                        else
                        {
                            tokens.Add(new Token(current_token, TokenTypes.NUMBER));
                            current_token = "";
                            state = ParserStates.DEFAULT;
                            i--;
                        }
                        break;
                    case ParserStates.STRING:
                        if (current_char.Equals('"'))
                        {
                            tokens.Add(new Token(current_token, TokenTypes.STRING));
                            current_token = "";
                            state = ParserStates.DEFAULT;
                            //i--;
                        }
                        else if(current_char.Equals('\n'))
                        {
                            throw new Exception("Unterminated string:[" + current_token.Trim() + "...] at line:" + line_number);
                        }
                        else
                        {
                            current_token += current_char;
                        }
                        break;
                    case ParserStates.COMMENT:  
                        if (current_char.Equals('\n'))
                        {
                            state = ParserStates.DEFAULT;
                        }
                        break;
        	    }
        	}

			return tokens;
		}
	}
}

