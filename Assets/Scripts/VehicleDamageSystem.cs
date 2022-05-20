using UnityEngine;
using System;

public class VehicleDamageSystem : MonoBehaviour
{
    private float delayTimeDamage = 1f;
    private float nextTimeDamage =  0f;

    private float partHealthReductionOnHit = 5;
    private float forceOnPart = 12f;

    private float totalDamageFactor = 3f; // total percentage to reduce in car stats

    [SerializeField]
    private ParticleSystem repairParticles;
    [SerializeField]
    private ParticleSystem damageParticles;

    public DamageAbleParts[] damageAbleParts;


    public delegate void OnVehicleDamageDelegate(float totalParts,float totalDamageFactor);
    public static event OnVehicleDamageDelegate OnVehicleDamage;

    public delegate void OnVehicleRepairDelegate();
    public static event OnVehicleRepairDelegate OnVehicleRepair;

    
    private void Start()
    {
        Initilize();
    }

    private void Initilize()
    {
        for (int i = 0; i < damageAbleParts.Length; i++)
        {
            damageAbleParts[i].health = partHealthReductionOnHit * (i + 1); // health is multiple of partHealthReductionOnHit
            damageAbleParts[i].partPos = damageAbleParts[i].part.transform.localPosition;
            //damageAbleParts[i].part.AddComponent<Rigidbody>().isKinematic = true;
            //damageAbleParts[i].part.AddComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.TrackBoundry && Time.time > nextTimeDamage)
        {
            nextTimeDamage = Time.time + delayTimeDamage;
            for (int i = 0; i < damageAbleParts.Length; i++)
            {
                if (!damageAbleParts[i].isDamaged)
                {
                    damageAbleParts[i].health -= partHealthReductionOnHit;
                    if (damageAbleParts[i].health <= 0)
                    {
                        var damageParticlesPos = damageAbleParts[i].part.transform.position;
                        damageParticlesPos.y += 1f;
                        damageParticles.transform.position = damageParticlesPos;
                        damageParticles.Play();
                        damageAbleParts[i].part.GetComponent<Rigidbody>().isKinematic = false;
                        damageAbleParts[i].part.GetComponent<Rigidbody>().useGravity = true;
                        damageAbleParts[i].part.transform.SetParent (null);
                        damageAbleParts[i].part.GetComponent<BoxCollider>().enabled = true;
                        //damageAbleParts[i].part.GetComponent<Rigidbody>().AddForce(Vector3.up * forceOnPart, ForceMode.Impulse);
                        //damageAbleParts[i].part.GetComponent<Rigidbody>().AddTorque(Vector3.up * forceOnPart, ForceMode.Impulse);
                        damageAbleParts[i].isDamaged = true;
                        OnVehicleDamage?.Invoke(damageAbleParts.Length, totalDamageFactor);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.PitRepair)
        {
            RepairCar();
        }
    }

    private void RepairCar()
    {
        repairParticles.Play();
        for (int i = 0; i < damageAbleParts.Length; i++)
        {
            damageAbleParts[i].part.GetComponent<Rigidbody>().isKinematic = true;
            damageAbleParts[i].part.transform.localPosition = damageAbleParts[i].partPos;
            damageAbleParts[i].part.SetActive(true);
            damageAbleParts[i].isDamaged = false;
            damageAbleParts[i].health = partHealthReductionOnHit * (i + 1); // health is multiple of partHealthReductionOnHit
        }
        OnVehicleRepair?.Invoke();
    }

}

[Serializable]
public class DamageAbleParts
{
    public GameObject part;
    public Vector3 partPos;
    public float health;
    public bool isDamaged;
}
