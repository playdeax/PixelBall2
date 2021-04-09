using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPopUp : MonoBehaviour
{
    public static ShopPopUp Ins;



    public enum SHOP_TYPE { 
        PREMIUM,
        RESCUE,
        COIN
    }
    private void Awake()
    {
        Ins = this;
    }
    public List<Button> tabs;
    public Sprite unselect;
    public Sprite selected;
    [Header("PACK")]
    public PackPanel packPanel;
    [Header("SKIN PREMIUM")]
    public PremiumPanel premiumPanel;
    [Header("SKIN RESCUE")]
    public PremiumPanel rescuePanel;
    [Header("SKIN COIN")]
    public PremiumPanel coinPanel;

    [Header("Preview")]
    public PremiumPreview previewPanel;

    public BBUIView popup;
    public BBUIButton btnClose;
    public GameObject lockGroup;
    void Start()
    {
        popup.HideBehavior.onCallback_Completed.AddListener(Popup_HideView_Finished);
        popup.ShowBehavior.onCallback_Completed.AddListener(Popup_ShowView_Finished);
        btnClose.OnPointerClickCallBack_Completed.AddListener(TouchClose);

        ShowTabSelected(1);
    }

    private void OnDestroy()
    {
        popup.HideBehavior.onCallback_Completed.RemoveAllListeners();
    }

    public void Popup_HideView_Finished() {
        gameObject.SetActive(false);
    }

    public void Popup_ShowView_Finished()
    {
        lockGroup.SetActive(false);
    }

    public void TabSelected(int index)
    {
        //Debug.Log("TabSelected " + index);
        //panels[index].SetActive(true);
        //tabs[index].GetComponent<Image>().sprite = selected;
        //tabs[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
        //for (int i = 0; i < panels.Count; i++)
        //{
        //    if(i!= index)
        //    {
        //        panels[i].SetActive(false);
        //        tabs[i].GetComponent<Image>().sprite = unselect;
        //        tabs[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        //    }
        //}

        SoundManager.instance.PlaySound_Click();
        ShowTabSelected(index);
    }


    public void ShowTabSelected(int index) {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (i == index)
            {
                tabs[index].GetComponent<Image>().sprite = selected;
                tabs[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
            }
            else
            {
                tabs[i].GetComponent<Image>().sprite = unselect;
                tabs[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
        }
        packPanel.gameObject.SetActive(false);
        premiumPanel.gameObject.SetActive(false);
        rescuePanel.gameObject.SetActive(false);
        coinPanel.gameObject.SetActive(false);
        previewPanel.gameObject.SetActive(false);

        if (index == 1)
        {
            premiumPanel.gameObject.SetActive(true);
            previewPanel.gameObject.SetActive(true);
            previewPanel.SetInfoBallPreview(Config.GetInfoBallFromID(premiumPanel.listBallIDs[0]), SHOP_TYPE.PREMIUM);
        }
        else if (index == 2)
        {
            rescuePanel.gameObject.SetActive(true);
            previewPanel.gameObject.SetActive(true);
            previewPanel.SetInfoBallPreview(Config.GetInfoBallFromID(rescuePanel.listBallIDs[0]), SHOP_TYPE.RESCUE);
        }
        else if (index == 3)
        {
            coinPanel.gameObject.SetActive(true);
            previewPanel.gameObject.SetActive(true);
            previewPanel.SetInfoBallPreview(Config.GetInfoBallFromID(coinPanel.listBallIDs[0]), SHOP_TYPE.COIN);
        }
        else if (index == 0) {
            packPanel.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        
    }


    public void OpenShop() {
        gameObject.SetActive(true);
        lockGroup.SetActive(true);
        popup.ShowView();
    }

    public void CloseShop() {
        popup.HideView();
    }

    public void TouchClose() {
        CloseShop();
    }

    public void SetBuySuccess() {
        previewPanel.ShowInfo();
    }

    public void ShowLockGroup(bool isShowLockGroup) {
        lockGroup.SetActive(isShowLockGroup);
    }
}
