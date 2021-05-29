using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    bool firebaseInitialized = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Start()
    {
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        //});
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
                
            }
            else
            {
                Debug.LogError(
                 "Error Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    void InitializeFirebase()
    {
        Debug.Log("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        firebaseInitialized = true;

        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Debug.Log("Firebase Messaging Initialized");
        InitRemoteConfig();
        Debug.Log("Firebase RemoteConfig Initialized");
        // This will display the prompt to request permission to receive
        // notifications if the prompt has not already been displayed before. (If
        // the user already responded to the prompt, thier decision is cached by
        // the OS and can be changed in the OS settings).
        Firebase.Messaging.FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
          task => {
              LogTaskCompletion(task, "RequestPermissionAsync");
          }
        );


        if (LoadSceneManager.instance.isActiveAndEnabled) {
            LoadSceneManager.instance.LoadMenuScene();
        }
    }



    private void InitRemoteConfig()
    {
        
#if UNITY_EDITOR
        var configSettings = new ConfigSettings();
        configSettings.MinimumFetchInternalInMilliseconds = 0;
        FirebaseRemoteConfig.DefaultInstance.SetConfigSettingsAsync(configSettings).ContinueWithOnMainThread(task1 =>
        {
            
#endif
            Debug.Log("start Init RemoteConfig");
            Dictionary<string, object> defaults =
                new Dictionary<string, object>();
            defaults.Add("TIME_NO_INTERSTITIAL_AFTER_SHOW_REWARDED", 30);
            defaults.Add("MIN_LEVEL_SHOW_INTERSTITIAL", 2);
            defaults.Add("NUMBER_LEVEL_PER_SHOW_INTERSTITIAL", 2);

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                .ContinueWithOnMainThread(task => { FetchDataAsync(); });

#if UNITY_EDITOR

        });
#endif
       
    }

    private void  FetchDataAsync() {
        Debug.Log("Set Default finish");
        System.Threading.Tasks.Task fetchTask =
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                TimeSpan.Zero).ContinueWithOnMainThread(FetchComplete);
        FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync().ContinueWithOnMainThread(task =>
        {
           
        });
        
    }
    void FetchComplete(Task fetchTask) {
        if (fetchTask.IsCanceled) {
            Debug.Log("Fetch canceled.");
        } else if (fetchTask.IsFaulted) {
            Debug.Log("Fetch encountered an error.");
        } else if (fetchTask.IsCompleted) {
            Debug.Log("Fetch completed successfully!");
        }

        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus) {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                    .ContinueWithOnMainThread(task => {
                        Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                            info.FetchTime));
                        var values = FirebaseRemoteConfig.DefaultInstance.AllValues;
                        Config.TIME_NO_INTERSTITIAL_AFTER_SHOW_REWARDED = values["TIME_NO_INTERSTITIAL_AFTER_SHOW_REWARDED"].LongValue;
                        Config.MIN_LEVEL_SHOW_INTERSTITIAL = values["MIN_LEVEL_SHOW_INTERSTITIAL"].LongValue;
                        Config.NUMBER_LEVEL_PER_SHOW_INTERSTITIAL = values["NUMBER_LEVEL_PER_SHOW_INTERSTITIAL"].LongValue;
                        Config.MIN_LEVEL_SHOW_REWARD_BUTTON = values["MIN_LEVEL_SHOW_REWARD_BUTTON"].LongValue;
                        Debug.Log("fetch finish");
                        Debug.Log("Config.TIME_NO_INTERSTITIAL_AFTER_SHOW_REWARDED = "+Config.TIME_NO_INTERSTITIAL_AFTER_SHOW_REWARDED);
                        Debug.Log("Config.MIN_LEVEL_SHOW_INTERSTITIAL = "+Config.MIN_LEVEL_SHOW_INTERSTITIAL);
                        Debug.Log("Config.NUMBER_LEVEL_PER_SHOW_INTERSTITIAL = "+Config.NUMBER_LEVEL_PER_SHOW_INTERSTITIAL);
                    });

                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason) {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason");
                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                }
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending.");
                break;
        }
    }
    public void LogLevelStart(int level)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Lv_" + level + "_S");
        }
    }
    public void LogLevelLose(int level)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Lv_" + level + "_F");
        }
    }
    public void LogLevelWin(int level)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Lv_" + level + "_W");
        }
    }

    public void LogLevelDead(int level)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Lv_" + level + "_D");
        }
    }
    public void LogShowInter(int curentLevel)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Ads_Inter", "CurentLevel", curentLevel);
        }
    }
    public void LogShowReward(int curentLevel)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Ads_Reward", "CurentLevel", curentLevel);
        }
    }
    public void LogRewarded()
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Ads_Rewarded");
        }
    }
    public void LogShowNative(int curentLevel)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Ads_Native", "CurentLevel", curentLevel);
        }
    }

    public const string POWERUP_UNDO = "Undo";
    public const string POWERUP_HINT = "Hint";
    public const string POWERUP_SHUFFLE = "Shuffle";
    public const string POWERUP_REMOVEBOMB = "RemoveBomb";
    public const string POWERUP_ADDCOL = "AddCol";
    public void LogUsePowerUp(string type, int curentLevel)
    {
        if (firebaseInitialized)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("PowerUp_" + type, "CurentLevel", curentLevel);
        }
    }


    #region FIREBASE MESSAGE
    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message");
        var notification = e.Message.Notification;
        if (notification != null)
        {
            Debug.Log("title: " + notification.Title);
            Debug.Log("body: " + notification.Body);
            var android = notification.Android;
            if (android != null)
            {
                Debug.Log("android channel_id: " + android.ChannelId);
            }
        }
        if (e.Message.From.Length > 0)
            Debug.Log("from: " + e.Message.From);
        if (e.Message.Link != null)
        {
            Debug.Log("link: " + e.Message.Link.ToString());
        }
        if (e.Message.Data.Count > 0)
        {
            Debug.Log("data:");
            foreach (System.Collections.Generic.KeyValuePair<string, string> iter in
                     e.Message.Data)
            {
                Debug.Log("  " + iter.Key + ": " + iter.Value);
            }
        }
    }

    public virtual void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);

    }

    // Log the result of the specified task, returning true if the task
    // completed successfully, false otherwise.
    protected bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled)
        {
            Debug.Log(operation + " canceled.");
        }
        else if (task.IsFaulted)
        {
            Debug.Log(operation + " encounted an error.");
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string errorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    errorCode = String.Format("Error.{0}: ",
                      ((Firebase.Messaging.Error)firebaseEx.ErrorCode).ToString());
                }
                Debug.Log(errorCode + exception.ToString());
            }
        }
        else if (task.IsCompleted)
        {
            Debug.Log(operation + " completed");
            complete = true;
        }
        return complete;
    }
    #endregion

}
