using System;

namespace mol
{
    public class ReturnStatement:Statement
    {
        public ReturnStatement()
        {
        }
    
        public void execute(Interpreter interpreter)
        {
            interpreter.CurrentStatement = interpreter.ReturnStack.Pop();
        }
    }
}

