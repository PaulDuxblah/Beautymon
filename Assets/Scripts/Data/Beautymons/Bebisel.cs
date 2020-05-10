using System.Collections;
using System.Collections.Generic;

abstract public class Bebisel : Beautymon
{
    public Bebisel(List<Move> newMoves, Talent newTalent) : base(newMoves, newTalent)
    {}

    public override List<Type> getTypes()
    {
        return new List<Type>() { new Ice() };
    }
    
    public override List<Move> getPossibleMoves()
    {
        return new List<Move>() {
            new Growl(),
            new FallingIce(),
            new Howl(),
            new ObstinateStrike(),
            new Scratch(),
        };
    }

    public override List<Talent> getPossibleTalents()
    {
        return new List<Talent>(){ new Sturdy() };
    }

    public override int getMaxHp()
    {
        return 60;
    }

    public override int getAttack()
    {
        return 70;
    }

    public override int getDefense()
    { 
        return 45;
    }

    public override int getSpecialAttack()
    {
        return 55;
    }

    public override int getSpecialDefense()
    {
        return 45;
    }

    public override int getSpeed()
    {
        return 40;
    }
}
