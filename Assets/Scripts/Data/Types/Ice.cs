using System.Collections;
using System.Collections.Generic;

public class Ice : Type
{
    public override string getName() { return "Ice"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Air(), new Ice(), new Plant() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Air(), new Insect(), new Plant(), new Rock() }; }
    public override List<Type> getImmunities() { return new List<Type>(){}; }
}
