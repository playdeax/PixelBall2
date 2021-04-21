using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class LoadingGamePopup : MonoBehaviour
{
    public static LoadingGamePopup Ins;
    public void Awake()
    {
        Ins = this;
    }
    public Image imgMask;

    public SceneTransitionController _sceneTransitionController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    Sequence sequenceShow;
    public void ShowLoading_InGame(Action actionIn) {
        // imgMask.rectTransform.DOSizeDelta(Vector2.zero, 0f);
        // gameObject.SetActive(true);
        // sequenceShow = DOTween.Sequence();
        // sequenceShow.Insert(0f, imgMask.rectTransform.DOSizeDelta(Vector2.one * 100f, 0.5f).SetEase(Ease.OutBack));
        // sequenceShow.Insert(0.55f, imgMask.rectTransform.DOSizeDelta(Vector2.one * 3000f, 1.5f).SetEase(Ease.OutQuad));
        // sequenceShow.OnComplete(() =>
        // {
        //     gameObject.SetActive(false);
        //     actionIn.Invoke();
        // });
        _sceneTransitionController.gameObject.SetActive(true);
    }

    public void ShowLoading_OutGame(Action actionOut)
    {
        // imgMask.rectTransform.DOSizeDelta(Vector2.one * 3000f, 0f);
        // gameObject.SetActive(true);
        // sequenceShow = DOTween.Sequence();
        // sequenceShow.Insert(0f, imgMask.rectTransform.DOSizeDelta(Vector2.one * 150f, 1f).SetEase(Ease.OutBack));
        // sequenceShow.Insert(0.55f, imgMask.rectTransform.DOSizeDelta(Vector2.zero, 1f).SetEase(Ease.OutQuad));
        // sequenceShow.OnComplete(() =>
        // {
        //     //gameObject.SetActive(false);
        //     actionOut.Invoke();
        // });
    }
}
