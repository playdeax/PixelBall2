using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardPopup : MonoBehaviour
{
    public BBUIView popup;
    public BBUIButton btnClose;
    public BBUIButton btnClaim;
    public BBUIButton btnClaimVideo;
    public ParticleSystem efxDay7;
    public BBUIView day7Obj;
    public GameObject lockGroup;

    public Transform posCenter;
    public Transform posCoin;
    public Transform posHeart;

    [Header("LIST DAILY REWARD")]
    public List<ItemDailyReward> listDailyRewards = new List<ItemDailyReward>();
    // Start is called before the first frame update
    void Start()
    {
        popup.ShowBehavior.onCallback_Completed.AddListener(Popup_ShowView_Finished);
        popup.HideBehavior.onCallback_Completed.AddListener(Popup_HideView_Finished);

        btnClose.OnPointerClickCallBack_Completed.AddListener(TouchClose);
        btnClaim.OnPointerClickCallBack_Completed.AddListener(TouchClaim);
        btnClaimVideo.OnPointerClickCallBack_Completed.AddListener(TouchClaimVideo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ItemDailyReward currDailyReward;
    public void OpenPopup() {
        gameObject.SetActive(true);
        efxDay7.gameObject.SetActive(false);
        day7Obj.gameObject.SetActive(false);
        lockGroup.SetActive(true);
        Config.currIndexDailyReward = Config.GetDailyReward();
        ShowItemReward();

        if (Config.CheckDailyReward())
        {
            btnClaim.Interactable = true;
            btnClaimVideo.Interactable = true;
        }
        else {
            btnClaim.Interactable = false;
            btnClaimVideo.Interactable = false;
        }

        if (CheckItemReward_isBall()) {
            btnClaimVideo.Interactable = false;
        }

        

        popup.ShowView();
    }


    public void ShowItemReward() {
        for (int i = 0; i < listDailyRewards.Count; i++)
        {
            if (i < Config.currIndexDailyReward)
            {
                listDailyRewards[i].ShowRewarded(true);
            }
            else
            {
                listDailyRewards[i].ShowRewarded(false);
                if (i == Config.currIndexDailyReward)
                {
                    currDailyReward = listDailyRewards[i];
                }
            }
        }
    }


    public bool CheckItemReward_isBall()
    {
        if (currDailyReward == null) return false;
        for (int i = 0; i < currDailyReward.listInfoItems.Count;i++) {
            if (currDailyReward.listInfoItems[i].itemType == Config.ITEM_TYPE.BALL) {
                return true;
            }
        }
        return false;

    }

    public void Popup_ShowView_Finished() {
        lockGroup.SetActive(false);
        efxDay7.gameObject.SetActive(true);
        efxDay7.Play();
        day7Obj.gameObject.SetActive(true);
        day7Obj.ShowView();
    }

    public void Popup_HideView_Finished() {
        gameObject.SetActive(false);
    }


    public void TouchClose() {
        lockGroup.SetActive(true);
        popup.HideView();
        efxDay7.gameObject.SetActive(false);
        day7Obj.gameObject.SetActive(false);
    }


    public void TouchClaim() {
        Config.SetDailyReward(Config.currIndexDailyReward+1);
       
        ShowReward(false);
        btnClaim.Interactable = false;
        btnClaimVideo.Interactable = false;


        Config.currIndexDailyReward = Config.GetDailyReward();
        ShowItemReward();
    }


    public void TouchClaimVideo() {
        Config.SetDailyReward(Config.currIndexDailyReward + 1);
       
        ShowReward(true);
        btnClaim.Interactable = false;
        btnClaimVideo.Interactable = false;

        Config.currIndexDailyReward = Config.GetDailyReward();
        ShowItemReward();
    }


    public void ShowReward(bool isVideo) {
        for (int i = 0; i < currDailyReward.listInfoItems.Count; i++) {
            if (currDailyReward.listInfoItems[i].itemType == Config.ITEM_TYPE.COIN)
            {
                int addCoin = currDailyReward.listInfoItems[i].value;
                if (isVideo) addCoin *= 2;
                ShowRewardCoin(addCoin);
            }
            else if (currDailyReward.listInfoItems[i].itemType == Config.ITEM_TYPE.BALL) {
                ShowRewardBall(currDailyReward.listInfoItems[i].value);
            }
            else if (currDailyReward.listInfoItems[i].itemType == Config.ITEM_TYPE.HEART)
            {
                int addHeart = currDailyReward.listInfoItems[i].value;
                if (isVideo) addHeart *= 2;
                ShowRewardHeart(addHeart);
            }
        }

        
    }


    public void ShowReward_Day7() {
        efxDay7.gameObject.SetActive(true);
        efxDay7.Play();
        day7Obj.gameObject.SetActive(true);
        day7Obj.ShowView();
    }

    public void ShowRewardCoin(int _addCoin) { 
        if(GamePlayManager.Ins != null)
        {
            GamePlayManager.Ins.AddCoin(_addCoin,posCenter.position,posCoin.position,()=>{ 
            
            });
        }
        else if (HomeManager.Ins != null)
        {
            HomeManager.Ins.AddCoin(_addCoin, posCenter.position, posCoin.position, () => {

            });
        }
    }


    public void ShowRewardBall(int _idBallNewSkin) {
        if (GamePlayManager.Ins != null)
        {
            GamePlayManager.Ins.OpenNewSkinPopup(_idBallNewSkin);
            efxDay7.gameObject.SetActive(false);
            day7Obj.gameObject.SetActive(false);
        }
        else if (HomeManager.Ins != null)
        {
            HomeManager.Ins.OpenNewSkinPopup(_idBallNewSkin);
            efxDay7.gameObject.SetActive(false);
            day7Obj.gameObject.SetActive(false);
        }
    }

    public void ShowRewardHeart(int _addHeart)
    {
        if (GamePlayManager.Ins != null)
        {
            GamePlayManager.Ins.AddHeart(_addHeart, posCenter.position, posHeart.position, () => {

            });
        }
        else if (HomeManager.Ins != null)
        {
            HomeManager.Ins.AddHeart(_addHeart, posCenter.position, posHeart.position, () => {

            });
        }
    }

}
