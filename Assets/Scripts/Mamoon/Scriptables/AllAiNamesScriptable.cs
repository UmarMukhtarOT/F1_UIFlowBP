using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiNames", menuName = "AllAiNames/Names", order = 5)]
public class AllAiNamesScriptable : ScriptableObject
{
    public List<string> aiNames;
}
