using System.Collections;
using System.Collections.Generic;

public class ObstinateStrike : Physical
{
    public override string getName() { return "Obstinate Strike"; }
    public override string getDescription() { return "As long as the Beautymon is on the field and doesn't get Asleep, Paralyzed or Frozen, and keeps attacking with this move, this move gets half more powerful at each use."; }
    public override int getPower() { return 40; }
    public override Type getType() { return new Will(); }
}
