using System;

namespace mol
{
    public interface Statement
    {
        void execute(Interpreter interpreter);
    }
}

