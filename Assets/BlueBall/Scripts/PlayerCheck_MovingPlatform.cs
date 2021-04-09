using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck_MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2DOnCollisionEnter2DOnCollisionEnter2D");
        if (collision.gameObject.CompareTag("MovingPlatform")) {
            Debug.Log("MovingPlatformMovingPlatformMovingPlatform");
            gameObject.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit2DOnCollisionExit2DOnCollisionExit2D");
        gameObject.transform.parent = null;
    }
}
