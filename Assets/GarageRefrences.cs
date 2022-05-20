using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageRefrences : MonoBehaviour
{

    public GameObject LoadingScreen;
    public GameObject CongratulationsTitle, EarnMoreTitle, MainPanel, CorePanel, coreButton, GeneralMenu;
    



    public Image FillBar;
    [Header("Player Attributes")]


    public Image Speed_Bar_Img;
    public Image Boost_Bar_Img;
    public Image Acceleration_Bar_Img;
    public Image Shocks_Bar_Img;

    public Text InfoText;
    public Text TotalCash;


    [Header("UI Buttons")]
    public Button PlayBtn;
    public GameObject NextBtn;
    public GameObject PrevBtn;
    public GameObject BuyStatusPanel;



    public Text BuyStatusTitleTxt, BuyStatusPhraseTxt;
    public Button BuyButton;
    public Button PlayButtonGenericMenu;

   


    [Header("Equip Prompt  Sprites")]
    [Space(10)]
    public GameObject EquipPanel;
    public Text     Equip_Title;
    public Text     Equip_WillGetDetail;
    public Image    EquipPowerBGImage;
    public Image    EquipCoreTypeImage;
    public Sprite[] EquipPowerBGSprites;

    [Header("Core Section")]
    public Sprite[] CoreTypeIconSprite;



    [Header("UnEquip  System")]
    public GameObject UnEquipPrompt;




    [Space(10)]
    [Header("Cores In Use Images")]
    [Space(10)]

    public GameObject SpeedCoreInUse;
    public GameObject BoostCoreInUse;
    public GameObject AccelrationCoreInUse;
    public GameObject ShockCoreInUse;


    [Space(10)]
    [Header("Cores Empaty")]
    [Space(10)]
    public GameObject SpeedCoreEmpty;
    public GameObject BoostCoreEmpty;
    public GameObject AccelrationCoreEmpty;
    public GameObject ShockCoreEmpty;



    [Header("Core Generation Script")]
    public CoreGenerationHandler CoreGenerationScript;





    [Header("BattlePass System")]
    public Battle_Pass_Manager BP_Manager;
    public GameObject BP_PopUp;

   


    public Sprite Bp_SpeedCoreSpriteLocked;
    public Sprite Bp_BoostCoreSpriteLocked;
    public Sprite Bp_AccelCoreSpriteLocked;
    public Sprite Bp_ShocksCoreSpriteLocked;





    public Sprite Bp_SpeedCoreSpriteUnLocked;
    public Sprite Bp_BoostCoreSpriteUnLocked;
    public Sprite Bp_AccelCoreSpriteUnLocked;
    public Sprite Bp_ShocksCoreSpriteUnLocked;




}
