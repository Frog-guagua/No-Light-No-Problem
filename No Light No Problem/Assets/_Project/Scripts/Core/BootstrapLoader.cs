using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapLoader : MonoBehaviour
{

    public float waitTime = 0.5f;


    
    void Start()
    {
        
        StartCoroutine(WaitForInstanceInit());
        
    }

    private IEnumerator WaitForInstanceInit()
    {
        yield return new WaitForSeconds(waitTime);

      
        SceneTransitionManager.Instance.FadeOutSceneWithAllBlack("Scene_Splash");
 
        
    }


    
}
