﻿using UnityEngine;
using System.Collections;
public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong
}

public class SplineWalker : MonoBehaviour {
    public BezierSpline spline;
    public float duration = 5;
    public bool lookForward;
    public SplineWalkerMode mode;

    private float progress;
    private bool goingForward = true;

    void Update()
    {
        if(goingForward)
        {
            progress += Time.deltaTime / duration;
            if(progress > 1f)
            {
                if(mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if(mode == SplineWalkerMode.Loop)
                {
                    progress = 0f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if(progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if(lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }

}
