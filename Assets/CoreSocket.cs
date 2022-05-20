using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CoreSocket : MonoBehaviour
{
    public CoreTypeInfo coreType;
    public CorePowerType corePowerType;
    public GameObject plusImagesObj;
    public Image coreImage;
    public Text coreName;
    public bool isCorePicked;

    public Image fillbar;
    public GarageRefrences UI_Reference;



    private void Start()
    {


    }

    private void OnEnable()
    {
        corePowerType = (CorePowerType)PlayerPrefs.GetInt(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + coreType.ToString() + "PowerType");
        UI_Reference.CoreGenerationScript.onCoreEquiped += SetInfo;
        SetInfo();

      
    }


    private void OnDisable()
    {

        
      // SetInfo();
        UI_Reference.CoreGenerationScript.onCoreEquiped -= SetInfo;

    }


  
    public void SetInfo()
    {

        if (PlayerPrefs.GetInt(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + coreType.ToString() + "CoreType") == 1)
        {

            Debug.Log("Equip  "+ PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + coreType.ToString() + "CoreType");
            SetEquiped();


        }
        else
        {
            Debug.Log("UnEquip");


            SetUnEquiped();



        }
       

    }




     public   void SetEquiped()
     {
        plusImagesObj.SetActive(false);
        coreImage.gameObject.SetActive(true);
        coreImage.sprite = PlayerSelection.Instance.UI_Reference.CoreTypeIconSprite[(int)coreType];
        coreName.text = coreType.ToString();
        isCorePicked = true;


     }
     public  void SetUnEquiped()
     {
        plusImagesObj.SetActive(true);
        coreImage.gameObject.SetActive(false);
        coreImage.sprite = null;
        coreName.text = "";
        isCorePicked = false;


     }






    //void SetStats()
    //{




    //    if (coreType == CoreTypeInfo.Speed)
    //    {
    //            //PlayerSelection.Instance.Players[PlayerSelection.current].Speed = PlayerSelection.Instance.Players[PlayerSelection.current].speedDefault ;

    //            //PlayerSelection.Instance.Players[PlayerSelection.current].Speed += UI_Reference.CorGenerationScript.GetCorePowerAmount(corePowerType);
    //            PlayerPrefs.SetFloat(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + "Speed", PlayerSelection.Instance.Players[PlayerSelection.current].Speed);


    //    }

    //  if (coreType == CoreTypeInfo.Boost)
    //        {



    //        // PlayerSelection.Instance.Players[PlayerSelection.current].Boost += UI_Reference.CorGenerationScript.GetCorePowerAmount(corePowerType);
    //        PlayerPrefs.SetFloat(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + "Boost", PlayerSelection.Instance.Players[PlayerSelection.current].Boost);


    //    }

    //    if (coreType == CoreTypeInfo.Accelration)
    //    {



    //      //  PlayerSelection.Instance.Players[PlayerSelection.current].Accel += UI_Reference.CorGenerationScript.GetCorePowerAmount(corePowerType);
    //        PlayerPrefs.SetFloat(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + "Accel", PlayerSelection.Instance.Players[PlayerSelection.current].Accel);


    //    }

    //    if (coreType == CoreTypeInfo.Shocks)
    //    {


    //      // PlayerSelection.Instance.Players[PlayerSelection.current].Shocks += UI_Reference.CorGenerationScript.GetCorePowerAmount(corePowerType);
    //       PlayerPrefs.SetFloat(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + "Shocks", PlayerSelection.Instance.Players[PlayerSelection.current].Shocks);

    //    }










    //    PlayerSelection.Instance.GetPlayerInfo();




    //}





}
