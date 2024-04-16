using System.Collections.Generic;
using UnityEngine;

public class Enemy
{

    [SerializeField] public int HP;
    [SerializeField] Combat board; // [SerializeField] might not work between scened idk

    Dictionary<int, string> Cards = new Dictionary<int, string>()
    {
        { 0, "Dragon" },
        { 1, "Demon" },
        { 2, "Knight" },
        { 3, "Archer" },
        { 4, "Wizard" }
    };

    public int[] Hand { get; private set; } = new int[5] { -1, -1, -1, -1, -1 };

    public void Turn()
    {
        System.Random random = new System.Random();
        int cardSlot = random.Next(4, 7);
        int cardChoice = random.Next(0, Hand.Length);
        for (int i = 0; i < Hand.Length; i++)
        {
            if (Hand[i] == -1)
            {
                Hand[i] = random.Next(0, Cards.Count);
            }
        }
        do
        {
            board.SetCard(false, cardSlot, Cards[cardChoice]);
        } while (!board.PlacedThisRound[1]);
        Hand[cardChoice] = -1; // Hopefully empty hand slot once card is placed
    }
}
