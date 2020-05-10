using System.Collections;
using System.Collections.Generic;

public class Burned : Condition
{
    public override string getName() { return "Burned"; }
    public override string getDescription() { return "The Beautymon loses 1/16 of its maximum HP at the end of each turn, and his physical attacks deal half damages. Fire Beautymons cannot be burned."; }
}
