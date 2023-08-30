using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    public GameObject turretPrefab;
    public int cost;
    // public GameObject turretUpgratedPrefab;
    // public int costUpgrated;
    public TurretType type;
    // public GameObject releaseSkillPrefab;
}

public enum TurretType
{
    charater1, charater2, charater3, charater4
}
