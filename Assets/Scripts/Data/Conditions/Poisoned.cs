using System.Collections;
using System.Collections.Generic;

public class Poisoned : Condition
{
    public override string getName() { return "Poisoned"; }
    public override string getDescription() { return "The Beautymon loses 1/8 of its maximum life at the end of each turn. Steel and Poison Beautymons cannot be poisoned."; }
}
