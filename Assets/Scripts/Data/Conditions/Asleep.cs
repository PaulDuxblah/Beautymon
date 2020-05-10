using System.Collections;
using System.Collections.Generic;

public class Asleep : Condition
{
    public override string getName() { return "Asleep"; }
    public override string getDescription() { return "The Beautymon cannot do anything (for 3 turns on battle since the turn he has been asleep by default) until he wakes up."; }
}
