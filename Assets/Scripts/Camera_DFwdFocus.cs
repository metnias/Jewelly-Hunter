using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Mathf;

public class Camera_DFwdFocus : Camera_PlatformSnap
{
    private bool right = true;

    public float horzPos = 2f;
    public float horzTurn = 5f;

    protected override void Update()
    {
        Vector3 relative = player.transform.position - transform.position;
        Vector3 target = Vector3.zero;

        // Horizontal: Dual Forward Focus w/ threshold triggered
        if (right)
        {
            if (relative.x > -horzPos) target.x = relative.x + horzPos; // Forward Focus
            else if (relative.x < -horzTurn) right = false; // threshold trigger
        }
        else
        {
            if (relative.x < horzPos) target.x = relative.x - horzPos;
            else if (relative.x > horzTurn) right = true;
        }

        // Apply target
        target.x = Clamp(target.x, -lerpCapSpeed, lerpCapSpeed); // Horizontal Speed Cap
        
        transform.Translate(target);

        base.Update();
    }
}
