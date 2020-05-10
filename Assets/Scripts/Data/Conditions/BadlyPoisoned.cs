using System.Collections;
using System.Collections.Generic;

public class BadlyPoisoned : Condition
{
    public override string getName() { return "Badly Poisoned"; }
    public override string getDescription() { return "The Beautymon loses 1/16 of its maximum life at the end of each turn, and an additionnal 1/16 for each turn he has been on the field. Steel and Poison Beautymons cannot be badly poisoned."; }
}
