using System.Collections;
using System.Collections.Generic;

public class Rock : Type
{
    public override string getName() { return "Rock"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Air(), new Fire(), new Insect(), new Normal(), new Poison(), new Rock() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Ice(), new Plant(), new Steel(), new Water(), new Will() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ new Electric() }; }
}
