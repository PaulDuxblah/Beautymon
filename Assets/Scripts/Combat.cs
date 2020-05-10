using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    Climate climate;
    int climateCountdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (move.invokeClimate != null) setClimate(move.invokeClimate, false); // TODO item to extend climate duration

        // Not targeting the enemy
        if (!move.targetsEnemy) return;

        // Immunity
        if (!defender.canBeAffectedByMove(move)) return;

        // Offensive move deals damages
        if (move is Offensive) calculateDamages(attacker, defender, (Offensive) move);

        // Secondary effects
        if (move.inflictCondition != null) {
            defender.changeConditionCounter(move.inflictCondition, move.inflictConditionModifier, move.inflictConditionForce);
        }
    }

    public void calculateDamages(Beautymon attacker, Beautymon defender, Offensive move)
    {
        // https://www.pokepedia.fr/Calcul_des_d%C3%A9g%C3%A2ts#Formule_math.C3.A9matique
        string offensiveStatName;
        string defensiveStatName;
        if (move is Physical) {
            offensiveStatName = Beautymon.ATTACK;
            if (move.attackOnTheOtherStat) {
                defensiveStatName = Beautymon.SPECIALDEFENSE;
            } else {
                defensiveStatName = Beautymon.DEFENSE;
            }
        } else {
            offensiveStatName = Beautymon.SPECIALATTACK;
            if (move.attackOnTheOtherStat) {
                defensiveStatName = Beautymon.DEFENSE;
            } else {
                defensiveStatName = Beautymon.SPECIALDEFENSE;
            }
        }
        
        // Get stats
        double offensiveStat = attacker.getStatWithTemporaryModifier(offensiveStatName);
        double defensiveStat = defender.getStatWithTemporaryModifier(defensiveStatName);

        // Calculate base move power, inspired from https://www.pokepedia.fr/Calcul_des_d%C3%A9g%C3%A2ts#Cas_g.C3.A9n.C3.A9ral
        double movePower = ((42 * move.getPower() * offensiveStat) / (defensiveStat * 50)) + 2;
        double moveTotalPower = movePower;

        Type moveType = move.getType();

        // Flash Fire
        if (attacker.flashFireBuff && moveType is Fire) moveTotalPower += movePower / 2;

        // STAB
        if (attacker.getTypes().Contains(moveType)) moveTotalPower += movePower / 2;

        // Burned
        if (attacker.condition is Burned && move is Physical) moveTotalPower -= moveTotalPower / 2;


        // Climate
        if (climate is Hail) {
            if (moveType is Ice) moveTotalPower = moveTotalPower * 2;
        } else if (climate is Rain) {
            if (moveType is Fire) {
                moveTotalPower = moveTotalPower / 2;
            } else if (moveType is Water) {
                moveTotalPower = moveTotalPower * 2;
            }
        } else if (climate is Sandstorm) {
            if (moveType is Air && move is Special) moveType = new Rock();

            if (moveType is Fire) {
                moveTotalPower = moveTotalPower / 2;
            } else if (moveType is Rock && move is Special) {
                moveTotalPower = moveTotalPower * 2;
            } 
        } if (climate is Sun) {
            if (moveType is Fire || moveType is Air) {
                moveTotalPower = moveTotalPower * 2;
            } else if (moveType is Water) {
                moveTotalPower = moveTotalPower / 2;
            }
        }

        // Resistances & Weaknesses
        foreach (Type defenderType in defender.getTypes()) {
            if (defenderType.getWeaknesses().Contains(moveType)) {
                moveTotalPower = moveTotalPower * 2;
            } else if (defenderType.getResistances().Contains(moveType)) {
                moveTotalPower = moveTotalPower / 2;
            }
        }

        defender.changeHP(moveTotalPower * -1);
    }
}
