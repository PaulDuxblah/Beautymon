using System.Collections;
using System.Collections.Generic;

public class Hail : Climate
{
    public override string getName() { return "Hail"; }
    public override string getDescription() { return "Non-Ice Beautymons lose 1/16 of their maximum HP at the end of each turn. Ice moves deals half more damages."; }
}
