using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class Enemymove : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset finish;
    public AnimationReferenceAsset walking;
    public AnimationReferenceAsset dead;
    public string currentState;
    public string currentAnimation;
    public float moveSpeed;
    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    private bool isDead;
    public int hp;
    private int totalhp;
    public Slider hpSlider;
    public int Lives;
    // Start is called before the first frame update
    void Start()
    {
        thePath = FindObjectOfType<Path>();
        currentState ="idle";
        totalhp=hp;
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead==false)
        {
            Move();
        }
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale=timeScale;
        currentAnimation=animation.name;
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
    }
    public void Move()
    {
        if (reachedEnd==false)
        {
            SetCharacterState("walking");
            transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed*Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, thePath.points[currentPoint].position)<.01f)
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

    void ReachDestination()
    {
        
        GameObject.Destroy(this.gameObject, 1.3f);
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value=(float)hp/totalhp;
        if (hp<=0)
        {
            isDead = true;
            SetCharacterState("dead");
        }
        if (isDead==true)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject.Destroy(this.gameObject, 1.3f);
    }

}
