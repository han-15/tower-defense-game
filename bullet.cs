using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    public float speed = 20;
    public int damage;
    private Transform target;
    private Transform target1;
    private Transform target2;
    private Transform target3;
    public GameObject explosionEffectPrefab;
    public float distanceArriveTarget = 4;

    //分别对不同标签的敌人锁定目标
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    public void SetTarget1(Transform _target)
    {
        this.target1 = _target;
    }
    public void SetTarget2(Transform _target)
    {
        this.target2 = _target;
    }
    public void SetTarget3(Transform _target)
    {
        this.target3 = _target;
    }
    

    void Update()
    {
            //无目标，则不生成箭
            if (target == null && target1 == null && target2 == null&& target3 == null)
            {

                Die();
                return;
            }
            else
        {
            //如果有目标，设定箭方向指向目标，并判断敌人是否进入攻击范围，若进入，则造成伤害
            //分别对三种目标都做判断
            if(target!=null)
                {

            this.transform.LookAt(target.position + new Vector3(0, 1.5f, 0));
            this.transform.Rotate(new Vector3(90, 0, 0));
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
            float dir = Vector3.Distance(target.position, transform.position);
            if (dir<=distanceArriveTarget)
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            }
            if(target1!=null)
                {
         this.transform.LookAt(target1.position + new Vector3(0, 1.5f, 0));
         this.transform.Rotate(new Vector3(90, 0, 0));
         transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
         float dir1 = Vector3.Distance(target1.position, transform.position);
           if (dir1<=distanceArriveTarget)
        {
            target1.GetComponent<supEnemy>().TakeDamage(damage);
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(effect, 1);
            Destroy(this.gameObject);

        }
            }
            if(target2!=null)
                {
        this.transform.LookAt(target2.position + new Vector3(0, 1.5f, 0));
        this.transform.Rotate(new Vector3(90, 0, 0));
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        float dir2 = Vector3.Distance(target2.position, transform.position);

        if (dir2<=distanceArriveTarget)
        {
            target2.GetComponent<supEnemy2>().TakeDamage(damage);
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(effect, 1);
            Destroy(this.gameObject);
        }
            }
            if (target3!=null)
            {
                this.transform.LookAt(target3.position + new Vector3(0, 1.5f, 0));
                this.transform.Rotate(new Vector3(90, 0, 0));
                transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
                float dir2 = Vector3.Distance(target3.position, transform.position);

                if (dir2<=distanceArriveTarget)
                {
                    target3.GetComponent<supEnemy3>().TakeDamage(damage);
                    GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                    Destroy(effect, 1);
                    Destroy(this.gameObject);
                }
            }
        }

       

      
       


    }

    //生成特效，持续一段时间后结束特效的播放
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}