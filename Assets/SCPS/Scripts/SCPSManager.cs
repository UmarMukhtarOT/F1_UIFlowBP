using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SCPSManager : MonoBehaviour
{
    [Header("Add the cars here. The player Object should have the Player boolean set to true")]
    public List<Car> Cars;
    //   private int[] PlacingTracking;
    public SCPSContainer _WaypointContainer;
    private float[] A;
    [SerializeField]
    private List<Car> SavedList;
    [Space]
    [Header("UI")]
    public Text PlayerPosition;
    public Text NumberOfPlayers;
    public GameObject EnemyPositionText;
    public Canvas WorldCanvas;
    private int playerPlaceInArray;
    public Text PlayerPositionList;
    private Car Player;
    public Text CountDownText;
     public bool CountDown;
    private float LapTime;
    public Text DistanceText;
    [Header("LapTimes")]
    public Text LastLapTime;
    public Text CurrentLapTime;
    [Space]
    public int MaxLaps;
    public Text LapTracker;
    private bool Finished = false;
    private bool hasStarted;
   
    //[Space]
    //public FinishedPanel finishedPanel;

    [Space]
    public MiniMap map;

    [Space]
    [Header("Enable this and add a FinishLine Scipt")]

    [Header("on a new collider for the finish line")]
    public bool UsesFinishLapCollider;

    [Header("GPS Arrow")]
    public GameObject ArrowGb;
    private Transform Arrow;
    public Vector3 ArrowCameraOffset;
    
    [Header("Shows the next checkpoint as a sphere")]
    public bool ShowNextCheckpoint;
    UiManager uiManager;
    // public Collider FinishLineCollider;
    // Start is called before the first frame update

    private void Awake()
    {
        //PlayerSpwanManager.OnPlayerSpawn += SetPlayer;
    }

    void Start()
    {
        uiManager = ReferenceManager.instance.uiManager;

        //if (CurrentLapTime != null) {
        //    CurrentLapTime.text = "Lap Time: --:--:--";
        //}
        ////used for the sorting algorithm
        //A = new float[Cars.Length];
        ////the list the algorithm sorts
        //SavedList = new Car[Cars.Length];
        //SavedList = Cars;
        ////checks if the scps manager is set up correctly else throw an error
        //if (EnemyPositionText != null && map.CarArrow != null && NumberOfPlayers != null && finishedPanel != null)
        //{
        //    for (int i = 0; i < Cars.Length; i++)
        //    {
        //        Cars[i].carArrow = Instantiate(map.CarArrow);
        //        //instantiate the enemy attributes
        //        if (!Cars[i].Player)
        //        {
        //            Cars[i].PositionText = Instantiate(EnemyPositionText);
        //            Cars[i].PositionText.transform.SetParent(WorldCanvas.transform);
        //            //car arrow color to enemu
        //            Cars[i].carArrow.GetComponent<SpriteRenderer>().color = map.EnemyArrowColor;
        //        }
        //        //instantiate the player objects that it needs
        //        else
        //        {
        //            Player = Cars[i];
        //            //car arrow color to player
        //            Cars[i].carArrow.GetComponent<SpriteRenderer>().color = map.PlayerArrowColor;
        //            //makes the player arrow's sorting one point higher than the others in order to always be on top
        //            Cars[i].carArrow.GetComponent<SpriteRenderer>().sortingOrder++;
        //        }
        //    }
        //    //Sets the number of players in the text
        //    NumberOfPlayers.text = "/" + Cars.Length;
        //    finishedPanel.Finished_Panel.SetActive(false);
        //    if (ArrowGb != null)
        //    {
        //        Arrow = Instantiate(ArrowGb, Camera.main.transform).transform;
        //        Arrow.localPosition = ArrowCameraOffset;
        //    }
        //}
        ////Throw error if the user has not set up something in SCPSManager
        //else
        //{
        //    Debug.LogError("You have not set up SCPS Correctly");
        //}
        ////Instantiate the countdown
        //if (CountDown)
        //{
        //    if (CountDownText != null)
        //    {
        //        StartCoroutine(StartCountDown());
        //    }
        //    else
        //    {
        //        Debug.LogError("You have not set up SCPS Countdown correctly");
        //    }
        //}
        //else
        //{
        //    hasStarted = true;
        //}
    }

    //private void SetPlayer(GameObject playerObj)
    //{
    //    Cars[0].CarTransform = playerObj.transform;
    //    Initilize();
    //}

    public void Initilize()
    {
        if (CurrentLapTime != null)
        {
            CurrentLapTime.text = "--:--:--";
            //CurrentLapTime.text = "Lap Time: --:--:--";
        }
        //used for the sorting algorithm
        A = new float[Cars.Count];
        //the list the algorithm sorts
        //SavedList = new Car[Cars.Count];
        SavedList = Cars;
        //checks if the scps manager is set up correctly else throw an error
        if (EnemyPositionText != null && map.CarArrow != null && NumberOfPlayers != null && uiManager.GameCompletePanel != null)
        {
            for (int i = 0; i < Cars.Count; i++)
            {
                Cars[i].carArrow = Instantiate(map.CarArrow);
                //instantiate the enemy attributes
                if (!Cars[i].Player)
                {
                    Cars[i].PositionText = Instantiate(EnemyPositionText);
                    Cars[i].PositionText.transform.SetParent(WorldCanvas.transform);
                    //car arrow color to enemu
                    Cars[i].carArrow.GetComponent<SpriteRenderer>().color = map.EnemyArrowColor;
                }
                //instantiate the player objects that it needs
                else
                {
                    Player = Cars[i];
                    //car arrow color to player
                    Cars[i].carArrow.GetComponent<SpriteRenderer>().color = map.PlayerArrowColor;
                    //makes the player arrow's sorting one point higher than the others in order to always be on top
                    Cars[i].carArrow.GetComponent<SpriteRenderer>().sortingOrder++;
                }
            }
            //Sets the number of players in the text
            NumberOfPlayers.text = "/" + Cars.Count;
            uiManager.GameCompletePanel.Finished_Panel.SetActive(false);
            if (ArrowGb != null)
            {
                // Usman Nawaz :: Comment
                //Arrow = Instantiate(ArrowGb, Camera.main.transform).transform;
                //Arrow.localPosition = ArrowCameraOffset;
                // End Comment
            }
        }
        //Throw error if the user has not set up something in SCPSManager
        else
        {
            Debug.LogError("You have not set up SCPS Correctly");
        }
        //Instantiate the countdown
        if (CountDown)
        {
            if (CountDownText != null)
            {
                StartCoroutine(StartCountDown());
            }
            else
            {
                Debug.LogError("You have not set up SCPS Countdown correctly");
            }
        }
        else
        {
            hasStarted = true;
        }
    }

    IEnumerator StartCountDown()
    {
        CountDownText.gameObject.SetActive(true);
        foreach (Car c in Cars)
        {
            if (c.CarTransform.gameObject.tag == Tags.Player)
            {
                //c.CarTransform.GetComponent<Rigidbody>().isKinematic = true;
                c.CarTransform.GetComponent<PowerslideKartPhysics.KartInputMobile>().enabled = false;
            }
            else
                c.CarTransform.gameObject.GetComponent<PowerslideKartPhysics.BasicWaypointFollowerDrift>().enabled = false;
        }
        CountDownText.text = "3";
        ReferenceManager.instance.gameManager.SwitchToAnimCamera();

        ReferenceManager.instance.trackManager.CurrentTrack.SetAnimCamera();
        yield return new WaitForSeconds(4f);
        ReferenceManager.instance.trackManager.CurrentTrack.SetAnimCamera();
        CountDownText.text = "2";
        yield return new WaitForSeconds(4f);
        ReferenceManager.instance.gameManager.startAnimCamera.GetComponent<SmoothFollow>().SetplayerTarget(ReferenceManager.instance.kartCamera.targetKart.GetComponent<PlayerInfoHandler>().cameraTargetForCountdown);
        ReferenceManager.instance.gameManager.startAnimCamera.GetComponent<SmoothFollow>().damping = 5;
        ReferenceManager.instance.gameManager.startAnimCamera.GetComponent<SmoothFollow>().rotationDamping = 10;
        // ReferenceManager.instance.trackManager.CurrentTrack.SetAnimCamera();
        CountDownText.text = "1";
        yield return new WaitForSeconds(4f);
        ReferenceManager.instance.gameManager.SwitchToMainCamera();
        //yield return new WaitForSeconds(2f);

        hasStarted = true;
        CountDownText.text = "GO!";
        ReferenceManager.instance.uiManager.carControllerPanel.SetActive(true);
        foreach (Car c in Cars)
        {
            if (c.CarTransform.gameObject.tag == Tags.Player)
            {
                //c.CarTransform.GetComponent<Rigidbody>().isKinematic = false;
                c.CarTransform.GetComponent<PowerslideKartPhysics.KartInputMobile>().enabled = true;
            }
            else
                c.CarTransform.gameObject.GetComponent<PowerslideKartPhysics.BasicWaypointFollowerDrift>().enabled = true;
        }
        yield return new WaitForSeconds(1f);
        CountDownText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //check if the player is allowed to move based on the script
        if (!Finished && hasStarted)
        {
            CalculateTime();
            CalculateCheckpoints();

            CalculatePositions();

            UpdateUI();
        }
        ManageMiniMap();
    }
    void CalculateTime()
    {
        //Used for debugging the time faster
       // Time.timeScale = 10f;
        if (CurrentLapTime != null)
        {
            LapTime += Time.deltaTime;
            int minutes = (int)LapTime / 60;
            int seconds = ((int)LapTime - minutes*60);
            float miliseconds = ((LapTime - minutes*60 - seconds) * 100f);
            CurrentLapTime.text = ((minutes == 0) ? "00" : minutes.ToString("00")) + ":" + ((seconds == 0) ? "00" : seconds.ToString("00")) + ":" + ((int)miliseconds).ToString("00");
            //CurrentLapTime.text = "Lap time: " + ((minutes == 0) ? "00" : minutes.ToString("00")) + ":" + ((seconds == 0) ? "00" : seconds.ToString("00")) + ":" + ((int)miliseconds).ToString("00");
        }
    }
    //Moves the Car arrows according to their relative car prefabs position
    void ManageMiniMap()
    {
        foreach (Car c in Cars)
        {
            // Usman Nawaz :: Change
            c.carArrow.transform.position = c.CarTransform.position + Vector3.up * 20f;
            //c.carArrow.transform.rotation = Quaternion.Euler(90, 0, -c.CarTransform.rotation.eulerAngles.y); // original 
            c.carArrow.transform.rotation = Quaternion.Euler(90, 0, -c.CarTransform.gameObject.GetComponent<PowerslideKartPhysics.Kart>().rotator.transform.rotation.eulerAngles.y);
            // Usman Nawaz :: Change End
        }
    }
    //Updates the ui to show the correct information needed
    void UpdateUI()
    {
        //Player place list
        string CurrentList = "";
        int b = 1;
        for (int i = SavedList.Count - 1; i >= 0; i--)
        {

            CurrentList += (b) + ". " + Cars[i].Name + "\n";
            b++;
            if (Cars[i].Player)
            {
                // Usman Nawaz :: Change
                //LapTracker.text = "Lap: " + Cars[i].CurrentLap + "/" + MaxLaps; // original

                LapTracker.text = Cars[i].CurrentLap + "/" + MaxLaps; // usman change

                // Usman Nawaz :: End Change
            }
        }
        PlayerPositionList.text = CurrentList;
    }

    //Algorithm to calculate what checkpoint each car is
    void CalculateCheckpoints()
    {
        foreach (Car c in Cars)
        {
            //Enable The checkpoint renderer if its next
            if (ShowNextCheckpoint)
            {
                if (c.Player && !(c.CurrentPosition + 1 == _WaypointContainer.wp.Count && UsesFinishLapCollider))
                {
                    int previousContainerInt = (c.CurrentPosition - 1 >= 0) ? c.CurrentPosition - 1 : _WaypointContainer.wp.Count - 1;
                    //if it exits the sphere disable it
                    if ((Vector3.Distance(c.CarTransform.position, _WaypointContainer.wp[previousContainerInt].transform.position)) > _WaypointContainer.WaypointRadius*2.5f)
                    {
                        _WaypointContainer.wp[previousContainerInt].GetComponent<Renderer>().enabled = false;
                    }
                    _WaypointContainer.wp[c.CurrentPosition].GetComponent<Renderer>().enabled = true;
                }
            }
            if (c.distance < _WaypointContainer.WaypointRadius)
            {
                if (c.CurrentPosition + 1 < _WaypointContainer.wp.Count)
                {
                    c.CurrentPosition++;
                }
                //check for lap Position
                else if (!UsesFinishLapCollider)
                {
                    PassedLapLine(c);

                }
            }
            c.distance = Vector3.Distance(c.CarTransform.position, _WaypointContainer.wp[c.CurrentPosition].transform.position);
            //Calculate arrow 
            if (c.Player && Arrow != null)
            {
                var targetRotation = Quaternion.LookRotation(_WaypointContainer.wp[c.CurrentPosition].transform.position - Arrow.position);
                //change the arrow to go to the main camera
                if (Arrow.parent != Camera.main.transform)
                {
                    Arrow.SetParent(Camera.main.transform);
                    Arrow.localPosition = ArrowCameraOffset;
                }
                // Smoothly rotate towards the target point.
                Arrow.rotation = Quaternion.Slerp(Arrow.rotation, targetRotation, 5f * Time.deltaTime);
                if (DistanceText != null)
                {
                    DistanceText.text = "Distance: " + (c.distance-_WaypointContainer.WaypointRadius).ToString("###")+"yds";
                }

            }
            c.finalConverted = (float)c.CurrentLap * (float)_WaypointContainer.wp.Count + ((float)c.CurrentPosition + 1 / ((float)c.distance + 1f));
        }
        for (int i = 0; i < Cars.Count; i++)
        {
            A[i] = Cars[i].finalConverted;
        }
        
        SelectionSort(Cars, SavedList);
        

    }
    void PassedLapLine(Car c)
    {
        if (c.Player && c.CurrentLap >= MaxLaps)
        {
            StartCoroutine( FinishedRace());
        }
        else
        {
            c.CurrentLap++;
            c.CurrentPosition = 0;
        }
        if (c.Player&&ShowNextCheckpoint)
        {
            _WaypointContainer.wp[_WaypointContainer.wp.Count - 2].GetComponent<Renderer>().enabled = false;
        }
        if (LastLapTime != null&&c.Player)
        {
            int minutes = (int)LapTime / 60;
            int seconds = ((int)LapTime - minutes * 60);
            float miliseconds = ((LapTime - minutes * 60 - seconds) * 100f);
            LastLapTime.text = "Lap time: " + ((minutes == 0) ? "00" : minutes.ToString("00")) + ":" + ((seconds == 0) ? "00" : seconds.ToString("00")) + ":" + ((int)miliseconds).ToString("00");
            LapTime = 0;
        }
    }
    public void HittedFinishLine(Collider col)
    {
        foreach (Car c in Cars)
        {//checks if the car hitted in the collider is the collider and update it
            if (UsesFinishLapCollider && col.transform.IsChildOf(c.CarTransform))
            {

                if (c.CurrentPosition + 1 < _WaypointContainer.wp.Count)
                {
                    if (c.CurrentLap > 1)
                    {
                        Debug.LogError("FinishLineShould be on last checkpoint");
                    }
                }
                //check for lap Position
                else if (UsesFinishLapCollider)
                {
                    PassedLapLine(c);
                }
            }
        }
    }
    //The function that starts when the player crosses the finishline 
    IEnumerator FinishedRace()
    {
        Finished = true;
        ReferenceManager.instance.gameManager.OnRaceComplete.Invoke();

        yield return new WaitForSeconds(6F);
        // Usman Nawaz :: Comment on Original

        //finishedPanel.Finished_Panel.SetActive(true);
        //if (GetPlayerPosition() == 1)
        //{
        //    finishedPanel.FinishedText.text = "You finished 1st!";
        //}
        //else if (GetPlayerPosition() == 2)
        //{
        //    finishedPanel.FinishedText.text = "You finished 2nd!";
        //}
        //else if (GetPlayerPosition() == 3)
        //{
        //    finishedPanel.FinishedText.text = "You finished 3rd!";
        //}
        //else
        //{
        //    finishedPanel.FinishedText.text = "You finished " + GetPlayerPosition() + "th";
        //}

        // Usman Nawaz :: Comment original End



        // Usman Nawaz :: Change
        if (PlayerPrefs.GetInt(PlayerPrefKeys.TutorialLevel, 0) == 0)
        {
            uiManager.GameCompletePanel.homeButton.SetActive(false);
        }
        uiManager.GameCompletePanel.Finished_Panel.SetActive(true);
        if (GetPlayerPosition() == 1)
        {
            uiManager.GameCompletePanel.FinishedText.text = "st";
            uiManager.GameCompletePanel.FinishedPos.text = "1";
        }
        else if (GetPlayerPosition() == 2)
        {
            uiManager.GameCompletePanel.FinishedText.text = "nd";
            uiManager.GameCompletePanel.FinishedPos.text = "2";
        }
        else if (GetPlayerPosition() == 3)
        {
            uiManager.GameCompletePanel.FinishedText.text = "rd";
            uiManager.GameCompletePanel.FinishedPos.text = "3";
        }
        else
        {
            uiManager.GameCompletePanel.FinishedText.text = "th";
            uiManager.GameCompletePanel.FinishedPos.text = GetPlayerPosition().ToString();
        }
        // Usman Nawaz :: Change End

        // Usman Nawaz :: Change
        Player.CarTransform.GetComponent<Rigidbody>().drag = 1f;  //original line
        foreach (Car c in Cars)
        {
            c.CarTransform.GetComponent<Rigidbody>().drag = 2;
        }
        SetAllPlayerPostionsOnCompletion();
        CoinsManager.instance.AddCoins(500);

        // End Change
    }

    public void SetAllPlayerPostionsOnCompletion()
    {

        int position = 1;
        for (int i = SavedList.Count-1; i >=0 ; i--)
        {
            PlayerPositionContainer playerPostionInfo = Instantiate(uiManager.GameCompletePanel.playerInfoContainer, uiManager.GameCompletePanel.playerInforContainerParent.transform).GetComponent<PlayerPositionContainer>();
            playerPostionInfo.transform.position = ReferenceManager.instance.uiManager.PlayerResultPosition[position-1].position;
            playerPostionInfo.positionText.text = (position).ToString();
            //playerPostionInfo.positionText.text = (SavedList.IndexOf(SavedList[i])).ToString();
            playerPostionInfo.NameText.text = SavedList[i].Name.ToString();
            playerPostionInfo.avatarImage.sprite = ReferenceManager.instance.uiManager.GetRandomAvatar();
            position += 1;
        }
    }

    //returns the player's position
    int GetPlayerPosition()
    {
        int b = 1;
        for (int i = SavedList.Count - 1; i >= 0; i--)
        {
            if (Cars[i].Player)
            {
                return b;
            }
            else
            {
                b++;
            }
        }
        return 0;
    }
    //The selection sort alogithm
    void SelectionSort(List<Car> car, List<Car> B)
    {
        int size = car.Count;

        for (int i = 0; i < size - 1; i++)
        {
            int Imin = i;
            for (int j = i + 1; j < size; j++)
            {
                if (A[j] < A[Imin])
                {
                    Imin = j;
                }
                // else if(A[j] == A[Imin] && B[j].distance > B[Imin].distance){
                //   Imin = j;
                //}

            }
            float temp = A[Imin];
            Car temp2 = B[Imin];
            A[Imin] = A[i];
            B[Imin] = B[i];
            A[i] = temp;
            B[i] = temp2;


        }
    }
    //calculate's positions based on the sorted list
    void CalculatePositions()
    {

        int b = 0;
        for (int i = SavedList.Count - 1; i >= 0; i--)
        {
            b++;
            if (SavedList[i].Player && PlayerPosition != null)
            {
                playerPlaceInArray = i;
                PlayerPosition.text = b.ToString();

            }
            if (SavedList[i].PositionText)
            {
                float distance = Vector3.Distance(SavedList[i].CarTransform.position, Camera.main.transform.position);
                if (distance < 50f)
                {
                    SavedList[i].PositionText.GetComponent<Text>().text = b + " : " + SavedList[i].Name;
                }
                else if (distance < 120f)
                {
                    SavedList[i].PositionText.GetComponent<Text>().text = b.ToString();
                }
                else
                {
                    SavedList[i].PositionText.GetComponent<Text>().text = "";
                }
                SavedList[i].PositionText.transform.position = SavedList[i].CarTransform.position + Vector3.up * SavedList[i].CarHeight;
                SavedList[i].PositionText.transform.rotation = Camera.main.transform.rotation;
            }
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        //PlayerSpwanManager.OnPlayerSpawn -= SetPlayer;
    }
}
[System.Serializable]
public class Car
{
    public bool Player;
    [HideInInspector]
    public bool finsihed;
    public string Name;
    public Transform CarTransform;
    public float CarHeight;
    [HideInInspector]
    public int CurrentPosition;
    [HideInInspector]
    public float distance;
    [HideInInspector]
    public float finalConverted;
    [HideInInspector]
    public int CurrentLap = 1;
    [HideInInspector]
    public GameObject PositionText;
    [HideInInspector]
    public GameObject carArrow;
}

[System.Serializable]
public class MiniMap
{
    public GameObject CarArrow;
    Transform[] CarArrows;
    public Color PlayerArrowColor;
    public Color EnemyArrowColor;
}
