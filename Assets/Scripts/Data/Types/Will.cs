using System.Collections;
using System.Collections.Generic;

public class Will : Type
{
    public override string getName() { return "Will"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Evil(), new Mystical(), new Rock(), new Steel(), new Will() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Insect(), new Poison(), new Spirit() }; }
    public override List<Type> getImmunities() { return new List<Type>(){ }; }
}
