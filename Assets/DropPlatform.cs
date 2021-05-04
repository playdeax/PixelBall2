using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    Vector3 oldPos;
    public bool isBack;
    public float timeBack = 2f;
    public float timeHold = 0.5f;
    void Start()
    {
        oldPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.EndsWith("Ball"))
        {
            StartCoroutine(WaitAndDrop(timeHold));
        }
    }
  
    void Update()
    {
        
    }
    private IEnumerator WaitAndDrop(float waitTime)
    {
       yield return new WaitForSeconds(waitTime);
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.GetComponent<Rigidbody2D>().gravityScale = 2;
       transform.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(timeBack);
        if (isBack)
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.GetComponent<Collider2D>().enabled = true;
            transform.position = oldPos;
        }
    }
}
