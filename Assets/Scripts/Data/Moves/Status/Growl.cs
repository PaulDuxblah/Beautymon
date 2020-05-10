using System.Collections;
using System.Collections.Generic;

public class Growl : Status
{
    public override string getName() { return "Growl"; }
    public override string getDescription() { return "Lowers the enemy attack by 2 points."; }
    public override Type getType() { return new Normal(); }

    new public string modifyStat = Beautymon.ATTACK;
    new public int modifyStatModifier = -2;
}
