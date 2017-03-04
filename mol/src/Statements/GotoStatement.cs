using System;

namespace mol
{
    public class GotoStatement:Statement
    {
        string label;
        public GotoStatement(string _label)
        {
            label = _label;
        }
        
        public void execute(Interpreter interpreter)
        {
            if(interpreter.TheLexer.Labels.ContainsKey(label))
            {
                interpreter.CurrentStatement = interpreter.TheLexer.Labels[label];
            }
        }
    }
}

