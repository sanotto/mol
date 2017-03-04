using System;

namespace mol
{
    public class InputStatement:Statement
    {
        string name ;
        public InputStatement(string _name)
        {
            name = _name;
        }

        public void execute(Interpreter interpreter)
        {
            string input = Console.ReadLine();
            Value value;
            try{
                Decimal numeric_value = Convert.ToDecimal(input);
                value = (Value) new NumberValue(numeric_value);
            }   
            catch(Exception)
            {
                        value = (Value) new StringValue(input);
            }
            interpreter.ScriptVariables[name] = value;
        }
    }
}

