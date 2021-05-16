using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingPopup : MonoBehaviour
{
    public GameObject lockPopup;
    public BBUIView popup;
    public BBUIButton btnRate;
    public BBUIButton btnLater;

    private Action actionCallback;
    private void Start()
    {
        popup.ShowBehavior.onCallback_Completed.AddListener(PopupShowView_Finished);
        popup.HideBehavior.onCallback_Completed.AddListener(PopupHideView_Finished);

        btnRate.OnPointerClickCallBack_Completed.AddListener(TouchRating);
        btnLater.OnPointerClickCallBack_Completed.AddListener(TouchClose);
    }
    private void OnDestroy()
    {
        popup.ShowBehavior.onCallback_Completed.RemoveAllListeners();
        popup.ShowBehavior.onCallback_Completed.RemoveAllListeners();

        btnRate.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnLater.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }
    private void PopupShowView_Finished()
    {
        lockPopup.SetActive(false);
    }
    private void PopupHideView_Finished()
    {
        actionCallback?.Invoke();
        gameObject.SetActive(false);
    }

    private void TouchRating()
    {
        PlayerPrefs.SetInt("rating",1);
        PlayerPrefs.Save();
        HidePopup();
        Application.OpenURL($"https://play.google.com/store/apps/details?id={Application.identifier}");
        
    }

    private void TouchClose()
    {
        HidePopup();
    }
    private void HidePopup()
    {
        lockPopup.SetActive(true);
        popup.HideView();
    }

    public void ShowPopup(Action callback = null)
    {
        actionCallback = callback;
        gameObject.SetActive(true);
        lockPopup.SetActive(true);
        popup.ShowView();
    }

}
