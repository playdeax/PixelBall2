using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public SpriteRenderer doorOpen;
    public FinishPoint_Key key;
    // Start is called before the first frame update
    void Start()
    {
        //doorOpen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOpenDoor() {
       // key.gameObject.SetActive(true);
    }


}
