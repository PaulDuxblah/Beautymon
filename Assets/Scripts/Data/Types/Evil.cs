using System.Collections;
using System.Collections.Generic;

public class Evil : Type
{
    public override string getName() { return "Evil"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Evil() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Will() }; }
    public override List<Type> getImmunities() { return new List<Type>(){  }; }
}
