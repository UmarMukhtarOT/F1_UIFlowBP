using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public TrackCreatorManagerScriptable trackManagerScriptable;
    public TrackInfoHandler CurrentTrack;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTrack();
    }

    public int GetRandomSpawnPosition()
    {
        int randomValue = Random.Range(0, trackManagerScriptable.allPlayerVehicles.Length);

        return randomValue;
    }
    public void SpawnTrack()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.TutorialLevel, 0) == 0)
        {
            GameObject track = Instantiate(trackManagerScriptable.tutorialLevelTrack.gameObject);
            track.SetActive(true);
            track.name = trackManagerScriptable.tutorialLevelTrack.GetComponent<TrackInfoHandler>().TrackName;
            CurrentTrack = track.GetComponent<TrackInfoHandler>();
            ReferenceManager.instance.ftue_manager.SetTutorialLevel();
        }
        else
        {
            if (!PlayerPrefs.HasKey(PlayerPrefKeys.SelectedTrack))
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.SelectedTrack, 0);
            }

            GameObject track = Instantiate(trackManagerScriptable.allPlayableTracks[PlayerPrefs.GetInt(PlayerPrefKeys.SelectedTrack)].gameObject);
            track.SetActive(true);
            CurrentTrack = track.GetComponent<TrackInfoHandler>();
            track.name = trackManagerScriptable.allPlayableTracks[PlayerPrefs.GetInt(PlayerPrefKeys.SelectedTrack)].GetComponent<TrackInfoHandler>().TrackName;
        }
    }

  
}
