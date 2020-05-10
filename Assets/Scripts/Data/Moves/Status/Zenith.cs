using System.Collections;
using System.Collections.Generic;

public class Zenith : Status
{
    public override string getName() { return "Zenith"; }
    public override string getDescription() { return "The climate changes to Sun for 5 turns."; }
    public override Type getType() { return new Fire(); }
    new public bool targetsEnemy = false;
    new public Climate invokeClimate = new Sun();
}
