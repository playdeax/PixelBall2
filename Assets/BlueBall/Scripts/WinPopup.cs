using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WinPopup : MonoBehaviour
{
    public BBUIView bg;
    public BBUIView popup;
    public BBUIButton btnNoThank;
    public BBUIButton btnReward;
    public TextMeshProUGUI txtReward;
    public BBUIButton btnFreeCoin;
    public BBUIButton btnDailyReward;
    public BBUIButton btnChest;
    public BBUIButton btnBall;
    public BBUIButton btnAddHeart;
    public BBUIButton btnShop;
    public BBUIButton btnShopCoin;
    public Transform posAddCoin;
    public Animator animator;
    public ParticleSystem efxWin;



    public GameObject lockPopup;
   
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtCompeted;

    [Header("NEW SKIN POPUP")]
    public RescuePopup rescuePopup;
    [Header("DAILY REWARD POPUP")]
    public DailyRewardPopup dailyRewardPopup;

    // Start is called before the first frame update
    void Start()
    {
        btnNoThank.OnPointerClickCallBack_Completed.AddListener(TouchNoThank);
        btnReward.OnPointerClickCallBack_Completed.AddListener(TouchReward);
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
    }

    private void OnDestroy()
    {
        btnNoThank.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnReward.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnFreeCoin.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnDailyReward.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnShop.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnChest.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int coinReward;
    public void ShowPopup(int _coinReward, int _level) {
        lockPopup.SetActive(true);
        coinReward = _coinReward;
        gameObject.SetActive(true);

        txtReward.text = "+" + coinReward;
        txtLevel.text = "LEVEL " + Config.GetLevel();

        FirebaseManager.instance.LogLevelWin(_level);

        StartCoroutine(ShowPopup_IEnumerator(coinReward));
        
    }

    public IEnumerator ShowPopup_IEnumerator(int coinReward) {
        SoundManager.instance.SFX_Win();
        bg.gameObject.SetActive(false);
        btnNoThank.gameObject.SetActive(false);
        btnReward.gameObject.SetActive(false);
        btnFreeCoin.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnDailyReward.gameObject.SetActive(false);
        btnBall.gameObject.SetActive(false);
        btnAddHeart.gameObject.SetActive(false);
        btnShop.gameObject.SetActive(false);
        btnBall.gameObject.SetActive(false);
        btnShopCoin.gameObject.SetActive(false);
        animator.gameObject.SetActive(false);
        efxWin.gameObject.SetActive(false);
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
        btnReward.gameObject.SetActive(true);
        btnReward.GetComponent<BBUIView>().ShowView();
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

        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.SFX_PhaoHoa();
        efxWin.gameObject.SetActive(true);
        efxWin.Play();

        yield return new WaitForSeconds(1f);
        lockPopup.SetActive(false);
        GamePlayManager.Ins.AddCoin(coinReward, animator.transform.position, posAddCoin.position,()=> { 
            
        });

        yield return new WaitForSeconds(3f);
        btnNoThank.gameObject.SetActive(true);
        btnNoThank.GetComponent<BBUIView>().ShowView();

        if (Config.currIDBallRescue != -1)
        {
            rescuePopup.OpenRescuePopup();
        }
        else if (Config.CheckDailyReward()) {
          //  dailyRewardPopup.OpenPopup();
        }
    }



    public void TouchNoThank() {

        if(!Config.isFinished_AddCoin) return;
        
        if (Config.interstitialAd_countWin % 2 == 0 && AdmobManager.instance.isInterstititalAds_Avaiable())
        {
            AdmobManager.instance.ShowInterstitialAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                Config.interstitialAd_countWin++;
                SetNextLevel();
            });
        }
        else
        {
            SetNextLevel();
        }
       
    }

    public void TouchReward() {
        //SceneManager.LoadScene("Level1");
        if(!Config.isFinished_AddCoin) return;
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    
                    GamePlayManager.Ins.AddCoin(coinReward, animator.transform.position, posAddCoin.position, () => {
                        btnReward.Interactable = false;
                        SetNextLevel();
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
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
        
    }

    public void SetNextLevel() {
        // LoadingGamePopup.Ins.ShowLoading_OutGame(() =>
        // {
        //     Config.SetLevel(Config.GetLevel() + 1);
        //     SceneManager.LoadScene("Level" + Config.GetLevel());
        //     Config.currInfoBall_Try = null;
        // });
        GamePlayManager.Ins.SetLoading_In(() =>
        {
            Config.SetLevel(Config.GetLevel() + 1);
            SceneManager.LoadScene("Level" + Config.GetLevel());
            Config.currInfoBall_Try = null;
        });
    }

    public void TouchFreeCoin() {
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
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
    }

    public void TouchShop() {
        GamePlayManager.Ins.ShowShopPopup();
    }


    public void TouchDailyReward() {
        Debug.Log("TouchDailyReward");
        dailyRewardPopup.OpenPopup();
    }

    public void TouchChest() {
        GamePlayManager.Ins.OpenStarterPackPopup();
    }

    public void TouchAddHeart() {
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
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
    }
}
