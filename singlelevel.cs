using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class singlelevel : MonoBehaviour
{
    private int currentStarNum = 0;
    private int levelIndex;
  public void BackButton()
    {
        SceneManager.LoadScene("00_level Selection");
    }
  public void PressStarsButton(int starNum)
    {
        currentStarNum = starNum;
        if (currentStarNum>PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" +  levelIndex, starNum);
        }
        BackButton();
        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex,starNum));
    }
}
