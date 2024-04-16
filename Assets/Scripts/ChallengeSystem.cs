using UnityEngine;

public class ChallengeSystem : MonoBehaviour
{

    Enemy opponent;
    [SerializeField] Combat board;  // [SerializeField] might not work between scenes idk

    void Start()
    {
        opponent = GetComponent<Enemy>();
    }

    void Update()
    {
        bool placeHolder = false; // Replace with combat trigger
        if (placeHolder)
        {
            board.CurrentEnemy = opponent;
        }
    }
}
