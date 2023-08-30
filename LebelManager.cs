using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LebelManager : MonoBehaviour
{
    public GameObject lebel;
    Button[] levelSelectButtons;
    int unlockedLevelIndex;

    private void Start()
    {
        unlockedLevelIndex = PlayerPrefs.GetInt("unlockedIndex");
        levelSelectButtons = new Button[lebel.transform.childCount] ;
        for (int i = 0; i < lebel.transform.childCount; i++)
        {
            levelSelectButtons[i] = lebel.transform.GetChild(i).GetComponent<Button>();
        }

        for(int i = 0;i < levelSelectButtons.Length; i++)
        {
            levelSelectButtons[i].interactable = false;
        }
        for( int i = 0;i < unlockedLevelIndex ; i++)
        {
            levelSelectButtons[i].interactable = true;
        }
    }

}
