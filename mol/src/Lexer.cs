using System;
using System.Collections.Generic;

namespace mol
{
    public class Lexer
    {
        private List<Token> tokens;
        public  Dictionary <string, int> Labels;
        public  List<Statement> Statements;
        private int position;
        private int if_label_counter;

        public Lexer( List<Token> _tokens)
        {
            tokens = _tokens;
            Labels = new Dictionary<string,int>();
            Statements = new List<Statement>();
            position = 0;
            if_label_counter = 0;
            do_syntactic_analysis();
            /*
            
            List<string> ir = new List<string>();
            foreach(Statement s in Statements)
            {
                ir.Add(s.ToString());
            }
            string[] irarray = ir.ToArray();
            foreach(KeyValuePair<string, int> entry in Labels)
            {
                irarray[entry.Value] = irarray[entry.Value] + ":" + entry.Key;
            }
            */
            
        }
        
        private void do_syntactic_analysis()
        {
            int current_line = 0;
            while (true)
            {
                while (matchType(TokenTypes.LINE))
                {
                    current_line++;
                }

                if(matchType(TokenTypes.LABEL))
                {
                    Labels.Add(get_prev_token(1).text, Statements.Count);
                }
                else if (match(TokenTypes.WORD,TokenTypes.EQUALS))
                {
                    string name = get_prev_token(2).text;
                    Expression expression = get_an_expression();
                    Statements.Add((Statement)new AssignStatement(name, expression));
                }
                else if (matchName("print"))
                {
                    Statements.Add((Statement)new PrintStatement(get_an_expression()));
                }
                else if (matchName("input"))
                {
                    Statements.Add((Statement)new InputStatement(consumeType(TokenTypes.WORD).ToString()));
                }
                else if (matchName("goto"))
                {
                    Statements.Add((Statement)new GotoStatement(consumeType(TokenTypes.WORD).toString()));
                }
                else if (matchName("gosub"))
                {
                    Statements.Add((Statement)new GosubStatement(consumeType(TokenTypes.WORD).toString()));
                }                
                else if (matchName("exsr"))
                {
                    Statements.Add((Statement)new GosubStatement(consumeType(TokenTypes.WORD).toString()));
                }              
                else if (matchName("begsr"))
                {
                    string label = consumeType(TokenTypes.WORD).toString();
                    Statements.Add((Statement)new EndpgmStatement());
                    Labels[label] =Statements.Count;
                }
                else if (matchName("return"))
                {
                    Statements.Add((Statement)new ReturnStatement());
                }
                else if (matchName("endsr"))
                {
                    Statements.Add((Statement)new ReturnStatement());
                }
                else if (matchName("endpgm"))
                {
                    Statements.Add((Statement)new EndpgmStatement());
                }
                else if (matchName("if"))
                {
                    if_label_counter++;
                    string label_trailer = Convert.ToString(if_label_counter);
                    string if_label = "lbl_if_" + label_trailer;
                    string else_label = "lbl_else_" + label_trailer;                   
                    Expression condition = get_an_expression();                    
                    Statements.Add((Statement)new IfThenStatement(condition, if_label));
                    Statements.Add((Statement)new GotoStatement(else_label));
                    Labels.Add(if_label, Statements.Count);
                }
                else if (matchName("else"))
                {                    
                    string label_trailer = Convert.ToString(if_label_counter);
                    string endif_label = "lbl_endif_" + label_trailer;
                    string else_label = "lbl_else_" + label_trailer;                                      
                    Statements.Add((Statement)new GotoStatement(endif_label));
                    Labels.Add(else_label, Statements.Count);
                }
                else if (matchName("endif"))
                {                    
                    string label_trailer = Convert.ToString(if_label_counter);
                    string endif_label = "lbl_endif_" + label_trailer;
                    Labels.Add(endif_label, Statements.Count);
                }
                else if (matchName("dowhile"))
                {
                    if_label_counter++;
                    string label_trailer = Convert.ToString(if_label_counter);
                    string do_again_label = "lbl_do_again_" + label_trailer;
                    Labels.Add(do_again_label, Statements.Count);
                    string do_label = "lbl_do_" + label_trailer;                   
                    string enddo_label = "lbl_enddo_"+label_trailer;
                    Expression condition = get_an_expression();                    
                    Statements.Add((Statement)new IfThenStatement(condition, do_label));
                    Statements.Add((Statement)new GotoStatement(enddo_label));
                    Labels.Add(do_label, Statements.Count);
                }
                else if (matchName("enddo"))
                {                    
                    string label_trailer = Convert.ToString(if_label_counter);
                    string do_again_label = "lbl_do_again_" + label_trailer;
                    Statements.Add((Statement)new GotoStatement(do_again_label));
                    string enddo_label = "lbl_enddo_"+label_trailer;
                    Labels.Add(enddo_label, Statements.Count);
                }
                else if(matchType(TokenTypes.EOF))
                {
                    break;
                }
                else 
                {
                    throw new Exception("Unrecognized statement: " + get_next_token(0).text);
                };
            }
        }


