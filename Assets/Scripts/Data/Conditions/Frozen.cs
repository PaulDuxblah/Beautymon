using System.Collections;
using System.Collections.Generic;

public class Frozen : Condition
{
    public override string getName() { return "Frozen"; }
    public override string getDescription() { return "The Beautymon cannot do anything (for 3 turns on battle since the turn he has been frozen by default) until he thaws. Taking damages from a fire attack thaws instantly. Ice Beautymons cannot be frozen."; }
}
