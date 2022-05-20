using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour
{
    public string playerName;
    public Transform CameraTargetOnCompletion;
    public Transform cameraTargetForCountdown;

    private void Awake()
    {
        if(this.CompareTag(Tags.Player))
            playerName = PlayerPrefs.GetString(PlayerPrefKeys.PlayerName,"PlayerName");


        //if (this.CompareTag(Tags.Ai))
        //{
        //    int aiName = Random.Range(0, ReferenceManager.instance.gameManager.aiNamesScriptable.aiNames.Count);
        //    playerName= ReferenceManager.instance.gameManager.aiNamesScriptable.aiNames[aiName];

        //}
    }

    private void Start()
    {
        ReferenceManager.instance.gameManager.OnRaceComplete += SetCameraOnCompletion;
        

    }
    private void OnDisable()
    {
        ReferenceManager.instance.gameManager.OnRaceComplete -= SetCameraOnCompletion;

    }
    void SetCameraOnCompletion()
    {
        if(this.CompareTag( Tags.Player))
        {
            ReferenceManager.instance.uiControl.targetKart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(CameraChangeWithWait());

        }

    }

    IEnumerator CameraChangeWithWait()
    {
        yield return new WaitForSeconds(2f);
        ReferenceManager.instance.uiControl.targetKart.GetComponent<Rigidbody>().drag = 100;
        ReferenceManager.instance.gameManager.startAnimCamera.SetActive(true);

        yield return new WaitForSeconds(1f);
        ReferenceManager.instance.uiControl.targetKart.GetComponent<Rigidbody>().velocity= Vector3.zero;

        //ReferenceManager.instance.gameManager.camera.SetActive(false);
        ReferenceManager.instance.gameManager.startAnimCamera.GetComponent<SmoothFollow>().SetplayerTarget(CameraTargetOnCompletion);

    }
}
