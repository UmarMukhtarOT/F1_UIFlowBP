using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMultiDirection : MonoBehaviour
{
    public bool x, y, z;

    [SerializeField]
    private float rotationSpeed;

    void FixedUpdate()
    {
        if(x)
            transform.Rotate(rotationSpeed, 0f, 0f);
        
        if(y)
            transform.Rotate(0f, rotationSpeed, 0f);

        if(z)
            transform.Rotate(0f, 0f, rotationSpeed);
    }
}
