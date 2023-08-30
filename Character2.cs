using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class Character2 : MonoBehaviour
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
    public int damage = 10;
    public int totalhp;
    public Slider hpSlider;
    public bool isDead;
    private bool isSkill = false;
    public float waitTime;
    public Transform Start1;

    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> supenemys2 = new List<GameObject>();
    public List<GameObject> supenemys3 = new List<GameObject>();

    //�������ﶯ��
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }
    //�������ﵱǰ״̬���Ӷ�ִ�ж�Ӧ�Ķ���
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
    public float attackRateTime = 1;
    private float timer = 0;
    public float distanceArriveTarget = 5;

    //�趨��ʼ״̬
    void Start()
    {
        totalhp = hp;
        timer = attackRateTime;
        currentState = "idle";
        SetCharacterState(currentState);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (isDead == false)
        {
            //�����жϣ�ѡ��ִ�ж�Ӧ���˵��˺�
            if ((enemys.Count + supenemys2.Count + supenemys3.Count) > 0 && timer >= attackRateTime && isSkill == false)
            {

                if (supenemys3.Count > 0)
                {
                    UpdateEnemys();
                    float dir = UnityEngine.Vector3.Distance(supenemys3[0].transform.position, transform.position);

                    if (dir > distanceArriveTarget)
                    {
                        SetCharacterState("idle");

                    }
                    if (dir <= distanceArriveTarget && timer >= attackRateTime)
                    {
                        timer -= timer;
                        Attack3();
                    }
                }


                if (supenemys2.Count > 0)
                {
                    UpdatesupEnemys2();
                    float dir = UnityEngine.Vector3.Distance(supenemys2[0].transform.position, transform.position);
                    if (dir > distanceArriveTarget)
                    {
                        SetCharacterState("idle");

                    }
                    if (dir <= distanceArriveTarget && timer >= attackRateTime)
                    {
                        timer -= timer;
                        Attack2();
                    }
                }

                if (enemys.Count > 0)
                {
                    UpdateEnemys();
                    float dir = UnityEngine.Vector3.Distance(enemys[0].transform.position, transform.position);

                    if (dir > distanceArriveTarget)
                    {
                        SetCharacterState("idle");

                    }
                    if (dir <= distanceArriveTarget && timer >= attackRateTime)
                    {
                        timer -= timer;
                        Attack();
                    }
                }

            }
            if ((enemys.Count + supenemys2.Count) == 0 && isSkill == false)
            {
                SetCharacterState("idle");
            }
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
            SetCharacterState("hit");

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].GetComponent<Enemy>().TakeDamage(damage);
            }
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

            for (int i = 0; i < supenemys2.Count; i++)
            {
                supenemys2[i].GetComponent<supEnemy2>().TakeDamage(damage);
            }
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

            for (int i = 0; i < supenemys3.Count; i++)
            {
                supenemys3[i].GetComponent<supEnemy>().TakeDamage(damage);
            }
        }
        else
        {
            timer = attackRateTime;
        }
    }
    //��Ӣ��������˺�����
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

    //�������＼�ܶ����Ĳ��ţ����ڷ��꼼�ܺ�ָ�����ʼ״̬
    //����Ч����Ӣ�������ͻ������ɸ���Χ���˺�
    public void Skill()
    {
        this.GetComponentInChildren<skill>().OnSkill();
    }

    public void ActSkill()
    {
        isSkill = true;
        float z = Start1.position.z - transform.position.z;
        if (z < -2)
        {
            transform.Rotate(new Vector3(0, 20, 0));
        }
        if (z > 2)
        {
            transform.Rotate(new Vector3(0, -20, 0));
        }
        SetCharacterState("skill");
        Invoke("playIdle", 2f);


    }

    void playIdle()
    {
        float z = Start1.position.z - transform.position.z;
        if (z < -2)
        {
            transform.Rotate(new Vector3(0, -20, 0));
        }
        if (z > 2)
        {
            transform.Rotate(new Vector3(0, 20, 0));
        }
        SetCharacterState("idle");

        isSkill = false;

    }

    void Die()
    {
        GameObject.Destroy(this.gameObject, 1.3f);
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