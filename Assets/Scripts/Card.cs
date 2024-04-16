public class Card
{
    public static Card Dragon { get { return new Card(0, 0, 0); } }
    public static Card Demon { get { return new Card(0, 0, 0); } }
    public static Card Knight { get { return new Card(0, 0, 0); } }
    public static Card Archer { get { return new Card(0, 0, 0); } }
    public static Card Wizard { get { return new Card(0, 0, 0); } }

    public int HP { get; private set; }
    public int Def { get; private set; }
    public int Atk { get; private set; }
    public int CurrentHP;
    public int CurrentDef;
    public bool Dead { get { return CurrentHP <= 0; } }

    internal Card(int hp, int def, int atk)
    {
        HP = hp;
        Def = def;
        Atk = atk;
        CurrentHP = HP;
        CurrentDef = Def;
    }

    public Card Clone()
    {
        return new Card(this.HP, this.Def, this.Atk);
    }

    public void Attack(Card card, int defenderHP)
    {
        if (card.CurrentDef > 0)
        {
            card.CurrentDef -= Atk / 2;
        }
        if (card.CurrentDef < 0)
        {
            card.CurrentHP += 2 * card.CurrentDef;
            card.CurrentDef = 0;
        }
        if (card.CurrentHP < 0)
        {
            defenderHP += card.CurrentHP;
            card.CurrentHP = 0;
            if (defenderHP < 0) defenderHP = 0;
        }
    }
}
