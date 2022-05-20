using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
    private Collider trig;

    [SerializeField]
    private Renderer[] arrRend;
    [SerializeField]
    private float coolDownTime = 1.0f;

    void Start()
    {
        trig = GetComponent<Collider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag!=Tags.Missile)
        {
            Debug.Log("CoolDown");
            StartCoroutine("CoolDownCorotine");
        }
    }

    IEnumerator CoolDownCorotine()
    {
        foreach (var rend in arrRend)
        {
            rend.enabled = false;
        }
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(coolDownTime);
        foreach (var rend in arrRend)
        {
            rend.enabled = true;
        }
        GetComponent<Collider>().enabled = true;

    }
}
