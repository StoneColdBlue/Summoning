using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string ScreenToLoad;
    public void LoadScene()
    {
        SceneManager.LoadScene(ScreenToLoad);
    }
}
