using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PowerslideKartPhysics;

public class ItemPicker : MonoBehaviour
{
    public ItemManager manager;
    CollectableSpinnerHandler collectableSpinnerHandler;
    public bool isPicked = false;

    // Start is called before the first frame update

    private void Start()
    {

        manager = FindObjectOfType<ItemManager>();
        collectableSpinnerHandler = ReferenceManager.instance.collectableSpinnerHandler;
        collectableSpinnerHandler.onSpinnerStopEvent += AssignPickableWithWait;
        collectableSpinnerHandler.OnSpinnerItemUsedEvent += OnItemUse;
    }

    void OnItemUse()
    {
        isPicked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag(Tags.Player))
        {
            if (other.CompareTag(Tags.PickableTreasure) && !isPicked)
            {
                Debug.Log("Treasure box is picked by Player" + transform.name);

                ReferenceManager.instance.collectableSpinnerHandler.InvokeSpinnerStartEvent();
                isPicked = true;
            }
        }
        if (this.CompareTag(Tags.Ai))
        {
            if (other.CompareTag(Tags.PickableTreasure))
            {
                Debug.Log("Treasure box is picked by " + transform.name); 
                AssignRandomPickable();
            }
        }
    }

    public void AssignRandomPickable()
    {
        if (manager != null)
        {
            // Give item to caster
            ItemCaster caster = transform.GetComponent<ItemCaster>();
            if (caster != null)
            {
                //offTime = 0.0f;
                int RandomNo = Random.Range(0, collectableSpinnerHandler.allCollectables.Length);
                RandomPickableUIItemHandler randomPicker = collectableSpinnerHandler.allCollectables[RandomNo].GetComponent<RandomPickableUIItemHandler>();
                // Give specific item if named, otherwise random item
                caster.GiveItem(
                    string.IsNullOrEmpty(randomPicker.ItemName) ? manager.GetRandomItem() : manager.GetItem(randomPicker.ItemName),
                    1, false, randomPicker.GetComponent<Image>().sprite);
            }
        }
    }


    void AssignPickableWithWait()
    {
        
        StartCoroutine( AssignPickable());
    }

    public IEnumerator AssignPickable()
    {
        yield return new WaitForSeconds(0.5F);
        if (manager != null)
        {
            // Give item to caster
            ItemCaster caster = transform.GetComponent<ItemCaster>();
            if (caster != null)
            {
                //offTime = 0.0f;
                
                // Give specific item if named, otherwise random item
                caster.GiveItem(
                    string.IsNullOrEmpty(collectableSpinnerHandler.selectedPickable.ItemName) ? manager.GetRandomItem() : manager.GetItem(collectableSpinnerHandler.selectedPickable.ItemName),
                    1, false, collectableSpinnerHandler.selectedPickable.GetComponent<Image>().sprite);
            }
        }
    }

}
