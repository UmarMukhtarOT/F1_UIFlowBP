using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerslideKartPhysics;

public class TrackInfoHandler : MonoBehaviour
{
    public string TrackName;
    public SCPSContainer TrackWaypoints;
    public bool isTutorialTrack=false;
    
    private BasicWaypoint firstWaypoint;

    public List<Transform> playerSpawnPosaitions;
    public Transform[] CameraAnimPositions;

    public delegate void OnTrackInit();
    public OnTrackInit OnTrackInitilialized;

    TrackManager trackManager;

    int selectedPlayer;
    public int noOfAi;
    int cameraSwitchValue=0;

    private void Awake()
    {
        trackManager = ReferenceManager.instance.trackManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedPlayer = PlayerPrefs.GetInt(PlayerPrefKeys.SelectedVehicleIndex, 0);
        if (!isTutorialTrack)
        {

            // this wiil be removed when AI Count work will implement in the future
            int randomNoOfAi = Random.Range(3, trackManager.trackManagerScriptable.allPlayerVehicles.Length);
            PlayerPrefs.SetInt(PlayerPrefKeys.TotalNoOfAi, randomNoOfAi);

            noOfAi = PlayerPrefs.GetInt(PlayerPrefKeys.TotalNoOfAi, 1);
        }
        else
        {
            noOfAi = 2;
        }

        firstWaypoint = TrackWaypoints.transform.GetChild(0).GetComponent<BasicWaypoint>();
      
        
        OnTrackInitilialized += OnTrackInitilialization;
        OnTrackInitilialized.Invoke();
    }

    void OnTrackInitilialization()
    {
        SetWayPoints();
        spawnPlayer();
        spawnAi();
        ReferenceManager.instance.scpsManager.Initilize();
    }

    void spawnPlayer()
    {
        int playerSpawnPos = trackManager.GetRandomSpawnPosition();

        Kart player = Instantiate(trackManager.trackManagerScriptable.allPlayerVehicles[selectedPlayer], playerSpawnPosaitions[playerSpawnPos].position, playerSpawnPosaitions[playerSpawnPos].rotation).GetComponent<Kart>();

        playerSpawnPosaitions.RemoveAt(playerSpawnPos);

        player.GetComponent<Rigidbody>().useGravity = false;
        
        Car playercar= new Car();

        playercar.CarTransform = player.transform;
        playercar.Name = player.GetComponent<PlayerInfoHandler>().playerName;
        playercar.Player = true;
        playercar.CarHeight = 2;



        ReferenceManager.instance.scpsManager.Cars.Add(playercar);
        playercar.CarTransform.gameObject.SetActive(true);

        ReferenceManager.instance.uiControl.Initialize(player);
        ReferenceManager.instance.kartCamera.Initialize(player);
    }
    void spawnAi()
    {
        for (int i = 0; i < noOfAi; i++)
        {
            Kart aiPlayer = Instantiate(trackManager.trackManagerScriptable.allAiVehicles[i], playerSpawnPosaitions[i].position, playerSpawnPosaitions[i].rotation).GetComponent<Kart>();
            
            aiPlayer.GetComponent<Rigidbody>().useGravity = false;

            Car aiPlayerCar = new Car();
            aiPlayerCar.CarTransform = aiPlayer.transform;
            aiPlayerCar.Name = aiPlayer.GetComponent<PlayerInfoHandler>().playerName;
            aiPlayerCar.Player = false;
            aiPlayerCar.CarHeight = 2;


            aiPlayer.GetComponent<BasicWaypointFollowerDrift>().targetPoint = firstWaypoint;
            ReferenceManager.instance.scpsManager.Cars.Add(aiPlayerCar);
            aiPlayer.gameObject.SetActive(true);

        }
    }


    void SetWayPoints()
    {
        ReferenceManager.instance.scpsManager._WaypointContainer = TrackWaypoints;
    }

    public void SetAnimCamera()
    {
        if(cameraSwitchValue<=CameraAnimPositions.Length)
        {
            ReferenceManager.instance.gameManager.startAnimCamera.GetComponent<SmoothFollow>().SetplayerTarget(CameraAnimPositions[cameraSwitchValue]);
            cameraSwitchValue += 1;

        }
    }
}
