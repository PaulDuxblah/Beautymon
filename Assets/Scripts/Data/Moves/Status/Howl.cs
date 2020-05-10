using System.Collections;
using System.Collections.Generic;

public class Howl : Status
{
    public override string getName() { return "Howl"; }
    public override string getDescription() { return "Raises attack by 2 points."; }
    public override Type getType() { return new Normal(); }

    new public bool targetsEnemy = false;
    new public string modifyStat = Beautymon.ATTACK;
    new public int modifyStatModifier = 2;
}
