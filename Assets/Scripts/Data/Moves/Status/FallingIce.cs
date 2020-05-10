using System.Collections;
using System.Collections.Generic;

public class FallingIce : Status
{
    public override string getName() { return "Falling Ice"; }
    public override string getDescription() { return "The climate changes to Hail for 5 turns."; }
    public override Type getType() { return new Ice(); }
    new public bool targetsEnemy = false;
    new public Climate invokeClimate = new Hail();
}
