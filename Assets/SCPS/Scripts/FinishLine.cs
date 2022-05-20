using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class FinishLine : MonoBehaviour
{ 

    private void OnTriggerEnter(Collider car)
    {
        Debug.Log(car.gameObject.name);
        SCPSManager scps = FindObjectOfType<SCPSManager>();
        if (scps.UsesFinishLapCollider)
        {
            scps.HittedFinishLine(car);
        }
    }
}
