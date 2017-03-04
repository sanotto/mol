using System;

namespace mol
{
    public class GosubStatement:Statement
    {
        private string label;
        public GosubStatement(string _label)
        {
            label = _label;
        }
    
        public void execute(Interpreter interpreter)
        {
            if(interpreter.TheLexer.Labels.ContainsKey(label))
            {
                interpreter.ReturnStack.Push(interpreter.CurrentStatement);
                interpreter.CurrentStatement = interpreter.TheLexer.Labels[label];
            }
            
        }
    }
}

