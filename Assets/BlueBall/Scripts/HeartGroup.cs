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
    public GameObject iconCountHeart;
    // Start is called before the first frame update
    void Start()
    {
        Config.OnChangeHeart += OnChangeHeart;
        Config.OnChangeActiveBall += OnChangeActiveBall;
        ShowHeart();

        OnChangeActiveBall();
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
        if (Config.GetBuyIAP(Config.IAP_ID.remove_ad_heart))
        {
            txtHeart.text = "Unlimited";
            if (iconCountHeart != null)
            {
                iconCountHeart.SetActive(false);
            }
        }
        else
        {
            txtHeart.text = $"{Mathf.FloorToInt(Config.currHeart)}";
        }
    }

    public void OnChangeActiveBall() {
        if (ballAnimator != null)
        {
            if (Config.currInfoBall_Try != null)
            {
                ballAnimator.runtimeAnimatorController = Config.currInfoBall_Try.animatorImgOverrideController;
            }
            else
            {
                ballAnimator.runtimeAnimatorController =
                    Config.GetInfoBallFromID(Config.GetBallActive()).animatorImgOverrideController;
            }
            
        }
    }
}
