using System.Collections;
using System.Collections.Generic;

public class Scratch : Physical
{
    public override string getName() { return "Scratch"; }
    public override string getDescription() { return "The Beautymon attacks with its claws."; }
    public override int getPower() { return 50; }
    public override Type getType() { return new Normal(); }
}
