using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    #region SOUND
    public const string SOUND = "sound";
    public static bool isSound = true;
    public static void SetSound(bool _isSound)
    {
        isSound = _isSound;
        if (_isSound)
        {
            PlayerPrefs.SetInt(SOUND, 1);
        }
        else
        {
            PlayerPrefs.SetInt(SOUND, 0);
        }
        PlayerPrefs.Save();
    }

    public static void GetSound()
    {
        int soundInt = PlayerPrefs.GetInt(SOUND, 1);
        if (soundInt == 1)
        {
            isSound = true;
        }
        else
        {
            isSound = false;
        }
    }
    #endregion
    
    #region VIBRATION
    public const string VIBRATION = "vibration";
    public static bool isVibration = true;
    public static void SetVibration(bool _isVibration)
    {
        isVibration = _isVibration;
        if (_isVibration)
        {
            PlayerPrefs.SetInt(VIBRATION, 1);
        }
        else
        {
            PlayerPrefs.SetInt(VIBRATION, 0);
        }
        PlayerPrefs.Save();
    }

    public static void GetVibration()
    {
        int vibrationInt = PlayerPrefs.GetInt(VIBRATION, 1);
        if (vibrationInt == 1)
        {
            isVibration = true;
        }
        else
        {
            isVibration = false;
        }
    }
    #endregion

    #region MUSIC
    public const string MUSIC = "music";
    public static bool isMusic = true;
    public static void SetMusic(bool _isMusic)
    {
        isMusic = _isMusic;
        if (_isMusic)
        {
            PlayerPrefs.SetInt(MUSIC, 1);
        }
        else
        {
            PlayerPrefs.SetInt(MUSIC, 0);
        }
        PlayerPrefs.Save();
    }

    public static void GetMusic()
    {
        int musicInt = PlayerPrefs.GetInt(MUSIC, 1);
        if (musicInt == 1)
        {
            isMusic = true;
        }
        else
        {
            isMusic = false;
        }
    }
    #endregion

    
    public enum GAMESTATE { 
        NONE,
        PRESTART,
        START,
        PLAYING,
        FINISH,
        GAMEOVER,
        PAUSE
    }
    public static GAMESTATE currGameState = GAMESTATE.NONE;
    public enum BALLSKIN
    {
        RED,
        BLUE,
        IMPOSTER,
        CAT
    }
    public static BALLSKIN currBallSkin = BALLSKIN.RED;

    public enum POPUP_ACTION {
        NONE,
        CONTINUE,
        RESTART,
        SKIPLEVEL
    }

    public enum ITEM_TYPE { 
        NONE,
        COIN,
        BALL,
        HEART
    }
    
    public enum BALL_TYPE
    {
        COIN,
        RESCUE,
        PREMIUM
    }


    #region BALL
    public static int currBallID = 1;
    public static InfoBall currInfoBall = null;

    public static InfoBall currInfoBall_Try = null;
    public static ConfigInfoBalls configInfoBalls;
    public static void SetConfigInfoBalls(ConfigInfoBalls _configInfoBalls) {
        configInfoBalls = _configInfoBalls;
    }

    public static InfoBall GetInfoBallFromID(int ballID) {
        for (int i = 0; i < configInfoBalls.infoBalls.Length; i++) {
            if (configInfoBalls.infoBalls[i].id == ballID) {
                return configInfoBalls.infoBalls[i];
            }
        }
        return null;
    }

    public const string BALL = "ball_";
    public static void SetInfoBallUnlock(int idBall) {
        PlayerPrefs.SetInt(BALL + idBall,1);
        PlayerPrefs.Save();
    }

    public static bool GetInfoBallUnlock(int idBall) {
        int ballUnlock = PlayerPrefs.GetInt(BALL + idBall, 0);
        if (ballUnlock == 1) {
            return true;
        }
        return false;
    }

    public static event Action OnChangeActiveBall = delegate () { };
    public const string BALL_ACTIVE = "ball_active";
    public static void SetBallActive(int idBall) {
        
        PlayerPrefs.SetInt(BALL_ACTIVE, idBall);
        PlayerPrefs.Save();

        OnChangeActiveBall.Invoke();
    }

    public static int GetBallActive() {
        return PlayerPrefs.GetInt(BALL_ACTIVE, 1);
    }
    #endregion


    #region RESCUE
    public static int currIDBallRescue = -1;
    #endregion


    #region REMOVE_AD
    public const string REMOVE_AD = "remove_Ad";
    public static void SetRemoveAd()
    {

        PlayerPrefs.SetInt(REMOVE_AD, 1);
        PlayerPrefs.Save();
    }

    public static bool GetRemoveAd()
    {
        if (PlayerPrefs.GetInt(REMOVE_AD, 0) == 1) return true;
        return false;
    }
    #endregion

    public static long GetTimeStamp()
    {

        return (long)(DateTime.UtcNow.Subtract(new DateTime(2020, 1, 1))).TotalSeconds;
    }



    public static int COIN_REWARD = 100;
    public static int FREE_COIN_REWARD = 1000;
    public static int FREE_COIN_REWARD2 = 500;
    #region COIN
    public const string COIN = "coin";
    public static event Action<float> OnChangeCoin = delegate (float _coin) { };
    public static float currCoin;
    public static bool isFinished_AddCoin = true;
    public static void SetCoin(float coinValue)
    {
        PlayerPrefs.SetFloat(COIN, coinValue);
        PlayerPrefs.Save();
        currCoin = coinValue;
        OnChangeCoin(coinValue);
    }

    public static void AddCoin(float addCoinValue)
    {
        if (addCoinValue != 0)
        {
            currCoin = currCoin + addCoinValue;
            Debug.Log(currCoin);
            if (currCoin < 0) currCoin = 0;
            PlayerPrefs.SetFloat(COIN, currCoin);
            PlayerPrefs.Save();
            OnChangeCoin(currCoin);
        }
    }

    public static float GetCoin()
    {
        return PlayerPrefs.GetFloat(COIN, 100);
    }
    #endregion


    public static int FREE_HEART_REWARD = 2;
    #region HEART
    public const string HEART = "heart";
    public static event Action<int> OnChangeHeart = delegate (int _heart) { };
    public static int currHeart;
    public static void SetHeart(int heartValue)
    {
        PlayerPrefs.SetInt(HEART, heartValue);
        PlayerPrefs.Save();
        currHeart = heartValue;
        OnChangeHeart(heartValue);
    }

    public static void AddHeart(int addHeartValue)
    {
        if (addHeartValue != 0)
        {
            currHeart = currHeart + addHeartValue;
            if (currHeart < 0) currHeart = 0;
            PlayerPrefs.SetInt(HEART, currHeart);
            PlayerPrefs.Save();
            OnChangeHeart(currHeart);
        }
    }

    public static int GetHeart()
    {
        return PlayerPrefs.GetInt(HEART, 3);
    }
    #endregion

    #region DAILY_REWARD
    public const string DAILY_REWARD = "daily_reward";
    public const string DAILY_REWARD_TIME = "daily_reward_time";
    public const int MAX_DAILY_REWARD = 7;
    public static int currIndexDailyReward = 0;
    public static void SetDailyReward(int indexDailyReward) {
        SetDailyRewardTime();
        PlayerPrefs.SetInt(DAILY_REWARD, indexDailyReward);
        PlayerPrefs.Save();
    }

    public static int GetDailyReward() {
        return PlayerPrefs.GetInt(DAILY_REWARD, 0);
    }

    public static void SetDailyRewardTime()
    {
        PlayerPrefs.SetInt(DAILY_REWARD_TIME, GetCurrDailyTime());
        PlayerPrefs.Save();
    }

    public static int GetDailyRewardTime() {
        return PlayerPrefs.GetInt(DAILY_REWARD_TIME, 0);
    }

    public static int GetCurrDailyTime() {
        int currDailyTime = DateTime.Now.DayOfYear + DateTime.Now.Year * 10000;
        return currDailyTime;
    }

    public static bool CheckDailyReward() {
        if (GetCurrDailyTime() > GetDailyRewardTime()) {
            if (GetDailyReward() < MAX_DAILY_REWARD)
            {
                return true;
            }
        }
        return false;
    }
    #endregion


    #region IAP
    public const string IAP = "iap_";
    public enum IAP_ID
    {
        blueball_starter_pack,
        premium_pack,
        remove_ad
    }

    public static void SetBuyIAP(IAP_ID idPack) {
        PlayerPrefs.SetInt(IAP + idPack.ToString(), 1);
        PlayerPrefs.Save();
    }

    public static bool GetBuyIAP(IAP_ID idPack) {
        int buyed = PlayerPrefs.GetInt(IAP + idPack.ToString(), 0);
        if (buyed == 1) return true;
        return false;
    }
    #endregion

    public static float TIME_START_PACK = 2 * 60 * 60 + 30 * 60 + 30f;
    public static int interstitialAd_countWin = 1;

    public static string FormatTime(int time)
    {
        int hour = time / 3600;
        int minus = (time - hour * 3600) / 60;
        int second = time - hour * 3600 - minus * 60;
        return String.Format("{0:00}:{1:00}.{2:00}", hour, minus, second);
    }

    public static bool CheckShowStartPack() {
        if (Config.GetBuyIAP(Config.IAP_ID.blueball_starter_pack)) return false;
        int time = Mathf.FloorToInt(Config.TIME_START_PACK - Time.realtimeSinceStartup);
        if (time > 5) {
            return true;
        }
        return false;
    }


    #region LEVEL
    public static int currLevel = 0;
    public const string LEVEL = "level";
    public static void SetLevel(int _level)
    {
        currLevel = _level;
        PlayerPrefs.SetInt(LEVEL, _level);
        PlayerPrefs.Save();
    }

    public static int GetLevel() {
        currLevel = PlayerPrefs.GetInt(LEVEL, 0);
        return currLevel;
    }
    #endregion
}
