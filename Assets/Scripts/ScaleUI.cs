using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{

    [SerializeField] Transform CamStartPos;
    [SerializeField] Transform CamFinalPos;
    //[SerializeField] Transform CamHolder;

    public static float WidthScale {  get; private set; }
    public static float HeightScale { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        WidthScale = Screen.width / 960f;
        HeightScale = Screen.height / 540f;
        CamStartPos.Translate(0f, 0f, CamStartPos.position.z * (WidthScale + HeightScale) / 2f);
        CamFinalPos.Translate(0f, CamFinalPos.position.y * (WidthScale + HeightScale) / 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        WidthScale = Screen.width / 960f;
        HeightScale = Screen.height / 540f;
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach (Transform transform in transforms)
        {
            transform.position.Set(transform.position.x * WidthScale, transform.position.y * HeightScale, 0f);
            transform.localScale.Scale(new Vector3(HeightScale, WidthScale, 0f));
        }
    }
}
