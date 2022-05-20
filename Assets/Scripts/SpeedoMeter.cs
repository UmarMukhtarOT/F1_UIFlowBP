using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedoMeter : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        speed.text = Mathf.FloorToInt(rb.velocity.magnitude*3).ToString();
    }
}
