using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScaleFit : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    private float ratio = 1920f / 1080f;
    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ratio);
        Debug.Log(Screen.width * 1f / Screen.height);
        if (ratio < Screen.width * 1f / Screen.height)
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
