using System.Collections;
using System.Collections.Generic;

public class Rain : Climate
{
    public override string getName() { return "Rain"; }
    public override string getDescription() { return "Water attacks are half more powerful, and Fire attacks are halved."; }
}
