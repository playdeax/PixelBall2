using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PausePopup : MonoBehaviour
{
    public BBUIView popup;
    public BBUIButton btnContinue;
    public BBUIButton btnRestart;
    public BBUIButton btnSkipLevel;
    public GameObject lockPopup;


    // Start is called before the first frame update
    void Start()
    {
        popup.ShowBehavior.onCallback_Completed.AddListener(PopupShowView_Finished);
        popup.HideBehavior.onCallback_Completed.AddListener(PopupHideView_Finished);

        btnContinue.OnPointerClickCallBack_Completed.AddListener(TouchContinue);
        btnRestart.OnPointerClickCallBack_Completed.AddListener(TouchRestart);
        btnSkipLevel.OnPointerClickCallBack_Completed.AddListener(TouchSkipLevel);
    }


    private void OnDestroy()
    {
        popup.ShowBehavior.onCallback_Completed.RemoveAllListeners();
        popup.ShowBehavior.onCallback_Completed.RemoveAllListeners();

        btnContinue.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnRestart.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnSkipLevel.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private Config.POPUP_ACTION popupActionType = Config.POPUP_ACTION.NONE;

    public void ShowPopup() {
        gameObject.SetActive(true);
        lockPopup.SetActive(true);
        popup.ShowView();
    }

    public void PopupShowView_Finished() {
        lockPopup.SetActive(false);
        
    }


    public void HidePopup() {
        lockPopup.SetActive(true);
        popup.HideView();
    }

    public void PopupHideView_Finished()
    {
        if (popupActionType == Config.POPUP_ACTION.CONTINUE)
        {
            GamePlayManager.Ins.SetUnPause();
        }
        else if (popupActionType == Config.POPUP_ACTION.RESTART) {
            SceneManager.LoadScene("Level1");
        }
        else if (popupActionType == Config.POPUP_ACTION.SKIPLEVEL)
        {
            SceneManager.LoadScene("Level1");
        }
        gameObject.SetActive(false);
    }


    public void TouchContinue() {
        popupActionType = Config.POPUP_ACTION.CONTINUE;
        HidePopup();
    }


    public void TouchRestart() {
        popupActionType = Config.POPUP_ACTION.RESTART;
        HidePopup();
    }

    public void TouchSkipLevel()
    {
        popupActionType = Config.POPUP_ACTION.SKIPLEVEL;
        HidePopup();
    }
}
