using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menulist;

    [SerializeField] private bool menuKeys = true;
    [SerializeField] private AudioSource bgmSound;
    TimerManager timerManager;
    void Start()
    {
        timerManager = TimerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuKeys)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menulist.SetActive(true);
                menuKeys = false;
                Time.timeScale = 0;
                bgmSound.Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menulist.SetActive(false);
            menuKeys = true;
            Time.timeScale = 1;
            bgmSound.Play();
        }
    }
    //点击返回游戏
    public void Return()
    {
        menulist.SetActive(false);
        menuKeys = true;
        Time.timeScale = 1;
        bgmSound.Play();

    }
    //点击选项
    public void Options()
    {
        SceneManager.LoadScene(0);
        TimerManager.instance.ResetTimer();
        Time.timeScale = 1;
    }
    //点击退出
    public void Quit()
    {
        Application.Quit();
    }
}
