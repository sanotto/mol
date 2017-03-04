using System;

namespace mol
{
    public class UnaryOperatorExpression:Expression
    {
        string an_operator;
        Expression right;
        public UnaryOperatorExpression(string _an_operator, Expression _right)
        {
            an_operator = _an_operator;
            right = _right;
        }
        
        public Value evaluate(Interpreter interpreter)
        {
            Value right_value = right.evaluate(interpreter);
            if(an_operator.Equals("sqr"))
            {
                return new NumberValue(Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(right_value.toNumber()))));
            }
            else if (an_operator.Equals("sin"))
            {
                return new NumberValue(Convert.ToDecimal(Math.Sin (Convert.ToDouble(right_value.toNumber()))));
            }
            else if (an_operator.Equals("cos"))
            {
                return new NumberValue(Convert.ToDecimal(Math.Cos(Convert.ToDouble(right_value.toNumber()))));
            }
            else if (an_operator.Equals("int"))
            {
                return new NumberValue(Convert.ToDecimal(Math.Truncate(Convert.ToDouble(right_value.toNumber()))));
            }
            else if (an_operator.Equals("round"))
            {
                return new NumberValue(Convert.ToDecimal(Math.Round(Convert.ToDouble(right_value.toNumber()))));
            }
            else if (an_operator.Equals("trim"))
            {
                return new StringValue(right_value.toString().Trim());
            }
            else if (an_operator.Equals("ltrim"))
            {
                return new StringValue(right_value.toString().TrimStart());
            }
            else if (an_operator.Equals("rtrim"))
            {
                return new StringValue(right_value.toString().TrimEnd());
            }
            throw new Exception("Unknown operator:" + an_operator);
        }
    }
}

