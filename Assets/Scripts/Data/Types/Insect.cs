using System.Collections;
using System.Collections.Generic;

public class Insect : Type
{
    public override string getName() { return "Insect"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Plant(), new Psychic(), new Spirit(), new Will() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Fire(), new Ice(), new Rock() }; }
    public override List<Type> getImmunities() { return new List<Type>(){  }; }
}
