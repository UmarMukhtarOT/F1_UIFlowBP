using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BP_EquipButton : MonoBehaviour
{
    [HideInInspector]
    public Image cardImage;
    public Image cardIcone;
    public Text CardTypeText;
    public Text AmountText;
    public Text corePowerTypeText;
    


    
    void Start()
    {
        cardImage = GetComponent<Image>();
        //200 coins
        //AMOUNT Type
        //1 Core Epic Boost

    }

   
    void Update()
    {
        
    }










}
