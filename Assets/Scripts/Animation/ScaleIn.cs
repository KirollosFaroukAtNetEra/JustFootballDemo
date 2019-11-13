using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleIn : AnimationBehaviour
{
    public float startSize = 0;
    public float targetSize = 2;
    public float minSize = 0.25f;
    public float maxSize = 1;

    public float speed = 6.0f;

    private Vector3 targetScale;
    private Vector3 baseScale;
    private float currScale;

    void OnEnable()
    {
        transform.localScale=new Vector3(0.5f,0.5f,0.5f);
        baseScale = transform.localScale;
        transform.localScale = baseScale * startSize;
        currScale = startSize;
        targetScale = baseScale * targetSize;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
    }

    public void ChangeSize(bool bigger)
    {

        if (bigger)
            currScale++;
        else
            currScale--;

        currScale = Mathf.Clamp(currScale, minSize, maxSize + 1);

        targetScale = baseScale * currScale;
    }
}
