using System.Collections;
using System.Collections.Generic;

public class Psychic : Type
{
    public override string getName() { return "Psychic"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Poison(), new Psychic() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Evil(), new Insect(), new Spirit() }; }
    public override List<Type> getImmunities() { return new List<Type>(){  }; }
}
