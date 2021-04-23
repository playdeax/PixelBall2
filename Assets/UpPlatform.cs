using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpPlatform : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public GameObject platform;
    public float timeMove = 1;
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.EndsWith("Ball"))
        {
            Up();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.EndsWith("Ball"))
        {
            Down();
        }
    }

    public void Up()
    {
        platform.transform.DOLocalMove(endPoint.localPosition, timeMove).SetEase(Ease.Linear);
    }

    public void Down()
    {
        platform.transform.DOLocalMove(startPoint.localPosition, timeMove).SetEase(Ease.Linear);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
