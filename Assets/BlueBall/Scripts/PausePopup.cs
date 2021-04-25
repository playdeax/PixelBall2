using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class PausePopup : MonoBehaviour
{
    public BBUIView popup;
    public BBUIButton btnContinue;
    public BBUIButton btnRestart;
    public BBUIButton btnSkipLevel;
    public GameObject lockPopup;
    public Toggle toggleSound;
    public Toggle toggleMusic;
    public Toggle toggleVibration;


    // Start is called before the first frame update
    void Start()
    {
        popup.ShowBehavior.onCallback_Completed.AddListener(PopupShowView_Finished);
        popup.HideBehavior.onCallback_Completed.AddListener(PopupHideView_Finished);

        btnContinue.OnPointerClickCallBack_Completed.AddListener(TouchContinue);
        btnRestart.OnPointerClickCallBack_Completed.AddListener(TouchRestart);
        btnSkipLevel.OnPointerClickCallBack_Completed.AddListener(TouchSkipLevel);

        toggleSound.onValueChanged.AddListener(TouchSound);
        toggleMusic.onValueChanged.AddListener(TouchMusic);
        toggleVibration.onValueChanged.AddListener(TouchVibration);

        InitToogle();
    }


    private void OnDestroy()
    {
        popup.ShowBehavior.onCallback_Completed.RemoveAllListeners();
        popup.ShowBehavior.onCallback_Completed.RemoveAllListeners();

        btnContinue.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnRestart.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnSkipLevel.OnPointerClickCallBack_Completed.RemoveAllListeners();

        toggleSound.onValueChanged.RemoveAllListeners();
        toggleMusic.onValueChanged.RemoveAllListeners();
        toggleVibration.onValueChanged.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitToogle()
    {
        toggleSound.isOn = Config.isSound;
        toggleMusic.isOn = Config.isMusic;
        toggleVibration.isOn = Config.isVibration;
    }

    public void TouchSound(bool isSound)
    {
        Config.SetSound(isSound);
    }

    public void TouchMusic(bool isMusic)
    {
        Config.SetMusic(isMusic);
        if (isMusic)
        {
            MusicManager.instance.PlayMusicBG();
        }
        else
        {
            MusicManager.instance.StopMusicBG();
        }
    }

    public void TouchVibration(bool isVibration)
    {
        Config.SetVibration(isVibration);
    }

    private Config.POPUP_ACTION popupActionType = Config.POPUP_ACTION.NONE;

    public void ShowPopup()
    {
        gameObject.SetActive(true);
        lockPopup.SetActive(true);
        popup.ShowView();
    }

    public void PopupShowView_Finished()
    {
        lockPopup.SetActive(false);
    }


    public void HidePopup()
    {
        lockPopup.SetActive(true);
        popup.HideView();
    }

    public void PopupHideView_Finished()
    {
        if (popupActionType == Config.POPUP_ACTION.CONTINUE)
        {
            GamePlayManager.Ins.SetUnPause();
            gameObject.SetActive(false);
        }
        else
        {
            GamePlayManager.Ins.SetLoading_In(() =>
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                gameObject.SetActive(false);
            
                if (popupActionType == Config.POPUP_ACTION.RESTART)
                {
                    SceneManager.LoadScene("Level" + Config.GetLevel());
                }
                else if (popupActionType == Config.POPUP_ACTION.SKIPLEVEL)
                {
                    Config.SetLevel(Config.GetLevel() + 1);
                    SceneManager.LoadScene("Level" + Config.GetLevel());
                }
            });
        }
    }


    public void TouchContinue()
    {
        popupActionType = Config.POPUP_ACTION.CONTINUE;
        HidePopup();
    }


    [Button("TouchRestart")]
    public void TouchRestart()
    {
        Debug.Log("TouchRestartTouchRestart");
        SceneManager.LoadScene("Level" + GameLevelManager.Ins.level);
        popupActionType = Config.POPUP_ACTION.RESTART;
        HidePopup();
    }

    public void TouchSkipLevel()
    {
        Debug.Log("TouchSkipLevelTouchSkipLevel");
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    popupActionType = Config.POPUP_ACTION.SKIPLEVEL;
                    HidePopup();
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