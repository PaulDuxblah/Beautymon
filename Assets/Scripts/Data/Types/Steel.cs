using System.Collections;
using System.Collections.Generic;

public class Steel : Type
{
    public override string getName() { return "Steel"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Air(), new Ice(), new Insect(), new Normal(), new Plant(), new Rock(), new Steel() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Fire(), new Will() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ new Poison() }; }
}
