using System;

namespace mol
{
    public class StringValue:Value
    {
    
        string value;

        public StringValue(string _value)
        {
            value = _value;
        }

        public string toString() 
        { 
            return value; 
        }

        public decimal toNumber() 
        { 
            return Convert.ToDecimal(value);
        }

        public Value evaluate(Interpreter interpreter) 
        { 
            return this; 
        }

    }
}

