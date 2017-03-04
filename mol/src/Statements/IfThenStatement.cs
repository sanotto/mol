using System;

namespace mol
{
    public class IfThenStatement:Statement
    {
        Expression condition;
        string  label;

        public IfThenStatement(Expression _condition, string _label)
        {
            label = _label;
            condition = _condition;
        }

        public void execute(Interpreter interpreter)
        {
            if(interpreter.TheLexer.Labels.ContainsKey(label))
            {
                Value value = condition.evaluate(interpreter);
                if(value.toNumber() != 0)
                {
                    interpreter.CurrentStatement = interpreter.TheLexer.Labels[label];
                }
            }
        }
    }
}

