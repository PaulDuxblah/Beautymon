using System.Collections;
using System.Collections.Generic;

public class RainDance : Status
{
    public override string getName() { return "Rain Dance"; }
    public override string getDescription() { return "The climate changes to Rain for 5 turns."; }
    public override Type getType() { return new Water(); }
    new public bool targetsEnemy = false;
    new public Climate invokeClimate = new Rain();
}
