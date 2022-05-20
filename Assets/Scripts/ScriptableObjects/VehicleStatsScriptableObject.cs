using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VehicleStats", menuName = "ScriptableObjects/VehicleStats", order = 51)]
public class VehicleStatsScriptableObject : ScriptableObject
{
    public string vehicleName;
    public float speed;
    public float acceleration;
    public float boost;
}
