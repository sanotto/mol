using System;

namespace mol
{
    public class VariableExpression:Expression
    {
        string var_name;

        public VariableExpression(string _var_name)
        {
            var_name=_var_name;
        }

        public Value evaluate(Interpreter interpreter)
        {
            if(interpreter.ScriptVariables.ContainsKey(var_name))
            {
                return  interpreter.ScriptVariables[var_name];
            }
            return new NumberValue(0);
        }
    }
}

