using System;

namespace mol
{
    public class OperatorExpression:Expression
    {
        Expression   left;
        string   an_operator;
        Expression   right;

        public OperatorExpression(Expression _left, string _an_operator, Expression _right)
        {
            left = _left;
            an_operator = _an_operator;
            right = _right;
        }

        public Value evaluate(Interpreter interpreter)
        {
            Value left_value = left.evaluate(interpreter);
            Value right_value = right.evaluate(interpreter);
            switch(an_operator)
            {
                case "+":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() + right_value.toNumber());
                    }
                    else
                    {
                        return (Value) new StringValue(left_value.toString() + right_value.toString());
                    }
                case "-":
                    return (Value) new NumberValue(left_value.toNumber() - right_value.toNumber());
                case "*":
                    return (Value) new NumberValue(left_value.toNumber() * right_value.toNumber());
                case "/":
                    return (Value) new NumberValue(left_value.toNumber() / right_value.toNumber());
                case "=":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() == right_value.toNumber() ? 1 : 0);
                    }
                    else
                    {
                        return (Value) new NumberValue(left_value.toString() == right_value.toString() ? 1 : 0);
                    }
                case "<":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() < right_value.toNumber() ? 1 : 0);
                    }
                    else
                    {
                        return (Value) new NumberValue(left_value.toString().CompareTo(right_value.toString()) < 0 ? 1 : 0);
                    }
                case "<=":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() <= right_value.toNumber() ? 1 : 0);
                    }
                    else
                    {
                        return (Value) new NumberValue(left_value.toString().CompareTo(right_value.toString()) <= 0 ? 1 : 0);
                    }
                case ">":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() > right_value.toNumber() ? 1 : 0);
                    }
                    else
                    {
                        return (Value) new NumberValue(left_value.toString().CompareTo(right_value.toString()) > 0 ? 1 : 0);
                    }
                case ">=":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() >= right_value.toNumber() ? 1 : 0);
                    }
                    else
                    {
                        return (Value) new NumberValue(left_value.toString().CompareTo(right_value.toString()) >= 0 ? 1 : 0);
                    }
                case "<>":
                    if(left_value is NumberValue)
                    {
                        return (Value) new NumberValue(left_value.toNumber() != right_value.toNumber() ? 1 : 0);
                    }
                    else
                    {
                        return (Value) new NumberValue(left_value.toString() != right_value.toString() ? 1 : 0);
                    }
                case "&":
                    return (Value)  new NumberValue( ((left_value.toNumber() !=0) && ( right_value.toNumber()) !=0) ? 1 : 0);
                case "|":
                    return (Value) new NumberValue( ((left_value.toNumber() !=0) || ( right_value.toNumber()) !=0) ? 1 : 0);
            }
            throw new Exception("Unknown operator.["+an_operator+"]");
        }
    }
}