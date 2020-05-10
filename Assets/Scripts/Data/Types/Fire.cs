using System.Collections;
using System.Collections.Generic;

public class Fire : Type
{
    public override string getName() { return "Fire"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Fire(), new Ice(), new Insect(), new Plant(), new Steel() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Air(), new Rock(), new Water() }; }
    public override List<Type> getImmunities() { return new List<Type>(){}; }
}
