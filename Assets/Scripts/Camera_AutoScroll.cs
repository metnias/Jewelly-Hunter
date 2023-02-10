using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Mathf;

public class Cemra_AutoScroll : Camera_PlatformSnap
{
    public float scrollSpeed;

    protected override void Update()
    {
        // Horizontal: AutoScroll
        Vector3 target = Vector3.right * scrollSpeed * Time.deltaTime;
        transform.Translate(target);

        base.Update();
    }
}