using System;

namespace mol
{
    public class EndpgmStatement:Statement
    {
        public EndpgmStatement()
        {
        }

        public void execute(Interpreter interpreter)
        {
            interpreter.Aborted = true;
        }
    }
}

