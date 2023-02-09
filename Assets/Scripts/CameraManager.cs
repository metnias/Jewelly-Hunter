using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Mathf;

public class CameraManager : MonoBehaviour
{
    public float limitLeft = 0f;
    public float limitRight = 0f;
    public float limitTop = 0f;
    public float limitBottom = 0f;

    private GameObject player;
    private Player_Controller ctrler;
    private bool right = true;

    public GameObject subBackground;
    public float subBackgroundLength = 19.2f;

    public float vertPos = 2f;
    public float vertCap = 3f;
    public float horzPos = 2f;
    public float horzTurn = 5f;
    public float lerpCapSpeed = 0.1f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            throw new NullReferenceException("Player does not exist!");
        }
        ctrler = player.GetComponent<Player_Controller>();
    }

    void Update()
    {
        Vector3 relative = player.transform.position - transform.position;
        Vector3 target = Vector3.zero;

        // Copy from last game because that was good
        // Vertical: Platform Snapping & Camera Window
        if (ctrler != null)
        {
            bool grounded = ctrler.Grounded();
            if (grounded) // Platform Snapping
            {
                target.y = vertPos + relative.y;
            }
            else // Camera Window
            {
                if (relative.y > vertCap)
                {
                    target.y = relative.y - vertCap;
                }
                else if (relative.y < -vertCap)
                {
                    target.y = relative.y + vertCap;
                }
            }
        }

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
        // target = Vector3.Lerp(Vector3.zero, target, lerpSpeed);
        target.x = Clamp(target.x, -lerpCapSpeed, lerpCapSpeed); // Horizontal Speed Cap
        target.y = Min(target.y, lerpCapSpeed); // Vertical Upward Speed Cap
        if (ctrler.Grounded()) target.y = Max(target.y, -lerpCapSpeed);
        
        transform.Translate(target);

        // Clamp pos
        target = transform.position;
        target.x = Clamp(target.x, limitLeft, limitRight);
        target.y = Clamp(target.y, limitBottom, limitTop);
        transform.position = target;


        if (subBackground == null) return;
        relative = transform.position * -0.5f;
        relative.y = Max(0f, relative.y); // to prevent goint to high
        relative.x %= subBackgroundLength;
        if (relative.x < 0f) relative.x += subBackgroundLength; // loop mod
        subBackground.transform.localPosition = relative;
    }
}
