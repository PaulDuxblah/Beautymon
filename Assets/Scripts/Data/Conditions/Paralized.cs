using System.Collections;
using System.Collections.Generic;

public class Paralized : Condition
{
    public override string getName() { return "Paralized"; }
    public override string getDescription() { return "The Beautymon is halved and won't be able to move every 3 turns since he has been paralyzed or sent on the field. Electric Beautymons cannot be paralized."; }
}
