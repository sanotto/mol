using System;

namespace mol
{
    public class PrintStatement:Statement
    {
        Expression an_expression;

        public PrintStatement(Expression _an_expression)
        {
            an_expression = _an_expression;               
        }

        public void execute(Interpreter interpreter)
        {
            Console.WriteLine(an_expression.evaluate(interpreter).toString());
        }
    }
}

