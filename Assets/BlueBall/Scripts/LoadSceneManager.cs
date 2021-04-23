using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        Config.GetSound();
        Config.GetMusic();
        Config.GetVibration();
        
        Config.currCoin = Config.GetCoin();
        Config.currHeart = Config.GetHeart();
        //Config.SetLevel(6);
    
        StartCoroutine(LoadMenuScene_IEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public IEnumerator LoadMenuScene_IEnumerator() {
        yield return new WaitForSeconds(2f);
        LoadMenuScene();
    }
    public bool isLoadMenu = false;
  
    public virtual void LoadMenuScene() {
        if (!isLoadMenu)
        {
            isLoadMenu = true;
            if (Config.currLevel == 1)
            {
                SceneManager.LoadSceneAsync("Home");

            }
            else
            {
                SceneManager.LoadSceneAsync("Home");
            }
        }
    }
}
