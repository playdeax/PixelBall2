using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint_Finish : MonoBehaviour
{
    public ParticleSystem efxFinsih;
    private Collider2D pointCollider2D;
    private void Awake()
    {
        pointCollider2D = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOpenFinishOpen() {
        gameObject.SetActive(true);
        efxFinsih.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) {
            pointCollider2D.enabled = false;
            GameLevelManager.Ins.SetGameFinish();
        }
    }


}
