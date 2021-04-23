using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using Sirenix.OdinInspector;
public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Ins;
    public BBUIButton btnPasue;
    public GameObject lockObj;
    private void Awake()
    {
        Ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        btnPasue.OnPointerClickCallBack_Completed.AddListener(TouchPause);
        Config.currGameState = Config.GAMESTATE.PRESTART;
        //ShowWinPopup();
        //ShowLosePopup();

        // LoadingGamePopup.Ins.ShowLoading_InGame(() =>
        // {
        //     
        // });

        StartCoroutine(SetLoading_Start());
    }

    private void OnDestroy()
    {
        btnPasue.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveLeft_Enter(null);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            MoveLeft_Exit();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight_Enter(null);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            MoveRight_Exit();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveJump(null);
        }
    }


    #region MOVE
    public void MoveLeft_Enter(Transform tut)
    {
        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            GameLevelManager.Ins.SetMoveLeft(true);
            if (tut != null)
            {
                tut.gameObject.SetActive(false);
            }
        }
    }

    public void MoveLeft_Exit()
    {
        GameLevelManager.Ins.SetMoveLeft(false);
    }


    public void MoveRight_Enter(Transform tut)
    {
        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            GameLevelManager.Ins.SetMoveRight(true);
            if (tut != null)
            {
                tut.gameObject.SetActive(false);
            }
        }
    }

    public void MoveRight_Exit()
    {
        GameLevelManager.Ins.SetMoveRight(false);
    }


    public void MoveJump(Transform tut) {
        GameLevelManager.Ins.SetJump();
        if (tut != null)
        {
            tut.gameObject.SetActive(false);
        }
    }
    #endregion



    #region PAUSE POPUP
    [Header("Pause POPUP")]
    public PausePopup pausePopup;

    public void TouchPause() {
        Time.timeScale = 0f;
        Debug.Log("TouchPauseTouchPauseTouchPause");
        Config.currGameState = Config.GAMESTATE.PAUSE;
        pausePopup.ShowPopup();
    }

    public void SetUnPause() {
        Time.timeScale = 1f;
        Config.currGameState = Config.GAMESTATE.PLAYING;
    }
    #endregion

    #region WIN POPUP
    [Header("WIN POPUP")]
    public WinPopup winPopup;

    public void ShowWinPopup(int _level)
    {
        winPopup.ShowPopup(1000, _level) ;
    }

    #endregion

    #region LOSE POPUP
    [Header("LOSE POPUP")]
    public LosePopup losePopup;

    public void ShowLosePopup(int _level)
    {
        Config.currGameState = Config.GAMESTATE.GAMEOVER;
        losePopup.ShowPopup(_level);
    }

    #endregion

    #region SHOP POPUP
    [Header("SHOP POPUP")]
    public ShopPopUp shopPopUp;

    public void ShowShopPopup()
    {
        shopPopUp.OpenShop();
    }

    #endregion

    #region STAR
    [Header("STAR")]
    public StarGroup starGroup;
    public int countStar = 0;
    public Transform posStar_Tranform;

    public void InitStarGroup(int maxStar) {
        starGroup.InitStarGroup(maxStar);
    }
    public void SetAddStar() {
        countStar++;
        starGroup.AddStar();
    }
    #endregion


    #region HEALTH
    [Header("HEALTH")]
    public int countHealth = 3;
    public HeartGroup heartGroup;
    public List<Image> listHealthIcons = new List<Image>();

    public void SetBeHit() {
        countHealth--;
        //ballHealth.DOFillAmount(countHealth * 1f / 3f, 0.2f).SetEase(Ease.OutQuart);
        UpdateHealth();
        if (countHealth <= 0) {
            //SceneManager.LoadScene("Level" + GameLevelManager.Ins.level);
            GameLevelManager.Ins.SetBallDead();
        }

    }
    public void SetRevive() {
        countHealth = 3;
        UpdateHealth();
    }

    public void UpdateHealth() {
        for (int i = 0; i < 3; i++) {
            listHealthIcons[i].gameObject.SetActive(i < countHealth);
        }
    }
    #endregion

    #region ADD COIN
    public ItemCoinReward itemCoinRewardPrefab;
    public Transform coinTranfromParent;

    int indexAddCoin = 0;
    int countIndexAddCoin = 0;

    public Action AddCoin_CallBack = delegate () { };
    public void AddCoin(int _countAddCoin, Vector3 posStart, Vector3 posEnd, Action _addCoin_CallBack) {
        AddCoin_CallBack = _addCoin_CallBack;
        indexAddCoin = 0;
        float valueReward = 100f;
        countIndexAddCoin = Mathf.RoundToInt(_countAddCoin / valueReward);
        if (countIndexAddCoin > 25) {
            countIndexAddCoin = 25;
            valueReward = _countAddCoin / 25f;
        }
        lockObj.SetActive(true);
        StartCoroutine(AddCoin_IEnumerator(posStart, posEnd, valueReward));
    }

    public IEnumerator AddCoin_IEnumerator(Vector3 posStart, Vector3 posEnd,float valueReward) {
        float timeWait = 0.5f / countIndexAddCoin;
        if (timeWait > 0.05f) timeWait = 0.05f;
        while (indexAddCoin < countIndexAddCoin) {
            ItemCoinReward itemCoinReward = Instantiate<ItemCoinReward>(itemCoinRewardPrefab, coinTranfromParent);
            itemCoinReward.ShowCoin(posStart,posEnd,valueReward);
            indexAddCoin++;
            yield return new WaitForSeconds(timeWait);
        }

        yield return new WaitForSeconds(3f);
        lockObj.SetActive(false);
        AddCoin_CallBack.Invoke();
    }
    #endregion


    #region ADD HEART
    public ItemHeartReward itemHeartRewardPrefab;

    int indexAddHeart = 0;
    int countIndexAddHeart = 0;

    public Action AddHeart_CallBack = delegate () { };
    public void AddHeart(int _countAddHeart, Vector3 posStart, Vector3 posEnd, Action _addHeart_CallBack)
    {
        AddHeart_CallBack = _addHeart_CallBack;
        indexAddHeart = 0;
        int valueReward = 1;
        countIndexAddHeart = _countAddHeart;
        lockObj.SetActive(true);
        StartCoroutine(AddHeart_IEnumerator(posStart, posEnd, valueReward));
    }

    public IEnumerator AddHeart_IEnumerator(Vector3 posStart, Vector3 posEnd, int valueReward)
    {
        float timeWait = 0.5f / countIndexAddCoin;
        if (timeWait > 0.05f) timeWait = 0.05f;
        while (indexAddHeart < countIndexAddHeart)
        {
            ItemHeartReward itemHeartReward = Instantiate<ItemHeartReward>(itemHeartRewardPrefab, coinTranfromParent);
            itemHeartReward.ShowHeart(posStart, posEnd, valueReward);
            indexAddHeart++;
            yield return new WaitForSeconds(timeWait);
        }

        yield return new WaitForSeconds(3f);
        lockObj.SetActive(false);
        AddHeart_CallBack.Invoke();
    }
    #endregion

    #region NEWSKIN
    [Header("NEW SKIN POPUP")]
    public RescuePopup rescuePopup;

    public void OpenNewSkinPopup(int _idBallNewSkin) {
        rescuePopup.OpenNewSkin(_idBallNewSkin);
    }

    public void CloseRescuePopup() {
        if (dailyRewardPopup.isActiveAndEnabled) {
            ShowDailyReward_Day7();
        }
    }
    #endregion


    #region DAILY_REWARD
    [Header("DAILY_REWARD POPUP")]
    public DailyRewardPopup dailyRewardPopup;
    public void ShowDailyReward_Day7() {
        dailyRewardPopup.ShowReward_Day7();
    }

    public void OpenDailyRewardPopup() {
        dailyRewardPopup.OpenPopup();
    }
    #endregion

    #region STARTER_PACK
    [Header("STARTER_PACK POPUP")]
    public StarterPackPopup starterPackPopup;
   
    public void OpenStarterPackPopup()
    {
        starterPackPopup.OpenPopup();
    }
    #endregion

    #region PREMIUM_PACK
    [Header("PREMIUM_PACK POPUP")]
    public PremiumPackPopup premiumPackPopup;

    public void OpenPremiumPackPopup()
    {
        premiumPackPopup.OpenPopup();
    }
    #endregion

    #region SCENE_TRANSITION
    [Header("SCENE TRANSITION")]
    public SceneTransitionController sceneTransitionController;

    public IEnumerator SetLoading_Start()
    {
        sceneTransitionController.gameObject.SetActive(true);
        sceneTransitionController.SetLoadin_OFF();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("SetLoading_StartSetLoading_Start");
        sceneTransitionController.ShowLoading_Out(() =>
        {
            sceneTransitionController.gameObject.SetActive(false);
            Config.currGameState = Config.GAMESTATE.PLAYING;
        });
    }
    public void SetLoading_In(Action actionIn)
    {
        Debug.Log("SetLoading_InAAAAAAAAAAAAAAAAAAAA");
        sceneTransitionController.gameObject.SetActive(true);
        sceneTransitionController.ShowLoading_In(() =>
        {
            actionIn.Invoke();
        });
    }  
    public void SetLoading_Out(Action actionOut)
    {
        sceneTransitionController.gameObject.SetActive(true);
        sceneTransitionController.ShowLoading_Out(() =>
        {
            actionOut.Invoke();
        });
    }
    
    [Button("TestLoading_In")]
    public void TestLoading_In()
    {
        SetLoading_In(() => { });
    }

    [Button("TestLoading_Out")]
    public void TestLoading_Out()
    {
        StartCoroutine(TestLoading_Out_IEnumerator());
    }

    public IEnumerator TestLoading_Out_IEnumerator()
    {
        yield return new WaitForSeconds(0.1f);
        SetLoading_Out(() => { });
    }


    #endregion
    
    
}
