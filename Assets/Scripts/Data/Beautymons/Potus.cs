using System.Collections;
using System.Collections.Generic;

abstract public class Potus : Beautymon
{
    public override List<Type> getTypes()
    {
        return new List<Type>() { new Fire() };
    }
    
    public override List<Move> getPossibleMoves()
    {
        return new List<Move>() {
            new Ember(),
            new Growl(),
            new Howl(),
            new ObstinateStrike(),
            new Scratch(),
            new Zenith(),
        };
    }

    public override List<Talent> getPossibleTalents()
    {
        return new List<Talent>(){ new FlameBody(), new FlashFire() };
    }

    // Compared to https://bulbapedia.bulbagarden.net/wiki/Torchic_(Pok%C3%A9mon)#Base_stats
    public override int getMaxHp()
    {
        return 55;
    }

    public override int getAttack()
    {
        return 70;
    }

    public override int getDefense()
    { 
        return 40;
    }

    public override int getSpecialAttack()
    {
        return 50;
    }

    public override int getSpecialDefense()
    {
        return 40;
    }

    public override int getSpeed()
    {
        return 55;
    }
}
