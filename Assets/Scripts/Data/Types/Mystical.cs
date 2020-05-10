using System.Collections;
using System.Collections.Generic;

public class Mystical : Type
{
    public override string getName() { return "Mystical"; }
    public override List<Type> getResistances() { return new List<Type>(){ new Air(), new Electric(), new Fire(), new Ice(), new Plant(), new Water() }; }
    public override List<Type> getWeaknesses() { return new List<Type>(){ new Mystical(), new Spirit(), new Will() }; }
    public override List<Type> getImmunities() { return new List<Type>(){  }; }
}
