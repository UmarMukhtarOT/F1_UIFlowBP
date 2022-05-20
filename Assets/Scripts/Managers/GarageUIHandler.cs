using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageUIHandler : MonoBehaviour
{
    [Header("Car Stats")]
 
    [SerializeField]
    private Text txtSpeed;
    [SerializeField]
    private Text txtAcceleration;
    [SerializeField]
    private Text txtBoost;

    [SerializeField]
    private Image barSpeed;
    [SerializeField]
    private Image barAcceleration;
    [SerializeField]
    private Image barBoost;

    [Header("Core Stats")]
    
    [SerializeField]
    private Text coreType;
    [SerializeField]
    private Text coreValue;

    [Header("Chabge Car Buttons")]
    [SerializeField]
    private Button btnNextVehicle;
    [SerializeField]
    private Button btnPrevVehicle;

    private GarageManager garageManager;

    private void Awake()
    {
        garageManager = FindObjectOfType<GarageManager>();
        UIDragItem.PointerDownOnCore += SetCoreStats;
        GarageManager.OnVehicleStatUpdate += UpdateCarStatUI;
    }

    private void Start()
    {
        SetPrevNextButtonsOnStart();
    }

    private void SetPrevNextButtonsOnStart()
    {
        if (garageManager.currentSelectedVehicleIndex >= garageManager.GetVehicleCount() - 1)
        {
            btnNextVehicle.GetComponent<Button>().interactable = false;
        }

        if (garageManager.currentSelectedVehicleIndex <= 0)
        {
            btnPrevVehicle.GetComponent<Button>().interactable = false;
        }
    }

    public void UpdateCarStatUI(VehicleStatsScriptableObject vehicleStat)
    {
        txtSpeed.text = vehicleStat.speed.ToString();
        txtAcceleration.text = vehicleStat.acceleration.ToString();
        txtBoost.text = vehicleStat.boost.ToString();

        barSpeed.fillAmount = vehicleStat.speed / 100f;
        barAcceleration.fillAmount = vehicleStat.acceleration / 2f;
        barBoost.fillAmount = vehicleStat.boost / 25f;
    }

    private void SetCoreStats(string type, float amount)
    {
        coreType.text = type;
        coreValue.text = amount.ToString();
    }

    public void OnBtnClickNext()
    {
        garageManager.ShowNextVehicle();
        if(garageManager.currentSelectedVehicleIndex >= garageManager.GetVehicleCount() - 1)
        {
            btnNextVehicle.GetComponent<Button>().interactable = false;
        }

        if(garageManager.currentSelectedVehicleIndex != 0 && garageManager.currentSelectedVehicleIndex < garageManager.GetVehicleCount() - 1)
        {
            btnNextVehicle.GetComponent<Button>().interactable = true;
            btnPrevVehicle.GetComponent<Button>().interactable = true;
        }
    }

    public void OnBtnClickPrev()
    {
        garageManager.ShowPrevVheicle();
        if (garageManager.currentSelectedVehicleIndex <= 0)
        {
            btnPrevVehicle.GetComponent<Button>().interactable = false;
        }

        if (garageManager.currentSelectedVehicleIndex != 0 && garageManager.currentSelectedVehicleIndex < garageManager.GetVehicleCount() - 1)
        {
            btnNextVehicle.GetComponent<Button>().interactable = true;
            btnPrevVehicle.GetComponent<Button>().interactable = true;
        }
    }

    public void OnBtnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        UIDragItem.PointerDownOnCore -= SetCoreStats;
        GarageManager.OnVehicleStatUpdate -= UpdateCarStatUI;
    }
}