using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFitAll : MonoBehaviour
{
    float targetAspect = 1242f / 2208f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(targetAspect);
        Debug.Log(Camera.main.aspect);
        if (targetAspect > Camera.main.aspect)
        {

            gameObject.transform.localScale = Vector3.one * targetAspect / Camera.main.aspect;
        }
    }

}
