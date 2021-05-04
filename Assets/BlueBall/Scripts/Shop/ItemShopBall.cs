using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ItemShopBall : MonoBehaviour
{
    public Sprite unselect;
    public Sprite selected;

    public Image img;
    public BBUIButton btn;
    public Animator animatorBall;
    public GameObject lockObj;
    public Action<ItemShopBall> ItemShopBall_CallBack = delegate (ItemShopBall itemShopBall) { };

    // Start is called before the first frame update
    void Start()
    {
        btn.OnPointerClickCallBack_Completed.AddListener(TouchBall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InfoBall infoBall;
    public void InitShopBall(InfoBall _infoBall) {
        infoBall = _infoBall;

        animatorBall.runtimeAnimatorController = infoBall.animatorImgOverrideController;
        ShowLockObject();
    }

    public void InitCallBack(Action<ItemShopBall> _itemShopBall_CallBack) {
        ItemShopBall_CallBack = _itemShopBall_CallBack;
    }

    public void TouchBall() {
        Debug.Log("TouchBallTouchBallTouchBall");
        ItemShopBall_CallBack.Invoke(this);
    }

    public void SetSelected() {
        img.sprite = selected;
    }

    public void SetUnSelected()
    {
        img.sprite = unselect;
    }

    public void ShowLockObject()
    {
        if (infoBall.id == Config.GetBallActive())
        {
            lockObj.SetActive(false);
        }
        else if (infoBall.ballType == Config.BALL_TYPE.PREMIUM && Config.GetBuyIAP(Config.IAP_ID.premium_pack))
        {
            lockObj.SetActive(false);
        }
        else if (Config.GetInfoBallUnlock(infoBall.id))
        {
            lockObj.SetActive(false);
        }
        else
        {
            lockObj.SetActive(true);
        }
    }
}
