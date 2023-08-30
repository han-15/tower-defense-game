using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveNew : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public Image unlockImage;


    private void Update()
    {
        //进行数据删除清理本地存档
        //PlayerPrefs.DeleteAll();
        UpdateLevelStatus();
        UpdateLevelImage();

    }



    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv"+previousLevelNum) > 0)
        {
            unlocked = true;
        }
    }
    private void UpdateLevelImage()
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
        }
        else
        {
            unlockImage.gameObject.SetActive(false);

        }
    }

    public void PressSelection(string LevelName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(LevelName);
        }
    }
}
