using System.Collections;
using System.Collections.Generic;

public class Water : Type
{
    public override string getName() { return "Water"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Fire(), new Ice(), new Rock(), new Steel(), new Water() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Electric(), new Plant(), new Poison() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ }; }
}
