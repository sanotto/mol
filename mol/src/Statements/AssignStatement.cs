using System;

namespace mol
{
    public class AssignStatement:Statement
    {
        string name;
        Expression value;

        public AssignStatement(string _name, Expression _value)
        {
            name = _name;
            value = _value;
        }
        
        public void execute(Interpreter interpreter)
        {
            interpreter.ScriptVariables[name] = value.evaluate(interpreter);
        }
    }
}

