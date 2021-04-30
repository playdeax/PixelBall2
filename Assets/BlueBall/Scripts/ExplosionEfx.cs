using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionEfx : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public void PlayExplosion()
    {
        float RandomForceX = Random.Range(-2f, 2f);
        float RandomForceY = Random.Range(1f, 4f);
        float RandomRotate = Random.Range(48f, 180f);
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.AddForce(new Vector2(RandomForceX, RandomForceY), ForceMode2D.Impulse);
        rigidbody2D.AddTorque(RandomRotate, ForceMode2D.Impulse);
    }

    private IEnumerator AutoHide()
    {
        yield return  new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
