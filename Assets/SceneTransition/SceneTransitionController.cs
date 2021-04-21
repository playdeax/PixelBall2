using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;
public class SceneTransitionController : MonoBehaviour
{
    public Image imgLoading;
    // Start is called before the first frame update
    void Start()
    {
        // float angle = 0;
        // DOTween.To(() => angle, x => angle = x, 360, duration)
        //     .OnUpdate(() => {
        //         Debug.Log(Mathf.sin(angle * Mathf.Rad2Deg));
        //     });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float _smoothing = 0.5f;
    public void ShowLoading_In(Action actionIn)
    {
        float _loadingOut = 1.1f;
        DOTween.To(() => _loadingOut, x => _loadingOut = x, -0.1f - _smoothing, 1f).SetEase(Ease.InQuad)
            .OnUpdate(() => {
                imgLoading.material.SetFloat("_cutoff",_loadingOut);
            })
            .OnComplete(() =>
            {
                actionIn.Invoke();
            });
        
    }
    
    public void ShowLoading_Out(Action actionOut)
    {
        float _loadingIn = -0.1f - _smoothing;
        DOTween.To(() => _loadingIn, x => _loadingIn = x, 1.1f, 1.5f).SetEase(Ease.InQuart)
            .OnUpdate(() => {
                imgLoading.material.SetFloat("_cutoff",_loadingIn);
            })
            .OnComplete(() =>
            {
                actionOut.Invoke();
            });
    }
}
