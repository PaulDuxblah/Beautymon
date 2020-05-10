using System.Collections;
using System.Collections.Generic;

public class Plant : Type
{
    public override string getName() { return "Plant"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Electric(), new Rock(), new Water(), new Will() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Air(), new Fire(), new Ice(), new Insect(), new Poison() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ }; }
}
