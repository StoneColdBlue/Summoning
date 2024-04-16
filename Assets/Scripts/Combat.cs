using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    [SerializeField] Transform[] SummonSlots = new Transform[6];

    [SerializeField] Transform[] Summons = new Transform[6];

    [SerializeField] Material[] CardMaterials = new Material[6];

    [SerializeField] SpriteRenderer[] Images = new SpriteRenderer[6];

    static Dictionary<string, Card> CardLookup = new Dictionary<string, Card>()
    {
        { "Dragon", Card.Dragon },
        { "Demon", Card.Demon },
        { "Knight", Card.Knight },
        { "Archer", Card.Archer },
        { "Wizard", Card.Wizard }
    };

    public Enemy CurrentEnemy;

    Card[,] Cards = new Card[2, 3] { { null, null, null }, { null, null, null } };
    public bool[] PlacedThisRound { get; private set; } = new bool[2] { false, false };
    public bool[] SlotsFull { get; private set; } = new bool[2] { false, false };
    int PlayerHP = 5;

    void Start()
    {

    }

    void Update()
    {
        SlotsFull[0] = false;
        SlotsFull[1] = false;
        for (int i = 0; i < Cards.GetLength(0); i++)
        {
            for (int j = 0; j < Cards.GetLength(1); j++)
            {
                SlotsFull[i] &= Cards[i, j] != null;
                if (Cards[i, j] != null)
                {
                    if (Cards[i, j].Dead)
                    {
                        Summons[i + j].Translate(0f, -10f, 0f);
                        CardMaterials[i + j].mainTexture = null;
                        Images[i + j].sprite = null;
                    }
                }
                else
                {
                    Summons[i + j].Translate(0f, -10f, 0f);
                    CardMaterials[i + j].mainTexture = null;
                    Images[i + j].sprite = null;
                }
            }
        }
        if ((PlacedThisRound[0] || SlotsFull[0]) && (PlacedThisRound[1] || SlotsFull[1]))
        {
            for (int i = 0; i < Cards.GetLength(0); i++)
            {
                for (int j = 0; j < Cards.GetLength(1); j++)
                {
                    if (Cards[i, j] != null)
                    {
                        if (!Cards[i, j].Dead)
                        {
                            if (i < Cards.GetLength(0))
                            {
                                if (Cards[i + 1, j] != null)
                                {
                                    Cards[i, j].Attack(Cards[i + 1, j], CurrentEnemy.HP);
                                }
                                else
                                {
                                    CurrentEnemy.HP -= Cards[i, j].Atk;
                                    if (CurrentEnemy.HP < 0) CurrentEnemy.HP = 0;
                                }
                            }
                            else
                            {
                                if (Cards[i - 1, j] != null)
                                {
                                    Cards[i, j].Attack(Cards[i - 1, j], PlayerHP);
                                }
                                else
                                {
                                    PlayerHP -= Cards[i, j].Atk;
                                    if (PlayerHP < 0) PlayerHP = 0;
                                }
                            }
                        }
                        else
                        {
                            Cards[i, j] = null;
                        }
                    }
                }
            }
            PlacedThisRound[0] = false;
            PlacedThisRound[1] = false;
            if (CurrentEnemy.HP <= 0) Winner(0);
            else if (PlayerHP <= 0) Winner(1);
        }
        else if (!SlotsFull[1]) CurrentEnemy.Turn();
    }

    public void SetCard(bool playerCard, int slot, string cardName)
    {
        if (!PlacedThisRound[playerCard ? 0 : 1])
        {
            if (Cards[playerCard ? 0 : 1, slot - 1] == null)
            {
                Cards[playerCard ? 0 : 1, slot] = CardLookup[cardName].Clone();
                Summons[playerCard ? 0 : 1 + (slot - 1) % 3].position = SummonSlots[slot - 1].position;
                CardMaterials[playerCard ? 0 : 1 + (slot - 1) % 3].mainTexture = Resources.Load<Texture2D>($"Images/{cardName}");
                Images[playerCard ? 0 : 1 + (slot - 1) % 3].sprite = Resources.Load<Sprite>($"Images/{cardName} Image");
                PlacedThisRound[playerCard ? 0 : 1] = true;
            }
        }
    }

    public static void Winner(int playerNumber)
    {
        // To-do winner system
    }
}