using System.Collections;
using System.Collections.Generic;

abstract public class Type
{
    abstract public string getName();
    abstract public List<Type> getResistances();
    abstract public List<Type> getWeaknesses();
    abstract public List<Type> getImmunities();


    public bool isResistantAgainst(Type type)
    {
        return getResistances().Contains(type);
    }

    public bool IsWeakAgainst(Type type)
    {
        return getWeaknesses().Contains(type);
    }

    public bool IsImmunedAgainst(Type type)
    {
        return getImmunities().Contains(type);
    }
}
