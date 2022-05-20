using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoreType
{
    SPEED,
    ACC,
    BOOST
}

public class Core : MonoBehaviour
{
    public int coreId;
    public CoreType coreType;
    public float statValue;
    public float maxValue;
}
