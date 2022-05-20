using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleRepositionHandler : MonoBehaviour
{
    [SerializeField]
    private Transform vehicleRotator;

    

    private Transform lastCheckPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tags.CheckpointReposition)
        {
            lastCheckPoint = other.gameObject.transform;
        }
    }

    private bool isOnce = true;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == Tags.Ground && isOnce)
        {
            isOnce = false;
            StartCoroutine("ReSpawnCoroutine");
        }
    }

    private void RepositionVehicle()
    {
        transform.position = lastCheckPoint.position;
        vehicleRotator.rotation = lastCheckPoint.rotation;
        isOnce = true;
    }

    IEnumerator ReSpawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        RepositionVehicle();
    }
}
