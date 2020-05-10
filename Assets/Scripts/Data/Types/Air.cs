using System.Collections;
using System.Collections.Generic;

public class Air : Type
{
    public override string getName() { return "Air"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Fire(), new Plant(), new Poison() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Electric(), new Ice(), new Rock() }; }
    public override List<Type> getImmunities() { return new List<Type>(){}; }
}
