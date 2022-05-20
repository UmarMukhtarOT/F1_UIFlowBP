
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("SCPS/SCPSContainer")]
public class SCPSContainer : MonoBehaviour
{

   
    public List<Transform> wp = new List<Transform>();
    public Transform target;
    public float WaypointRadius = 15f;
    public Material SphereMaterial;
    void OnDrawGizmos()
    {
        //Create a big sphere to show all the waypoints
        for (int i = 0; i < wp.Count; i++)
        {
            Gizmos.color = new Color(1, 0, 1, 0.4f);
            if (i == wp.Count-1) {
                Gizmos.color = new Color(1, 1, 1, 0.4f); 
            }
            Gizmos.DrawSphere(wp[i].transform.position, WaypointRadius);
            

            if (i < wp.Count - 1)
            {

                if (wp[i] && wp[i + 1])
                {

                    if (wp.Count > 0)
                    {

                        Gizmos.color = new Color(0.5f,0.5f,1);

                        if (i < wp.Count - 1)
                            Gizmos.DrawLine(wp[i].position, wp[i + 1].position);
                        if (i < wp.Count - 2)
                           Gizmos.DrawLine(wp[wp.Count - 1].position, wp[0].position);

                    }

                }

            }

        }

    }

}
