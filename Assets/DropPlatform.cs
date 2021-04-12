using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.EndsWith("Ball")) {
            StartCoroutine(WaitAndDrop(0.3f));
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
    }
}
