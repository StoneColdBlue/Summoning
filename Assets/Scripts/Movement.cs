using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector2 MousePosition { get; private set; }

    void Start()
    {

    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        InputVector = new Vector2 (h, v);
        MousePosition = Input.mousePosition;
    }
}
