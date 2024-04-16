using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Billboard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        Vector3 camPos = camera.transform.position;
        transform.LookAt(camPos);
    }
}
