using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int CountEnemyAlive = 0;
    public Wave[] waves;  //生成敌人的波数
    public Transform START;  //敌人出生点
    public float waveRate = 0.3f;
    public Quaternion rotation;
    // Start is called before the first frame update
     void Start() 
    {
        StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
   StopCoroutine("SpawnEnemy");
    }
    //按照设置好的参数生成敌人
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab,START.position,rotation);
                CountEnemyAlive++;
                if(i!=wave.count-1)
                   yield return new WaitForSeconds(wave.rate);
            }
            
            while (CountEnemyAlive>0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
       while(CountEnemyAlive>0)
        {
            yield return 0;
        }
        GameManager.instance.Win();
    }
   

}
