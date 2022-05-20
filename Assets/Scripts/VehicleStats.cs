using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PowerslideKartPhysics;
using TMPro;

public class VehicleStats : MonoBehaviour
{
    [SerializeField]
    private VehicleStatsScriptableObject vehicleStats;
    private Kart kart;

    private void Awake()
    {
        kart = GetComponent<Kart>();
        SetVehicleStats();
    }

    private void Start()
    {
        VehicleDamageSystem.OnVehicleDamage += ReduceStatsOnDamange;
        VehicleDamageSystem.OnVehicleRepair += ResetStatsOnRepair;
    }

    private void SetVehicleStats()
    {
        kart.maxSpeed = vehicleStats.speed;
        kart.acceleration = vehicleStats.acceleration;
        kart.boostSpeedAdd = vehicleStats.boost;
    }

    private void ReduceStatsOnDamange(float totalParts,float totalDamageFacor) 
    {
        // Speed value calculation to reduce on single hit
        float speedReduceValue = vehicleStats.speed;    //  we will reduce only speed on damage
        speedReduceValue /= totalDamageFacor;
        speedReduceValue /= totalParts;
        kart.maxSpeed -= speedReduceValue;
    }

    private void ResetStatsOnRepair()
    {
        SetVehicleStats();
    }

    private void OnDestroy()
    {
       VehicleDamageSystem.OnVehicleDamage -= ReduceStatsOnDamange;
       VehicleDamageSystem.OnVehicleRepair -= ResetStatsOnRepair;
    }
}
