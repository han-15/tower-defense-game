//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//public class GameManager : MonoBehaviour
//{

//    public GameObject endUI;
//    public GameObject winUI;
//    public GameObject loseUI;
//    private EnemySpawner enemySpawner;
//    public static GameManager instance;
//    public int Lives = 2;
//    public List<GameObject> enemys = new List<GameObject>();
//    public List<GameObject> supenemys = new List<GameObject>();
//    public List<GameObject> supenemys2 = new List<GameObject>();
//    public List<GameObject> supenemys3 = new List<GameObject>();
//    // Start is called before the first frame update

//    void Awake()
//    {
//        instance = this;
//        enemySpawner = GetComponent<EnemySpawner>();
//    }
//    void OnTriggerEnter(Collider col)
//    {
//        if (col.tag == "Enemy")
//        {
//            enemys.Add(col.gameObject);
//        }
//        if (col.tag == "supEnemy")
//        {
//            supenemys.Add(col.gameObject);
//        }
//        if (col.tag == "supEnemy2")
//        {
//            supenemys2.Add(col.gameObject);
//        }
//        if (col.tag == "supEnemy3")
//        {
//            supenemys3.Add(col.gameObject);
//        }
//    }

//    public void Failed()
//    {

//        enemySpawner.Stop();
//        endUI.SetActive(true);
//        winUI.SetActive(false);
//        loseUI.SetActive(true);

//    }
//    public void Win()
//    {

//        endUI.SetActive(true);
//        winUI.SetActive(true);
//        loseUI.SetActive(false);

//    }

//    private void Update()
//    {
//        int reLife = Lives - (enemys.Count + supenemys.Count + supenemys2.Count + supenemys3.Count);
//        if (reLife<=0)
//        {
//            Failed();
//        }
//    }
//    public void OnButtonRetry()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }
//    public void OnButtonMenu()
//    {
//        SceneManager.LoadScene(1);
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public GameObject endUI;
    public GameObject winUI;
    public GameObject loseUI;
    private EnemySpawner enemySpawner;
    public static GameManager instance;
    public int Lives = 2;
    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> supenemys = new List<GameObject>();
    public List<GameObject> supenemys2 = new List<GameObject>();
    public List<GameObject> supenemys3 = new List<GameObject>();
    //剩余生命
    public Text lifeText;
    private static int life = 100;

    // 生命值改变
    void ChangeLife(int relife)
    {
        if(relife>=0)
        {
            life = relife;
            lifeText.text = "剩余生命  " + life;
        }

        
    }
    void Awake()
    {
        instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
        if (col.tag == "supEnemy")
        {
            supenemys.Add(col.gameObject);
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

    public void Failed()
    {

        enemySpawner.Stop();
        endUI.SetActive(true);
        winUI.SetActive(false);
        loseUI.SetActive(true);

    }
    public void Win()
    {

        endUI.SetActive(true);
        winUI.SetActive(true);
        loseUI.SetActive(false);

    }

    private void Update()
    {
        int reLife = Lives - (enemys.Count + supenemys.Count + supenemys2.Count + supenemys3.Count);
        ChangeLife(reLife);
        if (reLife <= 0)
        {
            Failed();
        }
    }
    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(1);
    }
}

