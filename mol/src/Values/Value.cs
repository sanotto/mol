using System;

namespace mol
{
    public interface Value:Expression
    {
        string  toString();
        decimal toNumber();
    }
}

