using System.Collections;
using System.Collections.Generic;

public class Normal : Type
{
    public override string getName() { return "Normal"; }
    public override List<Type> getResistances() { return new List<Type>(){  }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Poison() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ new Will() }; }
}
