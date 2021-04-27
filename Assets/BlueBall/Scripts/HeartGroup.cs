using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeartGroup : MonoBehaviour
{
    public TextMeshProUGUI txtHeart;
    public Animator ballAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Config.OnChangeHeart += OnChangeHeart;
        Config.OnChangeActiveBall += OnChangeActiveBall;
        ShowHeart();

        if (ballAnimator != null)
        {
            ballAnimator.runtimeAnimatorController =
                Config.GetInfoBallFromID(Config.GetBallActive()).animatorImgOverrideController;
        }
    }

    private void OnDestroy()
    {
        Config.OnChangeHeart -= OnChangeHeart;
        Config.OnChangeActiveBall -= OnChangeActiveBall;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChangeHeart(int coinValue)
    {
        ShowHeart();
    }

    public void ShowHeart()
    {
        DOTween.Kill(txtHeart.transform);
        txtHeart.transform.localScale = Vector3.one;
        txtHeart.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f, 10, 2f).SetEase(Ease.InOutBack).SetRelative(true).SetLoops(3, LoopType.Restart);
        //txtCoin.transform.DOShakeScale(0.3f,0.5f,10).SetEase(Ease.InOutBack).SetRelative(true);
        txtHeart.text = $"{Mathf.FloorToInt(Config.currHeart)}";
    }

    public void OnChangeActiveBall() {
        if (ballAnimator != null)
        {
            ballAnimator.runtimeAnimatorController =
                Config.GetInfoBallFromID(Config.GetBallActive()).animatorImgOverrideController;
        }
    }
}
