using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
   
    public int skillDamagetoEnemy = 30;
    public int skillDamagetosupEnemy = 30;
    public int skillDamagetosupEnemy2 = 30;
    public int skillDamagetosupEnemy3 = 30;
    private Transform target;

    private Transform target2;
    private Transform target3;
    public GameObject explosionEffectPrefab;
    public float distanceArriveTarget = 4;
    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> supenemys = new List<GameObject>();
    public List<GameObject> supenemys2 = new List<GameObject>();
    public List<GameObject> supenemys3 = new List<GameObject>();

    //通过碰撞器检查不同标签敌人，若发生碰撞，将其存入对应的数组
    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Enemy")

        {
            enemys.Add(col.gameObject);
        }
        if (col.tag=="supEnemy")

        {
            supenemys.Add(col.gameObject);
        }
        if (col.tag == "supEnemy2")

        {
            supenemys2.Add(col.gameObject);
        }
        if (col.tag == "supEnemy3")

        {
            supenemys3.Add(col.gameObject);
        }
    }

    //不再发生碰撞，减小数组大小
    void OnTriggerExit(Collider col)
    {
        if (col.tag=="Enemy")

        {
            enemys.Remove(col.gameObject);
        }
        if (col.tag=="supEnemy")

        {
            supenemys.Remove(col.gameObject);
        }
        if (col.tag == "supEnemy2")

        {
            supenemys2.Remove(col.gameObject);
        }
        if (col.tag == "supEnemy3")

        {
            supenemys3.Remove(col.gameObject);
        }
    }

    //生成爆炸特效并在一段时间后结束特效播放
    void Start()
    {
        Destroy(this.gameObject,1);
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position-new Vector3(0,3,0), transform.rotation);
        Destroy(effect, 1);
    }
    private bool enemysAtked = false;
    private bool supenemysAtked = false;
    private bool supenemys2Atked = false;
    private bool supenemys3Atked = false;

    void Update()
    {
        //先更新每种标签敌人对应数组的大小
        UpdateEnemys();
        UpdatesupEnemys();
        UpdatesupEnemys2();
        UpdatesupEnemys3();
        //如果对应敌人数组的大小大于0并且未攻击过该敌人，则对其攻击
        if (enemys.Count > 0&&enemysAtked == false)
        {
            for (int i = 0; i<enemys.Count; i++)
            {
                enemys[i].GetComponent<Enemy>().TakeDamage(skillDamagetoEnemy);
            }
            enemysAtked = true;
        }
        if (supenemys.Count>0&& supenemysAtked == false)
        {

            for (int i = 0; i<supenemys.Count; i++)
            {
                supenemys[i].GetComponent<supEnemy>().TakeDamage(skillDamagetosupEnemy);
            }
            supenemysAtked = true;
        }
        if (supenemys2.Count>0&&supenemys2Atked== false)
        {

            for (int i = 0; i<supenemys2.Count; i++)
            {
                supenemys2[i].GetComponent<supEnemy2>().TakeDamage(skillDamagetosupEnemy2);
            }
            supenemys2Atked = true;
        }
        if (supenemys3.Count>0&&supenemys3Atked == false)
        {

            for (int i = 0; i<supenemys3.Count; i++)
            {
                supenemys3[i].GetComponent<supEnemy>().TakeDamage(skillDamagetosupEnemy3);
            }
            supenemys3Atked = true;

        }
    }
    //更新对应敌人数组的大小
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index<enemys.Count; index++)
        {
            if (enemys[index]==null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i<emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }
    void UpdatesupEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < supenemys.Count; index++)
        {
            if (supenemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            supenemys.RemoveAt(emptyIndex[i] - i);
        }
    }
    void UpdatesupEnemys2()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < supenemys2.Count; index++)
        {
            if (supenemys2[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            supenemys2.RemoveAt(emptyIndex[i] - i);
        }
    }
    void UpdatesupEnemys3()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < supenemys3.Count; index++)
        {
            if (supenemys3[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            supenemys3.RemoveAt(emptyIndex[i] - i);
        }
    }

}
