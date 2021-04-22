using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class SceneTransitionController : MonoBehaviour
{
    public Image imgLoading;

    public Slider sliderValue;

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

    float _loadingOut = 1.1f;
    Tween t_In;

    public void ShowLoading_In(Action actionIn)
    {
        Debug.Log("ShowLoading_InShowLoading_In");
        sliderValue.value = sliderValue.maxValue;
        imgLoading.material.SetFloat("_cutoff", sliderValue.value);
        Debug.Log("ShowLoading_InShowLoading_In 3333333333");
        DOTween.Kill(sliderValue);
        sliderValue.DOValue(sliderValue.minValue, 1f).SetEase(Ease.OutQuad).SetUpdate(true).OnUpdate(() =>
        {
            Debug.Log("ShowLoading_InShowLoading_In 22222");
            imgLoading.material.SetFloat("_cutoff", sliderValue.value);
        }).OnComplete(() =>
        {
            Debug.Log("ShowLoading_InShowLoading_In 2222222");
            actionIn.Invoke();
        });
    }

    float _loadingIn = -0.1f;

    public void ShowLoading_Out(Action actionOut)
    {
        sliderValue.value = sliderValue.minValue;
        imgLoading.material.SetFloat("_cutoff", sliderValue.value);
        sliderValue.DOValue(sliderValue.maxValue, 1f).SetEase(Ease.OutQuad).SetUpdate(true)
            .OnUpdate(() => { imgLoading.material.SetFloat("_cutoff", sliderValue.value); })
            .OnComplete(() => { actionOut.Invoke(); });
    }


    public void SetLoadin_ON()
    {
        imgLoading.material.SetFloat("_cutoff", sliderValue.minValue);
    }
    public void SetLoadin_OFF()
    {
        imgLoading.material.SetFloat("_cutoff", sliderValue.maxValue);
    }
}
