using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GharacterData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject character;
    public int cost;
    public CharacterType type;
}
public enum CharacterType
{
    //角色种类
    Character,
}