using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomPickableUIItemHandler : MonoBehaviour
{
    public string ItemName;
    

    //private void OnTriggerEnter(Collider other)
    //{
        //if(other.CompareTag(Tags.PickableEndPoint))
        //{
        //    transform.DOTogglePause();
        //    transform.position = ReferenceManager.instance.collectableSpinnerHandler.startPosition.position;
        //    transform.DOMove(ReferenceManager.instance.collectableSpinnerHandler.startPosition.position,0);
        //    StartCoroutine(UnpauseMovement());
        //    Debug.Log(this.name+" Collided with " + other.name);

        //}
    //}

    //IEnumerator UnpauseMovement()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    transform.DOMove(ReferenceManager.instance.collectableSpinnerHandler.endPosition.position, ReferenceManager.instance.collectableSpinnerHandler.moveSpeed).SetEase(Ease.Linear);
    //    //transform.DORestart();

    //}
}
