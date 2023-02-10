using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gem_Bops : MonoBehaviour
{
    public float speed = 3f;
    public float amount = 1f;

    private float posY;

    private void Start()
    {
        posY = transform.localPosition.y;
    }


    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
            posY + Mathf.Sin(Time.timeSinceLevelLoad * speed) * amount,
            transform.localPosition.z);
    }

}
