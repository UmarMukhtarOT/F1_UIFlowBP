using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CoreTypeInfo
{
    Speed, Boost, Accelration, Shocks
}

public enum CorePowerType
{
    Epic, Rare, Legendary
}

public class CoreHandler : MonoBehaviour
{
    public CoreTypeInfo coreType;
    public CorePowerType powerType;

    //[HideInInspector]
 

    public Image coreImagePlaceholder;
    public Text coreTypeText;
    public Text corePowerTypeText;
    public Button myselfButton;
    public Image fillbar;
    public string willGetMessage;



    private void Start()
    {

        

        SetCorePower();
    }

    void SetCorePower()
    {



        switch (powerType)
        {
            case CorePowerType.Epic:
                fillbar.fillAmount = 0.25f;
              
                break;

            case CorePowerType.Rare:
                fillbar.fillAmount = 0.45f;

               

                break;
            case CorePowerType.Legendary:
                fillbar.fillAmount = 0.80f;

              
                break;
            default:
                break;
        }
    }




    public void ButtonListener()
    {

        Debug.Log("powerType is " + powerType);
        PlayerSelection.Instance.coreGenScript.ShowCoreEquipPopUp(coreType, powerType, willGetMessage,this.gameObject);


    }




}
