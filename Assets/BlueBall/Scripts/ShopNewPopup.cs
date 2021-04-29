using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNewPopup : MonoBehaviour
{
    public static ShopNewPopup Ins;

    private void Awake()
    {
        Ins = this;
    }

    public BBUIButton btnBack;

    public PremiumPanel preminumPanel;
    public PremiumPanel coinPanel;
    public PremiumPreview previewPanel;
    // Start is called before the first frame update
    void Start()
    {
        btnBack.OnPointerClickCallBack_Completed.AddListener(TouchBack);
    }

    private void OnDestroy()
    {
        btnBack.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    public void ShowPopup()
    {
        gameObject.SetActive(true);
        btnBack.GetComponent<BBUIView>().ShowView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TouchBack()
    {
        gameObject.SetActive(false);
    }
    
    public void SetUpdateListBalls() {
        previewPanel.ShowInfo();
        preminumPanel.UpdateListBall();
        coinPanel.UpdateListBall();
    }
}
