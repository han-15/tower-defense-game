using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class Character3 : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset reveal;
    public AnimationReferenceAsset dead;
    public AnimationReferenceAsset skill;
    public string currentState;
    public string currentAnimation;
    public int revealhp;
    public int skillRevealhp;
    public int hp;
    public int damage = 10;
    public int totalhp;
    public Slider hpSlider;
    public bool isDead;
    private bool isSkill;

    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> characters2 = new List<GameObject>();
    public List<GameObject> characters4 = new List<GameObject>();

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
        else if (state.Equals("reveal"))
        {
            SetAnimation(reveal, true, 1f);
        }
        else if (state.Equals("dead"))
        {
            SetAnimation(dead, false, 1f);
        }
    }
    //设置人物技能动画的播放，并在放完技能后恢复到初始状态
    //技能效果是给友方英雄回血，回血量加大
    public void Skill()
    {
        isSkill = true;
        SetAnimation(skill, false, 1f);
        UpdateCharacters();
        UpdateCharacters2();
        UpdateCharacters4();
        if ((characters.Count > 0 || characters2.Count > 0 || characters4.Count > 0) )
        {
            if (characters.Count > 0)
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    if (characters[i].GetComponent<Character>().hp < characters[i].GetComponent<Character>().totalhp)
                    {
                        characters[i].GetComponent<Character>().hp += skillRevealhp;
                    }
                }
            }
            if (characters2.Count > 0)
            {
                for (int i = 0; i < characters2.Count; i++)
                {
                    if (characters2[i].GetComponent<Character2>().hp < characters2[i].GetComponent<Character2>().totalhp)
                    {
                        characters2[i].GetComponent<Character2>().hp += skillRevealhp;
                    }
                }
            }
            if (characters4.Count > 0)
            {
                for (int i = 0; i < characters4.Count; i++)
                {
                    if (characters4[i].GetComponent<Character4>().hp < characters4[i].GetComponent<Character4>().totalhp)
                    {
                        characters4[i].GetComponent<Character4>().hp += skillRevealhp;
                    }
                }
            }
        }
        Invoke("playIdle", 1f);
    }
    void playIdle()
    {
        SetCharacterState("idle");
        isSkill = false;
    }
    //碰撞检查每种标签的敌人，改变对应数组的大小
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
        if (col.tag == "Character")

        {
            characters4.Remove(col.gameObject);
        }
    }

    public float revealRateTime = 1;
    private float timer = 0;

    //设定初始状态
    void Start()
    {
        totalhp = hp;
        timer = revealRateTime;
        currentState = "idle";
        SetCharacterState(currentState);
    }
    void Update()
    {
        UpdateCharacters();
        UpdateCharacters2();
        UpdateCharacters4();
        timer += Time.deltaTime;
        if (isDead == false)
        {
            //多重判断，选择执行对应英雄的回血
            if ((characters.Count > 0 || characters2.Count > 0 || characters4.Count > 0) && timer >= revealRateTime)
            {
                timer -= timer;
                if (characters.Count > 0)
                {
                    for (int i = 0; i < characters.Count; i++)
                    {
                        if (characters[i].GetComponent<Character>().hp < characters[i].GetComponent<Character>().totalhp)
                        {
                            characters[i].GetComponent<Character>().hp += revealhp;
                            SetCharacterState("reveal");
                        }

                    }
                }
                if (characters2.Count > 0)
                {
                    for (int i = 0; i < characters2.Count; i++)
                    {
                        if (characters2[i].GetComponent<Character2>().hp < characters2[i].GetComponent<Character2>().totalhp)
                        {
                            characters2[i].GetComponent<Character2>().hp += revealhp;
                            SetCharacterState("reveal");
                        }

                    }
                }
                if (characters4.Count > 0)
                {
                    for (int i = 0; i < characters4.Count; i++)
                    {
                        if (characters4[i].GetComponent<Character4>().hp < characters4[i].GetComponent<Character4>().totalhp)
                        {
                            characters4[i].GetComponent<Character4>().hp += revealhp;
                            SetCharacterState("reveal");
                        }

                    }
                }
            }
            if ((characters.Count + characters2.Count + characters4.Count) == 0)
            {
                SetCharacterState("idle");
            }
        }
        else
        {
            Die();
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
    //更新每种标签友方英雄的数组大小
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