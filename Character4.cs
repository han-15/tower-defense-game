﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class Character4 : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset finish;
    public AnimationReferenceAsset hit;
    public AnimationReferenceAsset dead;
    public AnimationReferenceAsset skill;
    public string currentState;
    public string currentAnimation;
    public int hp;
    public int totalhp;
    public Slider hpSlider;
    public bool isDead;
    private bool isSkill = false;
    public List<GameObject> enemys = new List<GameObject>();
    public Transform StartPoint;
    public List<GameObject> supenemys2 = new List<GameObject>();
    public List<GameObject> supenemys3 = new List<GameObject>();
    //设置人物动画
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }
    //设置人物当前状态，从而执行对应的动画
    public void SetCharacterState(string state)
    {
        if (state.Equals("idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("hit"))
        {
            SetAnimation(hit, true, 1f);
        }
        else if (state.Equals("finish"))
        {
            SetAnimation(finish, false, 1f);
        }
        else if (state.Equals("dead"))
        {
            SetAnimation(dead, false, 1f);
        }
        else if (state.Equals("skill"))
        {
            SetAnimation(skill, false, 1f);
        }
    }
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
    public float attackRateTime = 1;
    private float timer = 0;
    public GameObject bulletPrefab;
    public GameObject SkillPrefab;

    //设定初始状态
    void Start()
    {

        totalhp = hp;
        timer = attackRateTime;
        currentState = "idle";
        SetCharacterState(currentState);
      //  Invoke("Skill", 1f);
    }
    void Update()
    {
        timer += Time.deltaTime
            ;
        if (isDead == false)
        {
            //多重判断，选择执行对应敌人的伤害
            if (isDead == false)
                if (enemys.Count + supenemys2.Count + supenemys3.Count > 0 && timer >= attackRateTime && isSkill == false)
            {
                timer -= timer;

                if (supenemys3.Count > 0)
                    Attack3();
                else
                {
                    if (supenemys2.Count > 0)
                        Attack2();
                    else
                    {
                        if (enemys.Count > 0)
                            Attack();
                    }

                }
            }
            if (enemys.Count + supenemys2.Count + supenemys3.Count == 0 && isSkill == false)
            {
                SetCharacterState("idle");
            }

        }
    }
    public float skillTime;
    public float buildTime;
    //设置人物技能动画的播放，并在放完技能后恢复到初始状态
    //技能效果是造成更高的伤害
    public void Skill()
    {
        isSkill = true;
        Debug.Log("skill");
        SetCharacterState("skill");
        Invoke("buildSkill",buildTime);
    }
    void buildSkill()
    {
        float z = StartPoint.position.z - transform.position.z;
        if (z > 2)
        { GameObject bullet = GameObject.Instantiate(SkillPrefab, transform.position + new Vector3(10, 0, 4), Quaternion.identity); }
        else if (z < -2)
        { GameObject bullet = GameObject.Instantiate(SkillPrefab, transform.position + new Vector3(10, 0, -4), Quaternion.identity); }
        else
        { GameObject bullet = GameObject.Instantiate(SkillPrefab, transform.position + new Vector3(10, 0, 0), Quaternion.identity); }
        Invoke("playIdle", skillTime);
    }
    void playIdle()
    {

        SetCharacterState("idle");
        isSkill = false;

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
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, enemys[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet>().SetTarget(enemys[0].transform);
        }

        else
        {
            timer = attackRateTime;
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
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, supenemys2[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet>().SetTarget2(supenemys2[0].transform);
        }

        else
        {
            timer = attackRateTime;
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
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, supenemys3[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet>().SetTarget3(supenemys3[0].transform);
        }

        else
        {
            timer = attackRateTime;
        }
    }
    //对英雄自身的伤害函数
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = (float)hp / totalhp;
        if (hp <= 0)
        {
            isDead = true;
            SetCharacterState("dead");
        }
        if (isDead == true)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject.Destroy(this.gameObject, 1.3f);
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