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
    //��ײ���ÿ�ֱ�ǩ�ĵ��ˣ��ı��Ӧ����Ĵ�С
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
        //���ܲ���
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
    //�жϵ��������Ƿ����0������Ӧ��ǩ�ĵ�����������0����Ը��ֱ�ǩ�ĵ�������˺�
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
    //��Ӧÿ�ֱ�ǩ���˵��˺�����
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
    //����ÿ�ֱ�ǩ���˵������С
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