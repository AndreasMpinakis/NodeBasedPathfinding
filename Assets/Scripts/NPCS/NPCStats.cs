using System;
using UnityEngine;

public class NPCStats : MonoBehaviour
{



}

[Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    [SerializeField] private int levelValue;
    [SerializeField] private int modifiedValue;
}