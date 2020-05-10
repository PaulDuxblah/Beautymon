using System.Collections;
using System.Collections.Generic;

public class Ember : Special
{
    public override string getName() { return "Ember"; }
    public override string getDescription() { return "The Beautymon attacks with small flames. Using it twice in a row will cause the target to burn."; }
    public override int getPower() { return 40; }
    public override Type getType() { return new Fire(); }

    new public Condition inflictCondition = new Burned();
    new public int inflictConditionModifier = 50;
}
