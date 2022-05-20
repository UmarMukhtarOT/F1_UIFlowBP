using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TrackCreatorManager", menuName = "TrackCreator/TrackCreatorManagerScriptableObj", order = 4)]
public class TrackCreatorManagerScriptable : ScriptableObject
{
    public GameObject[] allAiVehicles;
    public GameObject[] allPlayerVehicles;
    public GameObject[] allPlayableTracks;
    public GameObject tutorialLevelTrack;
}
