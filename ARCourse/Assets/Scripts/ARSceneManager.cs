using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneManager : MonoBehaviour
{
    private const LoadSceneMode loadSceneMode = LoadSceneMode.Single;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoHelloARFoundation()
    {
        SceneManager.LoadScene("HelloARFoundation", loadSceneMode);
    }

    public void GotoImageTrackingObject()
    {
        SceneManager.LoadScene("ImageTrackingObject", loadSceneMode);
    }

    public void GotoImageTrackingVideo()
    {
        SceneManager.LoadScene("ImageTrackingVideo", loadSceneMode);
    }
}
