using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossbullet : MonoBehaviour
{

    public int skillDamagetocharacter = 30;
    public int skillDamagetocharacter2 = 30;
    public int skillDamagetocharacter3 = 30;
    public int skillDamagetocharacter4 = 30;
    private Transform target;
    private Transform target2;
    private Transform target3;
    private Transform target4;
    public GameObject explosionEffectPrefab;
    public float distanceArriveTarget = 4;
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> characters2 = new List<GameObject>();
    public List<GameObject> characters3 = new List<GameObject>();
    public List<GameObject> characters4 = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Character")

        {
            characters.Add(col.gameObject);
        }
        if (col.tag == "Character2")

        {
            characters2.Add(col.gameObject);
        }
        if (col.tag == "Character3")

        {
            characters3.Add(col.gameObject);
        }
        if (col.tag == "Character4")

        {
            characters4.Add(col.gameObject);
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Character")

        {
            characters.Remove(col.gameObject);
        }
        if (col.tag == "Character2")

        {
            characters2.Remove(col.gameObject);
        }
        if (col.tag == "Character3")

        {
            characters3.Remove(col.gameObject);
        }
        if (col.tag == "Character4")

        {
            characters4.Remove(col.gameObject);
        }
    }
    void Start()
    {
        Destroy(this.gameObject, 1);
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position - new Vector3(0, 3, 0), transform.rotation);
        Destroy(effect, 1);
    }
    private bool charactersAtked = false;
    private bool characters2Atked = false;
    private bool characters3Atked = false;
    private bool characters4Atked = false;
    void Update()
    {
        UpdateCharacters();
        UpdateCharacters2();
        UpdateCharacters3();
        UpdateCharacters4();
        if (characters.Count > 0 && charactersAtked == false)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].GetComponent<Character>().TakeDamage(skillDamagetocharacter);
            }
            charactersAtked = true;
        }
        if (characters2.Count > 0 && characters2Atked == false)
        {
            for (int i = 0; i < characters2.Count; i++)
            {
                characters2[i].GetComponent<Character2>().TakeDamage(skillDamagetocharacter2);
            }
            charactersAtked = true;
        }
        if (characters3.Count > 0 && characters3Atked == false)
        {
            for (int i = 0; i < characters3.Count; i++)
            {
                characters3[i].GetComponent<Character3>().TakeDamage(skillDamagetocharacter3);
            }
            characters3Atked = true;
        }
        if (characters4.Count > 0 && characters4Atked == false)
        {
            for (int i = 0; i < characters4.Count; i++)
            {
                characters4[i].GetComponent<Character4>().TakeDamage(skillDamagetocharacter4);
            }
            characters4Atked = true;
        }
    }
    void UpdateCharacters()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < characters.Count; index++)
        {
            if (characters[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            characters.RemoveAt(emptyIndex[i] - i);
        }
    }
    void UpdateCharacters2()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < characters2.Count; index++)
        {
            if (characters2[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            characters2.RemoveAt(emptyIndex[i] - i);
        }
    }
    void UpdateCharacters3()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < characters3.Count; index++)
        {
            if (characters3[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            characters3.RemoveAt(emptyIndex[i] - i);
        }
    }
    void UpdateCharacters4()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < characters4.Count; index++)
        {
            if (characters4[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            characters4.RemoveAt(emptyIndex[i] - i);
        }
    }

}

