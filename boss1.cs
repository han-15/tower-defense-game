using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class boss1 : MonoBehaviour
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
    public float downSpeed = 0.1f;
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> characters2 = new List<GameObject>();
    public List<GameObject> characters3 = new List<GameObject>();
    public List<GameObject> characters4 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        thePath = FindObjectOfType<Path>();
        currentState = "idle";
        totalhp = hp;
        SetCharacterState(currentState);
        timer = attackRateTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isDead == false)
        {

            if (characters.Count + characters2.Count + characters3.Count + characters4.Count > 0 && timer >= attackRateTime)
            {
                timer -= timer;
                if (characters.Count > 0)
                {
                    Attack();
                }
                else
                {
                    if (characters2.Count > 0)
                    {
                        Attack2();
                    }
                    else
                    {
                        if (characters3.Count > 0)
                        {
                            Attack3();
                        }
                        else
                        {
                            if (characters4.Count > 0)
                            {
                                Attack4();
                            }
                        }
                    }
                }

            }
            if (characters.Count + characters2.Count + characters3.Count + characters4.Count == 0)
            {
                Move();
            }
        }
        else
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
            Die();
        }
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }
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
    public void Move()
    {
        if (reachedEnd == false)
        {
            SetCharacterState("walking");
            transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < .01f)
        {
            currentPoint = currentPoint + 1;
            if (currentPoint >= thePath.points.Length)
            {
                reachedEnd = true;
                SetCharacterState("finish");
            }
        }
        if (reachedEnd == true)
        {

            ReachDestination();
        }
    }

    void ReachDestination()
    {

        GameObject.Destroy(this.gameObject, 1.4f);
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = (float)hp / totalhp;
        if (hp <= 0)
        {
            isDead = true;
            SetCharacterState("dead");

        }

    }
    void Die()
    {

        GameObject.Destroy(this.gameObject, 1f);
    }

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
    public float attackRateTime = 1;
    private float timer = 0;
    public GameObject bulletPrefab;
    public Transform firePosition;
    void Attack()
    {
        if (characters[0] == null)
        {
            UpdateCharacters();
        }
        if (characters.Count > 0)
        {
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, characters[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet1>().SetTarget(characters[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }
    void Attack2()
    {
        if (characters2[0] == null)
        {
            UpdateCharacters2();
        }
        if (characters2.Count > 0)
        {
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, characters2[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet1>().SetTarget2(characters2[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }
    void Attack3()
    {
        if (characters3[0] == null)
        {
            UpdateCharacters3();
        }
        if (characters3.Count > 0)
        {
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, characters3[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet1>().SetTarget3(characters3[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }
    void Attack4()
    {
        if (characters4[0] == null)
        {
            UpdateCharacters4();
        }
        if (characters4.Count > 0)
        {
            SetCharacterState("hit");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, characters4[0].transform.position, Quaternion.identity);
            bullet.GetComponent<bullet1>().SetTarget4(characters4[0].transform);
        }
        else
        {
            timer = attackRateTime;
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