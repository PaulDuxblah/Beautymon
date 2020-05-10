using System.Collections;
using System.Collections.Generic;

abstract public class Move
{
    abstract public string getName();
    abstract public string getDescription();
    abstract public Type getType();

    public bool targetsEnemy = true;

    public Condition inflictCondition = null;
    public int inflictConditionModifier = 0;
    public bool inflictConditionForce = false;

    public string modifyStat = null;
    public int modifyStatModifier = 0;

    public Climate invokeClimate = null;
}
