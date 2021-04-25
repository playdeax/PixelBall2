using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Ins;
    public ConfigInfoBalls configInfoBalls;
    private void Awake()
    {
        Ins = this;
        DontDestroyOnLoad(gameObject);

        Config.SetConfigInfoBalls(configInfoBalls);
    }
    // Start is called before the first frame update
    void Start()
    {
        Config.currBallID = Config.GetBallActive();
        Config.currInfoBall = Config.GetInfoBallFromID(Config.currBallID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Button("Reset SCENE")]
    public void TouchResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
