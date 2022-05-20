using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoreData
{
    public CoreTypeInfo coreType;
    public CorePowerType corePower;
    public Sprite coreSprite;
    public int noOfCore;
    public string willGetMessage;
}
[CreateAssetMenu(fileName = "CoreDataContainer", menuName = "CoreDataContainer/CoreData", order = 11)]

public class CoreDataContainerScriptable : ScriptableObject
{
    public List<CoreData> allCoreData;
}
