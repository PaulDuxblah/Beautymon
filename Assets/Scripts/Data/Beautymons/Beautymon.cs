using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Beautymon
{
    public static string MAXHP = "HP";
    public static string ATTACK = "Attack";
    public static string DEFENSE = "Defense";
    public static string SPECIALATTACK = "Special Attack";
    public static string SPECIALDEFENSE = "Special Defense";
    public static string SPEED = "Speed";

    public string name;
    public List<string> possibleMoves = new List<string>();
    public List<Move> moves = new List<Move>();
    public List<string> possibleTalents = new List<string>();
    public Talent talent;
    public List<Type> types = new List<Type>();
    public int maxHp;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public int currentHp;
    public Dictionary<string, int> temporaryStatModifiers;
    public Condition condition;
    public Dictionary<string, int> conditionsCounter;
    public bool taunted = false;
    public bool confused = false;
    public bool flashFireBuff = false;

    public static Beautymon load(string beautymonName)
    {
        StreamReader reader = new StreamReader("Assets\\Scripts\\Data\\Beautymons\\" + beautymonName + ".json");
        string json = reader.ReadToEnd();
        reader.Close();
        
        Beautymon beautymon = JsonUtility.FromJson<Beautymon>(json);

        json = json.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("  ", "");
        string[] jsonArray = json.Split('\n');

        foreach (string line in jsonArray) {
            string editedLine = Utility.cleanJson(line);

            if (editedLine.Contains("possibleTalents: ")) {
                editedLine = editedLine.Replace("possibleTalents: [", "").Replace("]", "").Replace("\"", "");
                string[] editedLineArray = editedLine.Split(',');
                foreach (string talent in editedLineArray) {
                    beautymon.possibleTalents.Add(talent.Trim());
                }
            } else if (editedLine.Contains("possibleMoves: ")) {
                editedLine = editedLine.Replace("possibleMoves: [", "").Replace("]", "").Replace("\"", "");
                string[] editedLineArray = editedLine.Split(',');
                foreach (string move in editedLineArray) {
                    beautymon.possibleMoves.Add(move.Trim());
                }
            } else if (editedLine.Contains("types: ")) {
                editedLine = editedLine.Replace("types: [", "").Replace("]", "").Replace("\"", "");
                string[] editedLineArray = editedLine.Split(',');
                foreach (string type in editedLineArray) {
                    if (type.Trim().Length == 0) continue;
                    beautymon.types.Add(Type.load(type.Trim()));
                }
            }
        }

        beautymon.resetConditionsCounter();
        beautymon.resetTemporaryStatsModifiers();
        beautymon.currentHp = beautymon.maxHp;

        return beautymon;
    }

    public void setMoves(List<string> newMoves)
    {
        List<Move> oldMoves = moves;
        foreach (string move in newMoves) {
            if (!canLearnMove(move)) {
                moves = oldMoves;
                throw new System.Exception("Incorrect move!: " + move);
            }
            moves.Add(Move.load(move));
        }
    }

    public void setMoves(List<Move> newMoves)
    {
        List<string> newMovesNames = new List<string>();
        foreach (Move move in newMoves) {
            newMovesNames.Add(move.name);
        }
        setMoves(newMovesNames);
    }

    public void setTalent(Talent newTalent)
    {
        setTalent(newTalent.name);
    }

    public void setTalent(string newTalent)
    {
        if (!canHaveTalent(newTalent)) throw new System.Exception("Incorrect talent!: " + newTalent);
        talent = Talent.load(newTalent);
    }

    public int getStat(string statName)
    {
        if (statName == MAXHP) {
            return maxHp;
        } else if (statName == ATTACK) {
            return attack;
        } else if (statName == DEFENSE) {
            return defense;
        } else if (statName == SPECIALATTACK) {
            return specialAttack;
        } else if (statName == SPECIALDEFENSE) {
            return specialDefense;
        } else if (statName == SPEED) {
            return speed;
        }

        return 0;
    }

    public double getStatWithTemporaryModifier(string statName)
    {
        double statValue = getStat(statName);

        if (temporaryStatModifiers[statName] != 0) {
            if (temporaryStatModifiers[statName] > 0) {
                statValue = statValue * ((double) 1 + (1/2 * temporaryStatModifiers[statName]));
            } else {
                // I also did it in one line, but it was so unreadable I kept this version :D
                int absoluteValueOfTemporaryStatModifier = System.Math.Abs(temporaryStatModifiers[statName]);
                if (absoluteValueOfTemporaryStatModifier % 2 == 0) {
                    statValue = statValue * ((double) 1 / (1 + (absoluteValueOfTemporaryStatModifier / 2))); // 1/2, 1/3, 1/4 for -2, -4, -6
                } else {
                    statValue = statValue * ((double) 2 / (2 + absoluteValueOfTemporaryStatModifier)); // 2/3, 2/5, 2/7 for -1, -3, -5
                }
            }
        }
        
        return statValue;
    }

    public int getStatsSum()
    {
        return maxHp + attack + defense + specialAttack + specialDefense + speed;
    }

    public bool isKO()
    {
        return currentHp == 0;
    }

    public void changeHP(double hpModifier)
    {
        int finalHpModifier = (int) System.Math.Truncate(hpModifier);

        // Has to be minimum 1 if heal or -1 if damage
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
        bool wasFullHP = currentHp == maxHp;

        currentHp += hpModifier;
        if (currentHp > maxHp) currentHp = maxHp;
        if (currentHp < 0) currentHp = 0;

        // Sturdy
        if (currentHp == 0 && wasFullHP && talent.name == "Sturdy") currentHp = 1;
    }

    public bool canLearnMove(Move move)
    {
        return canLearnMove(move.name);
    }

    public bool canLearnMove(string move)
    {
        foreach (string possibleMove in possibleMoves) {
            if (possibleMove == move) return true;
        }

        return false;
    }

    public bool canHaveTalent(Talent talent)
    {
        return canHaveTalent(talent.name);
    }

    public bool canHaveTalent(string talent)
    {
        foreach (string possibleTalent in possibleTalents) {
            if (possibleTalent == talent) return true;
        }

        return false;
    }

    public bool hasType(Type type)
    {
        foreach (Type beautymonType in types) {
            if (beautymonType.name == type.name) return true;
        }

        return false;
    }

    public bool canBeAffectedByMove(Move move)
    {
        // Immunity by type
        foreach (Type type in types) {
            if (type.IsImmunedAgainst(move.type)) return false;
        }

        // Immunity by talent
        if ((talent.name == "FlashFire") && (move.type.name == "Fire")) {
            flashFireBuff = true;
            return false;
        }
        if ((talent.name == "WaterAbsorb") && (move.type.name == "Water")) {
            changeHP(maxHp * 0.25);
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
            { "Asleep", 0 },
            { "BadlyPoisoned", 0 },
            { "Burned", 0 },
            { "Frozen", 0 },
            { "Paralized", 0 },
            { "Poisoned", 0 },
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
        return isOfType(wantedType.name);
    }

    public bool isOfType(string typeName)
    {
        foreach (Type type in types) {
            if (type.name == typeName) return true;
        }

        return false;
    }

    public bool canBeAsleep()
    {
        return talent.name == "Insomniac";
    }

    public bool canBeBurned()
    {
        return !isOfType("Fire");
    }

    public bool canBeFrozen()
    {
        return !isOfType("Ice");
    }

    public bool canBeParalized()
    {
        return !isOfType("Electric");
    }

    public bool canBePoisoned()
    {
        return !isOfType("Poison") && !isOfType("Steel");
    }

    public bool canBeAffectedByCondition(Condition incomingCondition)
    {
        return (
            ((incomingCondition.name == "Asleep") && canBeAsleep())
            || ((incomingCondition.name == "Burned") && canBeBurned())
            || ((incomingCondition.name == "Frozen") && canBeFrozen())
            || ((incomingCondition.name == "Paralized") && canBeParalized())
            || (((incomingCondition.name == "Poisoned") || (incomingCondition.name == "BadlyPoisoned")) && canBePoisoned())
        );
    }

    public void changeConditionCounter(Condition incomingCondition, int modifier, bool force = false)
    {
        if (!canBeAffectedByCondition(incomingCondition)) return;

        if (incomingCondition != null && !force) return;

        conditionsCounter[incomingCondition.name] += modifier;

        if (conditionsCounter[incomingCondition.name] >= 100) {
            setCondition(incomingCondition, force);
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
