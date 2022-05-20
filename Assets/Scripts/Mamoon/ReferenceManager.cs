using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerslideKartPhysics;
public class ReferenceManager : MonoBehaviour
{
    static public ReferenceManager instance;

    public CollectableSpinnerHandler collectableSpinnerHandler;
    
    public TrackManager trackManager;

    public GameManager gameManager;

    public UIControl uiControl;

    public SCPSManager scpsManager;

    public KartCamera kartCamera;

    public FTUE_Manager ftue_manager;

    public UiManager uiManager;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


}
