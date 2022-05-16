using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Platform_No_Button : MonoBehaviour
{
    public Transform[] points;

    [SerializeField] float speed;
    [SerializeField] Transform platform;

    int currentPoint;

    void Start()
    {

    }

    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(platform.position, points[currentPoint].position) < 1.65f)
        {
            currentPoint++;

            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
