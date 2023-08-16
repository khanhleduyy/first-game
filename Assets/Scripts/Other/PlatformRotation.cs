using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    private float rotZ;
    public float rotationSpeed;
    public bool clockWiseRot;
    private void Update()
    {
        if (!clockWiseRot)
        {
            rotZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotZ += -Time.deltaTime * rotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
