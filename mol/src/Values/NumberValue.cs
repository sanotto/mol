using System;

namespace mol
{
    public class NumberValue:Value
    {
        decimal value;
        public NumberValue(decimal _value)
        {
            value = _value;
        }

        public string toString() 
        { 
            return Convert.ToString(value); 
        }

        public decimal toNumber() 
        { 
            return value;
        }

        public Value evaluate(Interpreter interpreter) 
        { 
            return this; 
        }

    }
}

