using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AnimationTransition : MonoBehaviour
{
    Animator animator;
    int walk;
    int roll;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        walk = Animator.StringToHash("isWalking");
        roll = Animator.StringToHash("isRolling");
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        //walk animation
        bool forward = Input.GetKey("w");
        bool backward = Input.GetKey("s");
        bool forward2 = Input.GetKey("up");
        bool backward2 = Input.GetKey("down");
        bool isRolling = animator.GetBool(walk);

        //if player presses w key
        if (forward || backward || forward2 || backward2)
        {
            //walk animation true
            animator.SetBool(walk, true);
        }
        else if (!forward || !backward || !forward2 || !backward2)
        {
            animator.SetBool(walk, false);
        }

        //roll animation
    }
}
