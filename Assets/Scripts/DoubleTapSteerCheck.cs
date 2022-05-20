using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapSteerCheck : MonoBehaviour
{
    public static bool leftSteer = false;
    public static bool rightSteer = false;

    public void OnLeftSteerDown()
    {
        leftSteer = true;
    }

    public void OnLeftSteerUp()
    {
        leftSteer = false;
    }

    public void OnRightSteerDown()
    {
        rightSteer = true;
    }

    public void OnRightSteerUp()
    {
        rightSteer = false;
    }
}
