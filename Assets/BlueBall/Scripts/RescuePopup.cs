using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RescuePopup : MonoBehaviour
{
    public BBUIView bg;
    public BBUIButton btnClose;
    public BBUIButton btnReward;
    public BBUIView popup;
    public GameObject lockGroup;
    public Animator animator;
    public TextMeshProUGUI txtTitle;

    public ParticleSystem efxBall;
    public ParticleSystem efxPopup;
    // Start is called before the first frame update
    void Start()
    {
        btnClose.OnPointerClickCallBack_Completed.AddListener(TouchClose);
        btnReward.OnPointerClickCallBack_Completed.AddListener(TouchReward);
        popup.ShowBehavior.onCallback_Completed.AddListener(Popup_ShowView_Finished);
        popup.HideBehavior.onCallback_Completed.AddListener(Popup_HideView_Finished);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool isVideo = true;
    public void OpenRescuePopup(bool _isVideo = true) {
        isVideo = _isVideo;
        gameObject.SetActive(true);
        lockGroup.gameObject.SetActive(true);
        popup.ShowView();
        animator.runtimeAnimatorController = Config.GetInfoBallFromID(Config.currIDBallRescue).animatorImgOverrideController;
        StartCoroutine(OpenPopup_IEnumerator());
    }

    public void OpenNewSkin(int idBallNewSkin)
    {
        isVideo = false;
        gameObject.SetActive(true);
        lockGroup.gameObject.SetActive(true);
        popup.ShowView();
        animator.runtimeAnimatorController = Config.GetInfoBallFromID(idBallNewSkin).animatorImgOverrideController;
        StartCoroutine(OpenPopup_IEnumerator());
    }

    public IEnumerator OpenPopup_IEnumerator() {
        SoundManager.instance.SFX_Win();
        btnClose.gameObject.SetActive(false);
        btnReward.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
        efxBall.gameObject.SetActive(false);
        efxPopup.gameObject.SetActive(false);
        animator.gameObject.SetActive(false);
        txtTitle.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        bg.gameObject.SetActive(true);
        bg.ShowView();

        yield return new WaitForSeconds(0.5f);
        txtTitle.gameObject.SetActive(true);
        txtTitle.gameObject.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.2f);
        animator.gameObject.SetActive(true);
        animator.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.2f);
        efxBall.gameObject.SetActive(true);
        efxBall.Play();

        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFX_PhaoHoa();
        efxPopup.gameObject.SetActive(true);
        efxPopup.Play();

        if (isVideo)
        {
            yield return new WaitForSeconds(1f);
            btnReward.gameObject.SetActive(true);
            btnReward.GetComponent<BBUIView>().ShowView();
        }

        yield return new WaitForSeconds(2f);
        btnClose.gameObject.SetActive(true);
        btnClose.GetComponent<BBUIView>().ShowView();

        lockGroup.gameObject.SetActive(false);
    }

    public void Popup_ShowView_Finished() {
        //lockGroup.gameObject.SetActive(false);
    }

    public void TouchClose(){
        ClosePopup();
    }

    public void ClosePopup() {
        lockGroup.gameObject.SetActive(true);
        popup.HideView();
    }

    public void Popup_HideView_Finished()
    {
        if (GamePlayManager.Ins != null)
        {
            GamePlayManager.Ins.CloseRescuePopup();
        }
        else if (HomeManager.Ins != null)
        {
            HomeManager.Ins.CloseRescuePopup();
        }
        gameObject.SetActive(false);
    }

    public void TouchReward() {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    ClaimSkin();
                    ClosePopup();
                }
                else
                {
                    ClosePopup();
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Available!");
        }

        
    }


    void ClaimSkin()
    {
        Config.SetInfoBallUnlock(Config.currIDBallRescue);
        Config.SetBallActive(Config.currIDBallRescue);
        Config.currInfoBall = Config.GetInfoBallFromID(Config.currIDBallRescue);
    }
}
