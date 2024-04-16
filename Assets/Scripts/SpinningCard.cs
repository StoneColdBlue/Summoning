using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCard : MonoBehaviour
{

    public float trunSpeed = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, trunSpeed * Time.deltaTime, 0);
    }
}
