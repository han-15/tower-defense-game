using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private int levelId;
    private Button btn;
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(int id,bool isLock)
    {
        levelId = id;
        if (isLock)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable= true;
        }
    }
    public void OnClick()
    {
        SceneManager.LoadScene(levelId);
    }
}
