using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : AnimationBehaviour
{
    public float shake_speed = 15;
    public float shake_intensity = 10;
    public float amountOverTime = 8;

    public Vector3 originPosition;

    public bool isShaking = true;

    void Start()
    {
        originPosition = transform.localPosition;
    }
    public override void StopAnimate()
    {
        isShaking = false;
        base.StopAnimate();
    }
    void Update()
    {
        if (isShaking)
        {
            shake_speed = shake_speed + amountOverTime;
            float step = shake_speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originPosition + Random.insideUnitSphere * shake_intensity, step);

        }
    }
}
