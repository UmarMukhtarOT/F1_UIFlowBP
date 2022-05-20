using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class CoreGenerationHandler : MonoBehaviour
{
    public CoreDataContainerScriptable coreDataContainerScriptable;
    public List<GameObject> SpawnedCores;

    public CoreHandler corePrefab;
    public Transform coreParent;

    [Header("Just for testing")]
    public CoreTypeInfo testingCoreTypeInfo;
    public CorePowerType testingCorePowerType;
    public int testingamout;
    public GarageRefrences UI_Reference;
    public PlayerSelection playerSelectionScript;

    public delegate void OnCoreEquiped();
    public OnCoreEquiped onCoreEquiped;
    public CoreSocket [] socket;
    public int socketNumber;



   



    void Start()
    {
        GenerateAllCores();
    }




    public void GenerateAllCores()
    {
        for (int i = 0; i < SpawnedCores.Count; i++)
        {
            SpawnedCores[i].gameObject.SetActive(false);
        }
        SpawnedCores.Clear();

        for (int i = 0; i < coreDataContainerScriptable.allCoreData.Count; i++)
        {
            for (int j = 0; j < coreDataContainerScriptable.allCoreData[i].noOfCore; j++)
            {
                CoreHandler NewCore = Instantiate(corePrefab, coreParent);
                NewCore.coreType = coreDataContainerScriptable.allCoreData[i].coreType;
                NewCore.powerType = coreDataContainerScriptable.allCoreData[i].corePower;
                NewCore.coreImagePlaceholder.sprite = coreDataContainerScriptable.allCoreData[i].coreSprite;
                NewCore.coreTypeText.text = coreDataContainerScriptable.allCoreData[i].coreType.ToString();
                NewCore.corePowerTypeText.text = coreDataContainerScriptable.allCoreData[i].corePower.ToString();
                NewCore.willGetMessage = coreDataContainerScriptable.allCoreData[i].willGetMessage;
                

                NewCore.name = NewCore.coreType.ToString() + NewCore.powerType.ToString();
                SpawnedCores.Add(NewCore.gameObject);
            }

        }
    }

    public void AddCoreInInventory(CoreTypeInfo coreType, CorePowerType corePowerType, int NoOfCores)
    {
        for (int i = 0; i < coreDataContainerScriptable.allCoreData.Count; i++)
        {
            if (coreDataContainerScriptable.allCoreData[i].coreType == coreType && coreDataContainerScriptable.allCoreData[i].corePower == corePowerType)
            {
                coreDataContainerScriptable.allCoreData[i].noOfCore += NoOfCores;
            }
        }
        GenerateAllCores();
    }
    

    public void AddintionInInventoryTesting()
    {
        AddCoreInInventory(testingCoreTypeInfo, testingCorePowerType, testingamout);
    }

    CoreTypeInfo coreTypeForEquip;
    CorePowerType corePowerTypeForEquip;
    float coreAmountTO_Equip;
    GameObject CurrentBtn;


    public void ShowCoreEquipPopUp(CoreTypeInfo coreType, CorePowerType powerType, string YouWillGetMessage, GameObject ClickedBtn)
    {


        for (int i = 0; i < socket.Length; i++)
        {

            if (socket[i].coreType==coreType)
            {
                if (socket[i].isCorePicked == true)
                {
                    return;
                }
            }

        }




        UI_Reference.EquipPanel.SetActive(true);
        Debug.Log("(int)powerType-1 " + ((int)powerType));
        UI_Reference.EquipPowerBGImage.sprite = UI_Reference.EquipPowerBGSprites[(int)powerType];
        UI_Reference.EquipCoreTypeImage.sprite = UI_Reference.CoreTypeIconSprite[(int)coreType];
        UI_Reference.Equip_WillGetDetail.text = YouWillGetMessage;
        UI_Reference.Equip_Title.text = coreType.ToString();
        coreTypeForEquip = coreType;
        corePowerTypeForEquip = powerType;
        coreAmountTO_Equip = GetCorePowerAmount(powerType);
        CurrentBtn = ClickedBtn;



    }


    public void Equip()
    {
        UI_Reference.EquipPanel.SetActive(false);

        InflateCore();
        onCoreEquiped.Invoke();
    }




    public void InflateCore()
    {


        Debug.Log("running");
        switch (coreTypeForEquip)
        {
            case CoreTypeInfo.Speed:
                playerSelectionScript.Players[PlayerSelection.current].Speed += coreAmountTO_Equip;

                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Speed",
                    playerSelectionScript.Players[PlayerSelection.current].Speed);
                

            

                break;
            case CoreTypeInfo.Boost:
                playerSelectionScript.Players[PlayerSelection.current].Boost += coreAmountTO_Equip;

                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Boost",
                    playerSelectionScript.Players[PlayerSelection.current].Boost);

              
                break;
            case CoreTypeInfo.Accelration:
                playerSelectionScript.Players[PlayerSelection.current].Accel += coreAmountTO_Equip;

                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Accel",
                    playerSelectionScript.Players[PlayerSelection.current].Accel);


                break;
            case CoreTypeInfo.Shocks:
                playerSelectionScript.Players[PlayerSelection.current].Shocks += coreAmountTO_Equip;

                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Shocks",
                    playerSelectionScript.Players[PlayerSelection.current].Shocks);

           
                break;
            default:
                break;
        }
        

        PlayerPrefs.SetInt(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + coreTypeForEquip.ToString() + "CoreType", 1);
        PlayerPrefs.SetInt(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + coreTypeForEquip.ToString() + "PowerType", (int)corePowerTypeForEquip);


        CurrentBtn.transform.DOScale(0, 0.2f).OnComplete(() =>
        {
            CurrentBtn.SetActive(false);
            CurrentBtn = null;





            for (int i = 0; i < coreDataContainerScriptable.allCoreData.Count; i++)
            {

                if (coreDataContainerScriptable.allCoreData[i].coreType == coreTypeForEquip)
                {
                    if (coreDataContainerScriptable.allCoreData[i].corePower == corePowerTypeForEquip)
                    {
                        coreDataContainerScriptable.allCoreData[i].noOfCore -= 1;
                    }
                }

            }


            playerSelectionScript.GetPlayerInfo();

        });



      


















    }

   

    public float GetCorePowerAmount(CorePowerType CorePwerTypes)
    {


        switch (CorePwerTypes)
        {
            case CorePowerType.Epic:
                return 25f;



            case CorePowerType.Rare:
                return 45f;


            case CorePowerType.Legendary:
                return 80f;



            default:
                return 0;
        }



    }





    public void UnEquipPrompt(int Num)
    {

        socketNumber = Num;
        UI_Reference.UnEquipPrompt.SetActive(true);


    }
    public void UnEquip()
    {
        

        UI_Reference.UnEquipPrompt.SetActive(false);
        Debug.Log("Socekt Number "+ socketNumber);
        PlayerPrefs.DeleteKey(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + socket[socketNumber].coreType.ToString() + "CoreType");
        PlayerPrefs.DeleteKey(PlayerSelection.Instance.Players[PlayerSelection.current].PlayerObject.name + socket[socketNumber].coreType.ToString() + "PowerType");

        socket[socketNumber].SetUnEquiped();



        switch (socketNumber)
        {
            case 0:
                playerSelectionScript.Players[PlayerSelection.current].Speed = playerSelectionScript.Players[PlayerSelection.current].speedDefault;
                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Speed", playerSelectionScript.Players[PlayerSelection.current].Speed);
                break;

            case 1:
                playerSelectionScript.Players[PlayerSelection.current].Boost = playerSelectionScript.Players[PlayerSelection.current].boostDefault;
                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Boost", playerSelectionScript.Players[PlayerSelection.current].Boost);
                break;
            case 2:
                playerSelectionScript.Players[PlayerSelection.current].Accel = playerSelectionScript.Players[PlayerSelection.current].AccelDefault;
                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Accel", playerSelectionScript.Players[PlayerSelection.current].Accel);
                break;
            case 3:
                playerSelectionScript.Players[PlayerSelection.current].Shocks = playerSelectionScript.Players[PlayerSelection.current].shocksDefault;
                PlayerPrefs.SetFloat(playerSelectionScript.Players[PlayerSelection.current].PlayerObject.name + "Shocks", playerSelectionScript.Players[PlayerSelection.current].Shocks);
                break;



            default:
                break;
        }





        //for (int i = 0; i < coreDataContainerScriptable.allCoreData.Count; i++)
        //{
          
          
        //        if (socket[socketNumber].coreType==coreDataContainerScriptable.allCoreData[i].coreType)
        //        {

        //            coreDataContainerScriptable.allCoreData[i].noOfCore +=1;

        //        }

                
           
        //}


        AddCoreInInventory(socket[socketNumber].coreType, socket[socketNumber].corePowerType, 1);


        playerSelectionScript.GetPlayerInfo();


    }




    private void OnEnable()
    {
        onCoreEquiped += Dummy;
    }
    private void OnDisable()
    {
        onCoreEquiped -= Dummy;
    }

    void Dummy()
    {
        Debug.Log("Awain Is Calling");
    }


    //public void AddCoreInInventoryOnReward(CoreTypeInfo coreType, CorePowerType corePowerType, int NoOfCores)
    //{
    //    for (int i = 0; i < coreDataContainerScriptable.allCoreData.Count; i++)
    //    {
    //        if (coreDataContainerScriptable.allCoreData[i].coreType == coreType && coreDataContainerScriptable.allCoreData[i].corePower == corePowerType)
    //        {
    //            coreDataContainerScriptable.allCoreData[i].noOfCore += NoOfCores;
    //        }
    //    }
    //}


    //public void AddintionInInventoryOnReward()
    //{
    //    AddCoreInInventoryOnReward(testingCoreTypeInfo, testingCorePowerType, testingamout);
    //}


}
