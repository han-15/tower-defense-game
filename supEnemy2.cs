using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Diagnostics;
using System.Numerics;

public class supEnemy2 : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset finish;
    public AnimationReferenceAsset walking;
    public AnimationReferenceAsset dead;
    public AnimationReferenceAsset hit;
    public string currentState;
    public string currentAnimation;
    public float moveSpeed;
    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    private bool isDead;
    private bool isHiting;
    public int hp;
    private int totalhp;
    public Slider hpSlider;
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> characters2 = new List<GameObject>();
    public List<GameObject> characters3 = new List<GameObject>();
    public List<GameObject> characters4 = new List<GameObject>();

    public float attackRateTime = 1;
    private float timer = 0;
    public int damage;
    public float distanceArriveTarget = 5;
    // Start is called before the first frame update
    void Start()
    {
        thePath = FindObjectOfType<Path>();
        currentState ="idle";
        totalhp=hp;
        SetCharacterState(currentState);
        timer = attackRateTime;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (isDead==false)
        {
            //多重判断，选择执行对应英雄的伤害
            if (characters2.Count > 0)
            { 
                UpdateCharacters2();
                float dir = UnityEngine.Vector3.Distance(characters2[0].transform.position, transform.position);
                if (dir > distanceArriveTarget)
                {
                    Move();
                }
                if (dir <= distanceArriveTarget && timer >= attackRateTime)
                {
                    timer -= timer;
                    Attack1();
                }
            }
            else
            {
                if (characters3.Count > 0)
                {
                    UpdateCharacters3();
                    float dir = UnityEngine.Vector3.Distance(characters3[0].transform.position, transform.position);
                    if (dir > distanceArriveTarget)
                    {
                        Move();
                    }
                    if (dir <= distanceArriveTarget && timer >= attackRateTime)
                    {
                        timer -= timer;
                        Attack2();
                    }
                }
                else
                {
                    if (characters4.Count > 0)
                    {
                        UpdateCharacters4();
                        float dir = UnityEngine.Vector3.Distance(characters4[0].transform.position, transform.position);
                        if (dir > distanceArriveTarget)
                        {
                            Move();
                        }
                        if (dir <= distanceArriveTarget && timer >= attackRateTime)
                        {
                            timer -= timer;
                            Attack3();
                        }
                    }
                    else
                    {
                        if (characters.Count > 0)
                        {
                            UpdateCharacters();
                            float dir = UnityEngine.Vector3.Distance(characters[0].transform.position, transform.position);


                            if (dir > distanceArriveTarget)
                            {
                                Move();

                            }
                            if (dir <= distanceArriveTarget && timer >= attackRateTime)
                            {
                                timer -= timer;
                                Attack();

                            }
                        }
                    }
                }

            }
            if (characters.Count+characters2.Count+characters3.Count + characters4.Count == 0)
            {
                Move();
            }
        }
        else
        {
            Die();
        }

    }
    //设置人物动画
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale=timeScale;
        currentAnimation=animation.name;
    }
    //设置人物当前状态，从而执行对应的动画
    public void SetCharacterState(string state)
    {
        if (state.Equals("idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("walking"))
        {
            SetAnimation(walking, true, 1f);
        }
        else if (state.Equals("finish"))
        {
            SetAnimation(finish, false, 1f);
        }
        else if (state.Equals("dead"))
        {
            SetAnimation(dead, false, 1f);
        }
        else if (state.Equals("hit"))
        {
            SetAnimation(hit, true, 1f);
        }
    }
    //敌人按照在道路上设置好的关键点移动
    public void Move()
    {
        if (reachedEnd==false)
        {
            SetCharacterState("walking");
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed*Time.deltaTime);
        }

        if (UnityEngine.Vector3.Distance(transform.position, thePath.points[currentPoint].position)<.01f)
        {
            currentPoint= currentPoint +1;
            if (currentPoint>=thePath.points.Length)
            {
                reachedEnd=true;
                SetCharacterState("finish");
            }
        }
        if (reachedEnd==true)
        {

            ReachDestination();
        }
    }
    //若到达终点，则敌人获胜，随后敌人消失
    void ReachDestination()
    {

        GameObject.Destroy(this.gameObject, 1.4f);
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }
    //对敌人自身的伤害函数
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value=(float)hp/totalhp;
        if (hp<=0)
        {
            isDead = true;
            SetCharacterState("dead");
        }

    }
    void Die()
    {

        GameObject.Destroy(this.gameObject, 1f);
    }
    //碰撞检查每种标签的英雄，改变对应数组的大小
    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Character")

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
        if (col.tag=="Character")

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
    //对应每种标签英雄的伤害函数
    void Attack()
    {

        if (characters[0]==null)
        {
            UpdateCharacters();
        }
        if (characters.Count>0)
        {


            SetCharacterState("hit");
            characters[0].GetComponent<Character>().TakeDamage(damage);

        }
        else
        {
            timer = attackRateTime;
        }

    }
    void Attack1()
    {

        if (characters2[0] == null)
        {
            UpdateCharacters2();
        }
        if (characters2.Count > 0)
        {


            SetCharacterState("hit");
            characters2[0].GetComponent<Character2>().TakeDamage(damage);

        }
        else
        {
            timer = attackRateTime;
        }

    }
    void Attack2()
    {

        if (characters3[0] == null)
        {
            UpdateCharacters3();
        }
        if (characters3.Count > 0)
        {


            SetCharacterState("hit");
            characters3[0].GetComponent<Character3>().TakeDamage(damage);

        }
        else
        {
            timer = attackRateTime;
        }

    }
    void Attack3()
    {

        if (characters4[0] == null)
        {
            UpdateCharacters4();
        }
        if (characters4.Count > 0)
        {


            SetCharacterState("hit");
            characters4[0].GetComponent<Character4>().TakeDamage(damage);

        }
        else
        {
            timer = attackRateTime;
        }

    }
    //更新每种标签英雄的数组大小
    void UpdateCharacters()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index<characters.Count; index++)
        {
            if (characters[index]==null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i<emptyIndex.Count; i++)
        {
            characters.RemoveAt(emptyIndex[i]-i);
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
