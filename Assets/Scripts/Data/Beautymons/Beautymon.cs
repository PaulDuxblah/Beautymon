using System.Collections;
using System.Collections.Generic;

abstract public class Beautymon
{
    public static string MAXHP = "HP";
    public static string ATTACK = "Attack";
    public static string DEFENSE = "Defense";
    public static string SPECIALATTACK = "Special Attack";
    public static string SPECIALDEFENSE = "Special Defense";
    public static string SPEED = "Speed";

    abstract public List<Move> getPossibleMoves();
    public List<Move> moves;

    abstract public List<Talent> getPossibleTalents();
    public Talent talent;

    abstract public List<Type> getTypes();

    abstract public int getMaxHp();
    abstract public int getAttack();
    abstract public int getDefense();
    abstract public int getSpecialAttack();
    abstract public int getSpecialDefense();
    abstract public int getSpeed();

    public int currentHp;
    public Dictionary<string, int> temporaryStatModifiers;

    public Condition condition;
    public Dictionary<string, int> conditionsCounter;

    public bool taunted = false;
    public bool confused = false;
    public bool flashFireBuff = false;

    public int getStat(string statName)
    {
        if (statName == MAXHP) {
            return getMaxHp();
        } else if (statName == ATTACK) {
            return getAttack();
        } else if (statName == DEFENSE) {
            return getDefense();
        } else if (statName == SPECIALATTACK) {
            return getSpecialAttack();
        } else if (statName == SPECIALDEFENSE) {
            return getSpecialDefense();
        } else if (statName == SPEED) {
            return getSpeed();
        }

        return 0;
    }

    public double getStatWithTemporaryModifier(string statName)
    {
        double statValue = getStat(statName);

        if (temporaryStatModifiers[statName] != 0) {
            if (temporaryStatModifiers[statName] > 0) {
                statValue = statValue * (1 + (1/2 * temporaryStatModifiers[statName]));
            } else {
                // I also did it in one line, but it was so unreadable I kept this version :D
                int absoluteValueOfTemporaryStatModifier = System.Math.Abs(temporaryStatModifiers[statName]);
                if (absoluteValueOfTemporaryStatModifier % 2 == 0) {
                    statValue = statValue * (1 / (1 + (absoluteValueOfTemporaryStatModifier / 2))); // 1/2, 1/3, 1/4 for -2, -4, -6
                } else {
                    statValue = statValue * (2 / (2 + absoluteValueOfTemporaryStatModifier)); // 2/3, 2/5, 2/7 for -1, -3, -5
                }
            }
        }
        
        return statValue;
    }

    public int getStatsSum()
    {
        return getMaxHp() + getAttack() + getDefense() + getSpecialAttack() + getSpecialDefense() + getSpeed();
    }

    public bool isKO()
    {
        return currentHp == 0;
    }

    public void changeHP(double hpModifier)
    {
        int finalHpModifier = (int) System.Math.Truncate(hpModifier);

        // Has to be minimum 1 or -1
        if (finalHpModifier == 0) {
            if (hpModifier < 0) {
                finalHpModifier = -1;
            } else {
                finalHpModifier = 1;
            }
        }

        changeHP(finalHpModifier);
    }

    public void changeHP(int hpModifier)
    {
        bool wasFullHP = currentHp == getMaxHp();

        currentHp += hpModifier;
        if (currentHp > getMaxHp()) currentHp = getMaxHp();
        if (currentHp < 0) currentHp = 0;

        // Sturdy
        if (currentHp == 0 && wasFullHP && talent is Sturdy) currentHp = 1;
    }

    public bool canBeAffectedByMove(Move move)
    {
        // Immunity by type
        foreach (Type type in getTypes()) {
            if (type.getImmunities().Contains(move.getType())) return false;
        }

        // Immunity by talent
        if (talent is FlashFire && move.getType() is Fire) {
            flashFireBuff = true;
            return false;
        }
        if (talent is WaterAbsorb && move.getType() is Water) {
            changeHP(getMaxHp() * 0.25);
            return false;
        }

        return true;
    }

    public void resetTemporaryStatsModifiers()
    {
        temporaryStatModifiers = new Dictionary<string, int>() {
            { ATTACK, 0 },
            { DEFENSE, 0 },
            { SPECIALATTACK, 0 },
            { SPECIALDEFENSE, 0 },
            { SPEED, 0 },
        };
    }

    public void resetConditionsCounter()
    {
        conditionsCounter = new Dictionary<string, int>() {
            { new Asleep().getName(), 0 },
            { new BadlyPoisoned().getName(), 0 },
            { new Burned().getName(), 0 },
            { new Frozen().getName(), 0 },
            { new Paralized().getName(), 0 },
            { new Poisoned().getName(), 0 },
        };
    }

    public void switchIn()
    {
        resetTemporaryStatsModifiers();
    }

    public void switchOut()
    {
        confused = false;
        taunted = false;
        flashFireBuff = false;
        resetTemporaryStatsModifiers();
    }

    public void temporaryStatChange(string statName, int modifier)
    {
        temporaryStatModifiers[statName] += modifier;
        if (temporaryStatModifiers[statName] > 6) temporaryStatModifiers[statName] = 6;
        if (temporaryStatModifiers[statName] < -6) temporaryStatModifiers[statName] = -6;
    }

    public bool isOfType(Type wantedType)
    {
        return isOfType(wantedType.getName());
    }

    public bool isOfType(string typeName)
    {
        foreach (Type type in getTypes()) {
            if (type.getName() == typeName) return true;
        }

        return false;
    }

    public bool canBeAsleep()
    {
        return talent is Insomniac;
    }

    public bool canBeBurned()
    {
        return !isOfType(new Fire().getName());
    }

    public bool canBeFrozen()
    {
        return !isOfType(new Ice().getName());
    }

    public bool canBeParalized()
    {
        return !isOfType(new Electric().getName());
    }

    public bool canBePoisoned()
    {
        return !isOfType(new Poison().getName()) && !isOfType(new Steel().getName());
    }

    public bool canBeAffectedByCondition(Condition condition)
    {
        return (
            (condition is Asleep && !canBeAsleep())
            || (condition is Burned && !canBeBurned())
            || (condition is Frozen && !canBeFrozen())
            || (condition is Paralized && !canBeParalized())
            || ((condition is Poisoned || condition is BadlyPoisoned) && !canBePoisoned())
        );
    }

    public void changeConditionCounter(Condition condition, int modifier, bool force = false)
    {
        if (!canBeAffectedByCondition(condition)) return;

        if (condition != null && !force) return;

        conditionsCounter[condition.getName()] += modifier;

        if (conditionsCounter[condition.getName()] >= 100) {
            setCondition(condition, force);
            resetConditionsCounter();
        }
    }

    public void setCondition(Condition incomingCondition, bool force = false)
    {
        if (condition != null && !force) return;

        if (!canBeAffectedByCondition(incomingCondition)) return;

        condition = incomingCondition;
    }
}
