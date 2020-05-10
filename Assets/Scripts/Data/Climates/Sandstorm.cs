using System.Collections;
using System.Collections.Generic;

public class Sandstorm : Climate
{
    public override string getName() { return "Sandstorm"; }
    public override string getDescription() { return "Non-Rock and non-Steel Beautymons lose 1/16 of their maximum HP at the end of each turn. Special Air attacks become Rock-type, Special Rock attacks are half more powerful, while Fire attacks are halved."; }
}
