using System.Collections;
using System.Collections.Generic;

abstract public class Offensive : Move
{
    public bool makesContact = false;
    // If this is true, the physical attack will use the defender special defense, and vice versa
    public bool attackOnTheOtherStat = false;
    abstract public int getPower();
}
