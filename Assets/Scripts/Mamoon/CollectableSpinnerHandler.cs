using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using PowerslideKartPhysics;

public class CollectableSpinnerHandler : MonoBehaviour
{
    public Transform[] allCollectables;
    ItemManager manager;
    public RandomPickableUIItemHandler selectedPickable;
    public GameObject pickableParent;
    public Image useItemButtonContainer;
    //public Transform CollectablePanel;
    //public Transform endPosition;
    //public Transform startPosition;
    //public float moveSpeed;
    public float timer=3f;
    int activeImage;
    bool keepMoving = false;


    public delegate void  OnSpinnerStart();
    public OnSpinnerStart onSpinnerStartEvent;

    public delegate void  OnSpinnerStop();
    public OnSpinnerStop onSpinnerStopEvent;

    public delegate void  OnSpinnerItemUsed();
    public OnSpinnerItemUsed OnSpinnerItemUsedEvent;

    // Start is called before the first frame update
    void Start()
    {
        onSpinnerStartEvent += StartMoving;
        onSpinnerStopEvent += stopRunningWaves;
        OnSpinnerItemUsedEvent += DeactivateAllPickablesOnUsed;
        SetRandomTimer();
       // onSpinnerStartEvent.Invoke();
    }
    float SetRandomTimer()
    {
        return timer= Random.Range(0.4f, 0.7f);
    }
    public void InvokeSpinnerStartEvent()
    {
        onSpinnerStartEvent.Invoke();

    }
    public void InvokeSpinnerStopEvent()
    {
        onSpinnerStopEvent.Invoke();

    }

    void StartMoving()
    {
        pickableParent.SetActive(true);
        StartCoroutine(startMoving());
    }

    private void Update()
    {
        if (keepMoving)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                InvokeSpinnerStopEvent();
            }
        }
    }

    public  IEnumerator startMoving()
    {
        if (keepMoving)
        {
            yield break;
        }

        keepMoving = true;

        while (keepMoving)
        {


            //while (timer > 0)
            {
                if (!keepMoving)
                {
                    yield break;
                }

                if (activeImage >= allCollectables.Length)
                {
                    activeImage = 0;
                }
                else
                {
                    for (int i = 0; i < allCollectables.Length; i++)
                    {
                        allCollectables[i].gameObject.SetActive(false);
                    }
                    allCollectables[activeImage].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.1F);
                    activeImage += 1;
                }
                //timer -= Time.deltaTime;
                yield return null;
            }
            

            //Reset Timer for next run 

            yield return null;
        }

        keepMoving = false;
    }

    void stopRunningWaves()
    {
        keepMoving = false;
        GetSelectedPickable();
        SetRandomTimer();
    }

    void GetSelectedPickable()
    {
        for (int i = 0; i < allCollectables.Length; i++)
        {
            if (allCollectables[i].gameObject.activeSelf)
                selectedPickable = allCollectables[i].GetComponent<RandomPickableUIItemHandler>();
                
        }
        StartCoroutine(waitToActiveUseButton());
    }

    IEnumerator waitToActiveUseButton()
    {
        yield return new WaitForSeconds(0.5f);
        pickableParent.SetActive(false);
        useItemButtonContainer.transform.parent.gameObject.SetActive(true);
        if (ReferenceManager.instance.trackManager.CurrentTrack.isTutorialTrack)
        {
            ReferenceManager.instance.ftue_manager.singleHandTutorial = false;
            ReferenceManager.instance.uiManager.tutorialLevelItems.tutorialHand.SetActive(true);
            ReferenceManager.instance.uiManager.tutorialLevelItems.tutorialHand.transform.position = useItemButtonContainer.transform.parent.transform.position;
            //Time.timeScale = 0.01f;
            Debug.Log("Speed Down Tutorial");
        }
        useItemButtonContainer.sprite = selectedPickable.GetComponent<Image>().sprite;
    }

    void DeactivateAllPickablesOnUsed()
    {
        useItemButtonContainer.transform.parent.gameObject.SetActive(false);

        //pickableParent.SetActive(false);
        //for (int i = 0; i < allCollectables.Length; i++)
        //{
        //    allCollectables[i].gameObject.SetActive(false);
        //}
    }

}
