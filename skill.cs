using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class skill : MonoBehaviour
{
    public int skillDamagetoEnemy = 30;
    public int skillDamagetosupEnemy2 = 30;
    public int skillDamagetosupEnemy3 = 30;
    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> supenemys2 = new List<GameObject>();
    public List<GameObject> supenemys3 = new List<GameObject>();
    //碰撞检查每种标签的敌人，改变对应数组的大小
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")

        {
            enemys.Add(col.gameObject);

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
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")

        {
            enemys.Remove(col.gameObject);
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
    private void Start()
    {
        //技能测试
        //OnSkill();
    }
    public void OnSkill()
    {
        Invoke("startSkill", 0.1f);

    }

    void startSkill()
    {
        UpdateEnemys();
        UpdatesupEnemys2();
        UpdatesupEnemys3();
        GameObject character2 = this.transform.parent.gameObject;
        character2.GetComponent<Character2>().ActSkill();
        Invoke("IsAttack", 0.5f);
    }
    //判断敌人数量是否大于0，若对应标签的敌人数量大于0，则对该种标签的敌人造成伤害
    void IsAttack()
    {

        if ((enemys.Count + supenemys2.Count + supenemys3.Count) > 0)
        {

            if (enemys.Count > 0)
                Attack();
            if (supenemys2.Count > 0)
                Attack2();
            if (supenemys3.Count > 0)
                Attack3();



        }
    }
    //对应每种标签敌人的伤害函数
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].GetComponent<Enemy>().TakeDamage(skillDamagetoEnemy);
            }
        }
    }
    void Attack2()
    {
        if (supenemys2[0] == null)
        {
            UpdatesupEnemys2();
        }
        if (supenemys2.Count > 0)
        {

            for (int i = 0; i < supenemys2.Count; i++)
            {
                supenemys2[i].GetComponent<supEnemy2>().TakeDamage(skillDamagetosupEnemy2);
            }
        }

    }
    void Attack3()
    {
        if (supenemys3[0] == null)
        {
            UpdatesupEnemys3();
        }
        if (supenemys3.Count > 0)
        {

            for (int i = 0; i < supenemys3.Count; i++)
            {
                supenemys3[i].GetComponent<supEnemy3>().TakeDamage(skillDamagetosupEnemy3);
            }
        }

    }
    //更新每种标签敌人的数组大小
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
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

}