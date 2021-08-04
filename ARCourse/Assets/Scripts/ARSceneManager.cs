using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

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

    public void GotoMain()
    {
        SceneManager.LoadScene("Main", loadSceneMode);
    }

    public void Goto(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        FindObjectOfType<ARSession>().Reset();
    }
}
