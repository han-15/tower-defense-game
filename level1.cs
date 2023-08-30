using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Save : MonoBehaviour
{
    private int currentStarNum = 0;
    public int levelIndex = 0;

  

    public void PressStarsButton(int _starNum)
    {
        currentStarNum = _starNum;
        if(currentStarNum >0)
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _starNum);
  
        }

        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex,_starNum));
    }
}