        private Token get_next_token(int offset)
        {
            int absolut_position = position + offset;
            if ((absolut_position) >= tokens.Count)
            {
                return new Token("", TokenTypes.EOF);
            }
            return tokens[absolut_position];
        }

        private Token get_prev_token(int offset)
        {
            int absolut_position = position - offset;
            return tokens[absolut_position];
        }

        private bool match(TokenTypes typeOne, TokenTypes typeTwo)
        {
            Token token1 = get_next_token(0);
            Token token2 = get_next_token(1);
            if (token1.type != typeOne) return false;
            if (token2.type != typeTwo) return false;
            position += 2;
            return true;
        }

        private bool matchName(string name)
        {
            Token token = get_next_token(0);

            if (token.type != TokenTypes.WORD) return false;
            if (!name.Equals(token.text)) return false;
            position++;
            return true;
        }

        private bool matchType(TokenTypes type)
        {
            Token token = get_next_token(0);
            if (token.type != type) return false;
            position++;
            return true;
        }


        private Token consumeName(string name)
        {
            if (!matchName(name))
            {
                throw new Exception("Expected :"+name);
            }
            return get_prev_token(1);
        }
    
        private Token consumeType(TokenTypes type)
        {
            Token token = get_next_token(0);
            if (token.type != type)
            {
                throw new Exception("Expected Type"+type);
            }
            return tokens[position++];
        }

        private Expression get_an_expression()
        {
            return language_operator();
        }

        private Expression language_operator()
        {
            Expression a_expression = atomic();

            while ( (matchType(TokenTypes.OPERATOR)) || (matchType(TokenTypes.EQUALS)))
            {
    
                string this_operator = get_prev_token(1).text;
                Expression right = atomic();
                a_expression = (Expression) new OperatorExpression(a_expression, this_operator, right);
            }
            return a_expression;
        }

        private Expression atomic()
        {
            //echo("atomic: get(0): ".print_r($this->get(0),true));
            if (matchType(TokenTypes.WORD))
            {
                //echo("Found Variable Expression\n");
                return  (Expression)new VariableExpression(get_prev_token(1).text);
            }
            else if (matchType(TokenTypes.FNCTION))
            {
                //echo("Found Function Expression\n");
                string an_operator = get_prev_token(1).text;
                if (!an_operator.Equals("subst"))
                {
                    consumeType(TokenTypes.LEFT_PAREN);
                    Expression expression = get_an_expression();
                    consumeType(TokenTypes.RIGHT_PAREN);
                    return (Expression) new UnaryOperatorExpression(an_operator, expression);
                }
                else
                {
                    consumeType(TokenTypes.LEFT_PAREN);
                    Expression expression_string = get_an_expression();
                    consumeType(TokenTypes.COLON);
                    Expression expression_from = get_an_expression();
                    consumeType(TokenTypes.COLON);
                    Expression expression_leght = get_an_expression();
                    consumeType(TokenTypes.RIGHT_PAREN);
                    return (Expression) new ThriceOperatorExpression(an_operator, expression_string, expression_from, expression_leght);
                }
            }
            else if (matchType(TokenTypes.NUMBER))
            {
                //echo("Found Number\n");
                return (Expression) new NumberValue(Convert.ToDecimal(get_prev_token(1).text));
            }
            else if (matchType(TokenTypes.STRING))
            {
                //echo("Found String Expression\n");
                return (Expression)new StringValue(get_prev_token(1).text);
            }
            else if (matchType(TokenTypes.LEFT_PAREN))
            {
                //echo("Found Parentheses Expression\n");
                Expression a_expression =get_an_expression();
                consumeType(TokenTypes.RIGHT_PAREN);
                return a_expression;
            }
            throw new Exception("Couldn't parse :( ");
        }
    }
}

