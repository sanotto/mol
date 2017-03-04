using System;

namespace mol
{
    public interface Expression
    {
        Value evaluate(Interpreter interpreter);
    }
}

