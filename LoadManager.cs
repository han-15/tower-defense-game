using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    AsyncOperation async;
    public Slider progressSlider;
    public GameObject loadScreen;
    public Slider slider;
    public Text text;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        operation.allowSceneActivation = false;
        Debug.Log("shhh");
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            text.text = operation.progress * 100 + "%";

            if(operation.progress>=0.9f)
            {
                slider.value = 1;

                text.text = "按任意键或点击以继续";

                if(Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

}
