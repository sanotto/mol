using System;

namespace mol
{
    public class ThriceOperatorExpression:Expression
    {
        string an_operator;
        Expression ex_string;
        Expression ex_from;
        Expression ex_lenght;

        public ThriceOperatorExpression(string _an_operator, Expression _ex_string, Expression _ex_from, Expression _ex_lenght)
        {
            an_operator = _an_operator;
            ex_string=_ex_string;
            ex_from=_ex_from;
            ex_lenght=_ex_lenght;
        }
        
        public Value evaluate(Interpreter interpreter)
        {
            Value string_value = ex_string.evaluate(interpreter);
            Value from_value = ex_from.evaluate(interpreter);
            Value len_value = ex_lenght.evaluate(interpreter);
            if(an_operator.Equals("subst"))
            {
                int frm = Convert.ToInt16(from_value.toNumber());
                int len = Convert.ToInt16(len_value.toNumber());
                return new StringValue(string_value.toString().Substring(frm,len)   );
            }
            throw new Exception("Unknown operator:" + an_operator);
        }
    }
}

