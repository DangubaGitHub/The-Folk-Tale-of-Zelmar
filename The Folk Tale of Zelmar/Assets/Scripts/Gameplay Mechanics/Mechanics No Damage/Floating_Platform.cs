using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Platform : MonoBehaviour
{
    public Transform[] points;

    [SerializeField] float speed;
    [SerializeField] Transform platform;

    int currentPoint;

    Red_Button red_Button_Script;
    [SerializeField] GameObject RedButton;

    private void Awake()
    {
        red_Button_Script = RedButton.GetComponent<Red_Button>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (red_Button_Script.isPushed)
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
}
