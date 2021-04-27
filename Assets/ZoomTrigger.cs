using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ZoomTrigger : MonoBehaviour
{
    public PixelPerfectCamera perfectCamera;
    public Transform ballTransform;
    public Transform targetTransform;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            StartCoroutine(MoveOnCamera());
        }
    }

    public IEnumerator TurnBackCamera()
    {
        yield return new WaitForSeconds(2.2f);
        perfectCamera.transform.GetComponent<ProCamera2D>().CameraTargets[0].TargetTransform = ballTransform;
        perfectCamera.transform.GetComponent<ProCamera2D>().CameraTargets[0].TargetOffset = new Vector2(0, 0);
        Destroy(gameObject);
    }
    public IEnumerator MoveOnCamera()
    {
        yield return new WaitForSeconds(0.3f);
        perfectCamera.transform.GetComponent<ProCamera2D>().CameraTargets[0].TargetTransform = targetTransform;
        perfectCamera.transform.GetComponent<ProCamera2D>().CameraTargets[0].TargetOffset = new Vector2(0, 12);
        StartCoroutine(TurnBackCamera());
    }
}
