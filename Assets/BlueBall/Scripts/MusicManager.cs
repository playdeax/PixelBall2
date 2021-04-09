using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioMusic;

    public List<AudioClip> listBgMusic;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        audioMusic = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (Config.isMusic)
        {
            if(!MusicManager.instance.audioMusic.isPlaying)
                MusicManager.instance.PlayMusicBG();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusicBG() {
        if (Config.isMusic)
        {
            if (audioMusic.clip == null)
            {
                int k = Random.Range(0, listBgMusic.Count);
                audioMusic.clip = listBgMusic[k];
                audioMusic.loop = true;
                audioMusic.Play();
            }
            else
            {
                audioMusic.Play();
            }
        }
    }

    public void StopMusicBG() {
        audioMusic.Stop();
    }
}
