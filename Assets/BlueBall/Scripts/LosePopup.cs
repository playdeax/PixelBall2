using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LosePopup : MonoBehaviour
{
    public BBUIView bg;
    public BBUIView popup;
    public BBUIButton btnSkip;
    public BBUIButton btnPlay;
    public Animator animator;

    public BBUIButton btnFreeCoin;
    public BBUIButton btnDailyReward;
    public BBUIButton btnChest;
    public BBUIButton btnBall;
    public BBUIButton btnAddHeart;
    public BBUIButton btnShop;
    public BBUIButton btnShopCoin;
    public Transform posAddCoin;

    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtCompeted;

    public GameObject lockPopup;
    // Start is called before the first frame update
    void Start()
    {
        btnSkip.OnPointerClickCallBack_Completed.AddListener(TouchSkip);
        btnPlay.OnPointerClickCallBack_Completed.AddListener(TouchPlay);
        btnFreeCoin.OnPointerClickCallBack_Completed.AddListener(TouchFreeCoin);
        btnDailyReward.OnPointerClickCallBack_Completed.AddListener(TouchDailyReward);
        btnShop.OnPointerClickCallBack_Completed.AddListener(TouchShop);
        btnChest.OnPointerClickCallBack_Completed.AddListener(TouchChest);
        btnAddHeart.OnPointerClickCallBack_Completed.AddListener(TouchAddHeart);

        if (Config.currInfoBall_Try != null)
        {
            animator.runtimeAnimatorController = Config.currInfoBall_Try.animatorImgOverrideController;
        }
        else
        {
            animator.runtimeAnimatorController = Config.currInfoBall.animatorImgOverrideController;
        }

        txtLevel.text = "LEVEL " + Config.GetLevel();
    }


    private void OnDestroy()
    {
        btnSkip.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnPlay.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnFreeCoin.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnDailyReward.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnShop.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnChest.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPopup()
    {
        gameObject.SetActive(true);
        lockPopup.SetActive(true);

        StartCoroutine(ShowPopup_IEnumerator());
    }

    public IEnumerator ShowPopup_IEnumerator()
    {
        SoundManager.instance.SFX_GameOver();
        bg.gameObject.SetActive(false);
        btnPlay.gameObject.SetActive(false);
        btnSkip.gameObject.SetActive(false);
        btnFreeCoin.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnDailyReward.gameObject.SetActive(false);
        btnBall.gameObject.SetActive(false);
        btnAddHeart.gameObject.SetActive(false);
        btnShop.gameObject.SetActive(false);
        btnBall.gameObject.SetActive(false);
        btnShopCoin.gameObject.SetActive(false);
        animator.gameObject.SetActive(false);
        txtLevel.gameObject.SetActive(false);
        txtCompeted.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        bg.gameObject.SetActive(true);
        bg.ShowView();

        yield return new WaitForSeconds(0.2f);
        txtLevel.gameObject.SetActive(true);
        txtLevel.gameObject.GetComponent<BBUIView>().ShowView();
        txtCompeted.gameObject.SetActive(true);
        txtCompeted.gameObject.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.5f);
        btnBall.gameObject.SetActive(true);
        btnBall.GetComponent<BBUIView>().ShowView();
        btnSkip.gameObject.SetActive(true);
        btnSkip.GetComponent<BBUIView>().ShowView();
        btnFreeCoin.gameObject.SetActive(true);
        btnFreeCoin.GetComponent<BBUIView>().ShowView();
        if (Config.CheckShowStartPack())
        {
            btnChest.gameObject.SetActive(true);
            btnChest.GetComponent<BBUIView>().ShowView();
        }
        btnShopCoin.gameObject.SetActive(true);
        btnShopCoin.GetComponent<BBUIView>().ShowView();
        btnBall.gameObject.SetActive(true);
        btnBall.GetComponent<BBUIView>().ShowView();
        btnAddHeart.gameObject.SetActive(true);
        btnAddHeart.GetComponent<BBUIView>().ShowView();
        btnShop.gameObject.SetActive(true);
        btnShop.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.2f);
        animator.gameObject.SetActive(true);
        animator.GetComponent<BBUIView>().ShowView();
        btnDailyReward.gameObject.SetActive(true);
        btnDailyReward.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(1f);
        lockPopup.SetActive(false);

        yield return new WaitForSeconds(1f);
        btnPlay.gameObject.SetActive(true);
        btnPlay.GetComponent<BBUIView>().ShowView();

        if (Config.CheckDailyReward())
        {
            GamePlayManager.Ins.OpenDailyRewardPopup();
        }
    }



    public void TouchSkip()
    {
       

        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    Config.SetLevel(Config.GetLevel() + 1);
                    SetLoadingGame();

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Avaiable!");
        }
    }

    public void TouchPlay()
    {
        SetLoadingGame();
    }

    public void SetLoadingGame() {
        LoadingGamePopup.Ins.ShowLoading_OutGame(() =>
        {
            SceneManager.LoadScene("Level" + Config.GetLevel());
            Config.currInfoBall_Try = null;
        });
    }


    public void TouchFreeCoin()
    {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    btnFreeCoin.Interactable = false;
                    GamePlayManager.Ins.AddCoin(Config.FREE_COIN_REWARD, animator.transform.position, posAddCoin.position, () => {
                        btnFreeCoin.Interactable = false;
                    });

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Avaiable!");
        }
    }

    public void TouchShop()
    {
        GamePlayManager.Ins.ShowShopPopup();
    }


    public void TouchDailyReward()
    {
        GamePlayManager.Ins.OpenDailyRewardPopup();
    }

    public void TouchChest()
    {
        GamePlayManager.Ins.OpenStarterPackPopup();
    }

    public void TouchAddHeart()
    {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    btnAddHeart.Interactable = false;
                    GamePlayManager.Ins.AddHeart(Config.FREE_HEART_REWARD, animator.transform.position, btnBall.transform.position, () => {
                        btnAddHeart.Interactable = false;
                    });

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Avaiable!");
        }
    }
}
