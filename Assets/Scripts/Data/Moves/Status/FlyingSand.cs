using System.Collections;
using System.Collections.Generic;

public class FlyingSand : Status
{
    public override string getName() { return "Flying Sand"; }
    public override string getDescription() { return "The climate changes to Sandstorm for 5 turns."; }
    public override Type getType() { return new Rock(); }
    new public bool targetsEnemy = false;
    new public Climate invokeClimate = new Sandstorm();
}
