using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject magnetCollider;

    public static BallManager Ins;

    private void Awake()
    {
        Ins = this;
    }
    void Start()
    {
        magnetCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            GameLevelManager.Ins.SetBallDead();
        }
    }
}
