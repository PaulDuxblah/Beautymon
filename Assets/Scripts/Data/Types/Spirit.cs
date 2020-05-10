using System.Collections;
using System.Collections.Generic;

public class Spirit : Type
{
    public override string getName() { return "Spirit"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Mystical(), new Spirit() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Evil(), new Insect(), new Psychic(), new Will() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ new Normal() }; }
}
