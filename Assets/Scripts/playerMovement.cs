using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Movement _input;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;

    private void Awake()
    {
        _input = GetComponent<Movement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0,_input.InputVector.y);

        //Target Direction
        MoveToTarget(targetVector);

        //charater rotation
    }

    private void MoveToTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        transform.Translate(targetVector *  speed);
    }
}
