using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GarageVehicle
{
    public GameObject vehiclePrefab;
    public VehicleStatsScriptableObject vehicleStats;
   
}

public class GarageManager : MonoBehaviour
{
    [Header("Vehicle")]
    public int currentSelectedVehicleIndex = 0;
    [SerializeField]
    private Transform vehiclePos;
    private GameObject currentVehicle;
    [SerializeField]
    private GarageVehicle[] Vehicles;
 
    [SerializeField]
    private GameObject[] coreInUseContainers;
   
    [SerializeField]
    private GameObject[] coresAvailableContainers;
    [Header("Cores")]
    [SerializeField]
    private GameObject[] coresPrefabs;

    private int isFirstTimeGameLoad;


    public delegate void OnVehicleStatUpdateDelegate(VehicleStatsScriptableObject vehicleStat);
    public static event OnVehicleStatUpdateDelegate OnVehicleStatUpdate;

    private void Awake()
    {
        currentSelectedVehicleIndex = PlayerPrefs.GetInt(PlayerPrefKeys.SelectedVehicleIndex,0);
    }

    void Start()
    {
        UIDragItem.OnCorePartAdd += UpdateVehicleStatOnCoreAdd;
        UIDragItem.OnCorePartRemove += UpdateVehicleStatOnCoreRemove;
        UIDragItem.SaveCoreAfterChange += SaveVehicleCores;
        UIDragItem.SaveCoreAfterChange += SaveAvailableCores;
        isFirstTimeGameLoad = PlayerPrefs.GetInt(PlayerPrefKeys.isFirstTimeGameLoad, 1);
        if(isFirstTimeGameLoad == 1)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.isFirstTimeGameLoad, 0);
            SaveAvailableCores();
            SaveVehicleCores();
        }
        else
        {
            LoadAvailableCores();
            LoadVehicleCores();
        }
        ShowVehicle();
        LoadVehicleStat();
        OnVehicleStatUpdate?.Invoke(Vehicles[currentSelectedVehicleIndex].vehicleStats);
    }

    private void UpdateVehicleStatOnCoreAdd(Core core)
    {
        if (core.coreType == CoreType.SPEED)
        {
            Vehicles[currentSelectedVehicleIndex].vehicleStats.speed += core.statValue;
        }
        if (core.coreType == CoreType.ACC)
        {
            Vehicles[currentSelectedVehicleIndex].vehicleStats.acceleration += core.statValue;
        }
        if (core.coreType == CoreType.BOOST)
        {
            Vehicles[currentSelectedVehicleIndex].vehicleStats.boost += core.statValue;
        }

        OnVehicleStatUpdate?.Invoke(Vehicles[currentSelectedVehicleIndex].vehicleStats);
        SaveVehicleStat();
    }

   

    private void UpdateVehicleStatOnCoreRemove(Core core)
    {
        if (core.coreType == CoreType.SPEED)
        {
            Vehicles[currentSelectedVehicleIndex].vehicleStats.speed -= core.statValue;
        }
        if (core.coreType == CoreType.ACC)
        {
            Vehicles[currentSelectedVehicleIndex].vehicleStats.acceleration -= core.statValue;
        }
        if (core.coreType == CoreType.BOOST)
        {
            Vehicles[currentSelectedVehicleIndex].vehicleStats.boost -= core.statValue;
        }
        OnVehicleStatUpdate?.Invoke(Vehicles[currentSelectedVehicleIndex].vehicleStats);
        SaveVehicleStat();
    }

    private void ShowVehicle()
    {
        if (currentVehicle)
            Destroy(currentVehicle);

        currentVehicle = Instantiate(Vehicles[currentSelectedVehicleIndex].vehiclePrefab);
    }

    public int GetVehicleCount()
    {
        return Vehicles.Length;
    }

    public void ShowNextVehicle()
    {
        currentSelectedVehicleIndex++;
        ShowVehicle();
        LoadVehicleStat();
        LoadVehicleCores();
        PlayerPrefs.SetInt(PlayerPrefKeys.SelectedVehicleIndex, currentSelectedVehicleIndex);
        OnVehicleStatUpdate?.Invoke(Vehicles[currentSelectedVehicleIndex].vehicleStats);
    }

    public void ShowPrevVheicle()
    {
        currentSelectedVehicleIndex--;
        ShowVehicle();
        LoadVehicleStat();
        LoadVehicleCores();
        PlayerPrefs.SetInt(PlayerPrefKeys.SelectedVehicleIndex, currentSelectedVehicleIndex);
        OnVehicleStatUpdate?.Invoke(Vehicles[currentSelectedVehicleIndex].vehicleStats);
    }

    private void SaveVehicleStat()
    {
        PlayerPrefs.SetString(Vehicles[currentSelectedVehicleIndex].vehicleStats.vehicleName, JsonUtility.ToJson(Vehicles[currentSelectedVehicleIndex].vehicleStats));
    }

    private void LoadVehicleStat()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(Vehicles[currentSelectedVehicleIndex].vehicleStats.vehicleName), Vehicles[currentSelectedVehicleIndex].vehicleStats);
    }

    private bool isOnce = true;
    private void SaveVehicleCores()
    {
       for(int i = 0; i < coreInUseContainers.Length; i++)
       {
            if (coreInUseContainers[i].GetComponent<UIDropSlot>().currentItem)
            {
                PlayerPrefs.SetInt(Vehicles[currentSelectedVehicleIndex].vehicleStats.vehicleName + "_core" + i, coreInUseContainers[i].GetComponent<UIDropSlot>().currentItem.GetComponent<Core>().coreId);
                if (isFirstTimeGameLoad == 1 && isOnce)
                {
                    UpdateVehicleStatOnCoreAdd(coreInUseContainers[i].GetComponent<UIDropSlot>().currentItem.GetComponent<Core>()); 
                }
            }
            else
            {
               PlayerPrefs.SetInt(Vehicles[currentSelectedVehicleIndex].vehicleStats.vehicleName + "_core" + i, 0);
            }
        }

        isOnce = false;
    }

    private void LoadVehicleCores()
    {
        for (int i = 0; i < coreInUseContainers.Length; i++)
        {
            if (coreInUseContainers[i].GetComponent<UIDropSlot>().currentItem)
            {
                Destroy(coreInUseContainers[i].GetComponent<UIDropSlot>().currentItem.gameObject);
            }
            int coreID =  PlayerPrefs.GetInt(Vehicles[currentSelectedVehicleIndex].vehicleStats.vehicleName + "_core" + i);
            if(coreID != 0)
            {
                var core = Instantiate(coresPrefabs[coreID - 1], coreInUseContainers[i].transform);
                coreInUseContainers[i].GetComponent<UIDropSlot>().currentItem = core.GetComponent<UIDragItem>();
                core.GetComponent<UIDragItem>().currentSlot = coreInUseContainers[i].GetComponent<UIDropSlot>();
                core.GetComponent<RectTransform>().localPosition = Vector2.zero;
            }
        }
    }

    private void SaveAvailableCores()
    {
        for (int i = 0; i < coresAvailableContainers.Length; i++)
        {
            if (coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem)
                PlayerPrefs.SetInt(coresAvailableContainers[i].name + "_core" + i, coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem.GetComponent<Core>().coreId);
            else
                PlayerPrefs.SetInt(coresAvailableContainers[i].name + "_core" + i, 0);


            if (isFirstTimeGameLoad == 1)
            {
                if (coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem)
                {
                    coresAvailableContainers[i].GetComponent<CoreContainer>().coreName.text = coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem.gameObject.GetComponent<Core>().coreType.ToString();
                    coresAvailableContainers[i].GetComponent<CoreContainer>().coreFillBar.fillAmount = coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem.gameObject.GetComponent<Core>().statValue / coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem.gameObject.GetComponent<Core>().maxValue;
                }
            }
        }

        LoadAvailableCores();
    }

    private void LoadAvailableCores()
    {
        for (int i = 0; i < coresAvailableContainers.Length; i++)
        {
            if (coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem)
            {
                Destroy(coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem.gameObject);
            }
            int coreID = PlayerPrefs.GetInt(coresAvailableContainers[i].name + "_core" + i);
            if (coreID != 0)
            {
                var core = Instantiate(coresPrefabs[coreID - 1], coresAvailableContainers[i].transform);
                coresAvailableContainers[i].GetComponent<UIDropSlot>().currentItem = core.GetComponent<UIDragItem>();
                core.GetComponent<UIDragItem>().currentSlot = coresAvailableContainers[i].GetComponent<UIDropSlot>();
                core.GetComponent<RectTransform>().localPosition = core.GetComponent<UIDragItem>().initialPos;
                coresAvailableContainers[i].GetComponent<CoreContainer>().coreName.text = core.GetComponent<Core>().coreType.ToString();
                coresAvailableContainers[i].GetComponent<CoreContainer>().coreFillBar.fillAmount = core.GetComponent<Core>().statValue / core.GetComponent<Core>().maxValue;
            }
        }
    }

    private void OnDestroy()
    {
        UIDragItem.OnCorePartAdd -= UpdateVehicleStatOnCoreAdd;
        UIDragItem.OnCorePartRemove -= UpdateVehicleStatOnCoreRemove;
        UIDragItem.SaveCoreAfterChange -= SaveVehicleCores;
        UIDragItem.SaveCoreAfterChange -= SaveAvailableCores;
    }
}
