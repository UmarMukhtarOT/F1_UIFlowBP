using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerslideKartPhysics;

public class PlayerSpwanManager : MonoBehaviour
{

    #region Constant
    // Put your code here
    #endregion

    #region Static Variables
    // Put your code here
    #endregion

    #region Editor-assigned variables
    [Header("Player Spawn Point")]
    [SerializeField] Transform spawnPoint;
    [Space]
    [Header("Player Prefabs")]
    [SerializeField] private GameObject[] playerPrefab;
    [Space]
    [Header("Other Attributes")]
    [SerializeField] private bool useAntiGrav = false;
    [SerializeField]  private GameObject uiContainer;
    [SerializeField] private KartCamera kartCam;
    [SerializeField] private KartGravityPreset antiGravPreset;
    #endregion


    public delegate void OnPlayerSpwanDelegate(GameObject playerObj);
    public static event OnPlayerSpwanDelegate OnPlayerSpawn;
  
    void Start()
    {
        SpawnKart(playerPrefab[PlayerPrefs.GetInt(PlayerPrefKeys.SelectedVehicleIndex,0)]);
    }

    public void SpawnKart(GameObject kart)
    {
        // Spawn a given kart at the spawn point
        Kart newKart = null;
        if (kart != null)
        {
            //newKart = Instantiate(kart, spawnPoint, Quaternion.LookRotation(spawnDir.normalized, Vector3.up)).GetComponent<Kart>();

            newKart = Instantiate(kart, spawnPoint.position,Quaternion.identity).GetComponent<Kart>();
            newKart.transform.rotation = spawnPoint.rotation;

            OnPlayerSpawn?.Invoke(newKart.gameObject);

            // Set anti-gravity mode
            if (useAntiGrav && newKart.GetComponent<KartPresetControl>() != null)
            {
                newKart.GetComponent<KartPresetControl>().LoadGravityPreset(antiGravPreset);
                newKart.GetComponent<Rigidbody>().useGravity = false;
            }
        }

        // Show the UI and connect it to the spawned kart
        if (uiContainer != null)
        {
            UIControl uiController = uiContainer.GetComponent<UIControl>();
            if (uiController != null)
            {
                uiController.Initialize(newKart);
            }

            uiContainer.SetActive(true);
        }

        // Connect the camera to the spawned kart
        if (kartCam != null)
        {
            kartCam.Initialize(newKart);
        }

        gameObject.SetActive(false);
    }
}
