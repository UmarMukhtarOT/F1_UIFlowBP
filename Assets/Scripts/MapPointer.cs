using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointer : MonoBehaviour
{
    [SerializeField]
    private GameObject mapPointer;

    private void OnEnable()
    {
        mapPointer.gameObject.SetActive(true);
    }
}
