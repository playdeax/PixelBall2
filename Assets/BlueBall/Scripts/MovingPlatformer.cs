using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatformer : MonoBehaviour
{
    public PlatformType platformType;

    public List<Transform> points;
    public float speed = 1;
    int currentPoint = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float t;
    void Update()
    {
        if(platformType == PlatformType.Moving)
        {
            if(Vector2.Distance(transform.position, points[currentPoint].position) > 0.01f)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, step);
            }
            else
            {
                if(currentPoint < points.Count - 1)
                {
                    currentPoint++;
                }
                else
                {
                    points.Reverse();
                    currentPoint = 1;
                }
            }
        }
    }
}

public enum PlatformType
{
    Moving,
    None
}
