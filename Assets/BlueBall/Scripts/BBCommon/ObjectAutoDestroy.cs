using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAutoDestroy : MonoBehaviour
{
    public float timeAutoDestroy = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AutoDestroy() {
        yield return new WaitForSeconds(timeAutoDestroy);
        Destroy(gameObject);
    }
}
