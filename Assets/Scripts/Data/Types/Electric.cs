using System.Collections;
using System.Collections.Generic;

public class Electric : Type
{
    public override string getName() { return "Electric"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Electric(), new Air(), new Steel() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Rock() }; }
    public override List<Type> getImmunities() { return new List<Type>(){}; }
}
