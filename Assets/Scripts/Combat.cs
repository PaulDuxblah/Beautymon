using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    Climate climate;
    int climateCountdown = 0;

    public Canvas UI;
    public RawImage Moves;

    // Start is called before the first frame update
    void Start()
    {
        Beautymon bebisel = Beautymon.load("Bebisel");
        bebisel.setTalent("Sturdy");
        bebisel.setMoves(new List<string>(){ "Scratch" });
        Beautymon potus = Beautymon.load("Potus");
        potus.setTalent(Talent.load("Flash Fire"));
        potus.setMoves(new List<Move>(){ Move.load("Ember"), Move.load("Growl"), Move.load("Scratch"), Move.load("Zenith") });

        displayMoves(potus);

        useMove(potus, bebisel, potus.moves[0]);
        useMove(bebisel, potus, bebisel.moves[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayMoves(Beautymon beautymon)
    {
        Transform transform = Moves.transform;
        int index = 0;
        foreach (Transform child in transform) {
            if (beautymon.moves.Count > index) {
                displayMove(child, beautymon.moves[index]);
            } else {
                displayEmptyMove(child);
            }
            index++;
        }
    }

    public void displayMove(Transform transform, Move move)
    {
        transform.Find("Name").GetComponent<Text>().text = move.name;
    }

    public void displayEmptyMove(Transform moveHolder)
    {
        moveHolder.Find("Name").GetComponent<Text>().text = "Nothing";
    }

    public void setClimate(Climate newClimate, bool climateExtension)
    {
        climate = newClimate;
        climateCountdown = 5;
        if (climateExtension) climateCountdown = 8;
    }

    public void useMove(Beautymon attacker, Beautymon defender, Move move)
    {
        // Change climate
        if (move.invokesClimate != null) setClimate(move.invokesClimate, false); // TODO item to extend climate duration

        // Not targeting the enemy
        if (!move.targetsEnemy) {
            if (move.modifiesStat != null) attacker.temporaryStatChange(move.modifiesStat, move.modifiesStatModifier);
            return;
        }

        // Immunity
        if (!defender.canBeAffectedByMove(move)) return;

        // Deals damages
        if (move.power > 0) calculateDamages(attacker, defender, move);

        // Change stats
        if (move.modifiesStat != null) defender.temporaryStatChange(move.modifiesStat, move.modifiesStatModifier);

        // Secondary effects
        if (move.inflictsCondition != null) defender.changeConditionCounter(move.inflictsCondition, move.inflictsConditionModifier, move.inflictsConditionForce);
    }

    public void calculateDamages(Beautymon attacker, Beautymon defender, Move move)
    {
        // https://www.pokepedia.fr/Calcul_des_d%C3%A9g%C3%A2ts#Formule_math.C3.A9matique
        string offensiveStatName;
        string defensiveStatName;
        if (move.isPhysical) {
            offensiveStatName = Beautymon.ATTACK;
            if (move.attacksOnTheOtherStat) {
                defensiveStatName = Beautymon.SPECIALDEFENSE;
            } else {
                defensiveStatName = Beautymon.DEFENSE;
            }
        } else {
            offensiveStatName = Beautymon.SPECIALATTACK;
            if (move.attacksOnTheOtherStat) {
                defensiveStatName = Beautymon.DEFENSE;
            } else {
                defensiveStatName = Beautymon.SPECIALDEFENSE;
            }
        }

        // Get stats
        double offensiveStat = attacker.getStatWithTemporaryModifier(offensiveStatName);
        double defensiveStat = defender.getStatWithTemporaryModifier(defensiveStatName);

        // Calculate move power before coeffiencients
        double movePower = ((30 * move.power * offensiveStat) / (defensiveStat * 50)) + 2;
        double moveTotalPower = movePower;

        Type moveType = move.type;

        // Flash Fire
        if (attacker.flashFireBuff && moveType.name == "Fire") moveTotalPower += movePower / 2;

        // STAB
        if (attacker.hasType(moveType)) moveTotalPower += movePower / 2;

        // Burned
        if ((attacker.condition != null) && (attacker.condition.name == "Burned") && (move.isPhysical)) moveTotalPower -= moveTotalPower / 2;

        // Climate
        if (climate != null) {
            if (climate.name == "Hail") {
                if (moveType.name == "Ice") moveTotalPower = moveTotalPower * 1.5;
            } else if (climate.name == "Rain") {
                if (moveType.name == "Fire") {
                    moveTotalPower = moveTotalPower / 2;
                } else if (moveType.name == "Water") {
                    moveTotalPower = moveTotalPower * 1.5;
                }
            } else if (climate.name == "Sandstorm") {
                if ((moveType.name == "Air") && (!move.isPhysical)) moveType = Type.load("Rock");

                if (moveType.name == "Fire") {
                    moveTotalPower = moveTotalPower / 2;
                } else if ((moveType.name == "Rock") && (!move.isPhysical)) {
                    moveTotalPower = moveTotalPower * 1.5;
                } 
            } if (climate.name == "Sun") {
                if ((moveType.name == "Fire") || (moveType.name == "Air")) {
                    moveTotalPower = moveTotalPower * 1.5;
                } else if (moveType.name == "Water") {
                    moveTotalPower = moveTotalPower / 2;
                }
            }
        }

        // Resistances & Weaknesses
        foreach (Type defenderType in defender.types) {
            if (defenderType.IsWeakAgainst(moveType)) {
                moveTotalPower = moveTotalPower * 2;
            } else if (defenderType.isResistantAgainst(moveType)) {
                moveTotalPower = moveTotalPower / 2;
            }
        }

        defender.changeHP(moveTotalPower * -1);
        Debug.Log("DEFENDER HP AFTER ATTACK: " + defender.name + " " + defender.currentHp + "/" + defender.maxHp);
    }
}
