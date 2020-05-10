using System.Collections;
using System.Collections.Generic;

public class Poison : Type
{
    public override string getName() { return "Poison"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Insect(), new Normal(), new Will() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Air(), new Plant(), new Psychic() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ new Poison() }; }
}
