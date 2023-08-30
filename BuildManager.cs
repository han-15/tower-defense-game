
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Timers;

public class BuildManager : MonoBehaviour
{
    //技能面板以及技能按钮
    public GameObject skillCanvas;
    public Button buttonskill;
    //总共12个武将
    const int N = 12;
    //鼠标选中的方块
    public MapCube selectmapcube;
    public TurretData[] turret = new TurretData[N];


    //表示当前选中的炮台，要建造的炮台
    public TurretData selectedTurretData;
    //表示当前选中的炮台，场景中的物体
    public GameObject selectedTurretGo;
    //显示游戏金钱的文本框
    public Text moneyText;
    //显示技能冷却时间的文本框
    public Text CDText;
    //时间
    TimerManager timerManager;
    private static int money = 10;
    private static int CD = 0;
    //金钱动画
    public Animator moneyAnimator;
    //游戏计时器，用于金钱和CD的计算
    //static Timer timer;
    //武将技能
    public skill skill = null;
  
   
    //金钱与技能冷却改变并显示在UI上
    void ChangeMoneyCD(int change = 0)
    {
        // 每秒钟增加money
        if (timerManager != null && timerManager.GetTime() >= 1f)
        {
            money++;
            CD++;
           timerManager.ResetTimer();
        }
        money += change;
        moneyText.text = "$" + money;
        if (CD <= 8)
            CDText.text = "技能冷却中" + (8 - CD);
        else
            CDText.text = "技能已就绪";
    }
   
    
    private void Start()
    {
        timerManager = TimerManager.instance;
        money = 10;
        CD = 0;
   
    }
    private void Update()
    {
        //要一直更新金钱和技能冷却的数据
        ChangeMoneyCD();
  
        //进行建造
        //检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            //没有点击UI
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                //点到指定物体，能放置武将的物体都在MapCube层
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的cube
                    if (mapCube.turretGo == null)
                    {
                        //cube上没有武将，可以创建
                        if (money >= selectedTurretData.cost)
                        {
                            ChangeMoneyCD(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                            selectedTurretData = null;
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("moneyFlicker");
                        }
                    }
                    else if (mapCube.turretGo != null)
                    {

                        //已经存在武将
                        if (CD >= 8)
                        {
                            mapCube.isSkilled = true;
                        }
                        selectmapcube = mapCube;//储存选择的数据
                        if (mapCube.turretGo == selectedTurretGo && skillCanvas.activeInHierarchy)
                        {
                            //选择同一个武将，并且界面UI已经显示出来，再次点击关闭技能界面
                            HideskillUI();
                        }
                        else//展示技能界面
                        {
                            ShowskillUI(mapCube.transform.position, mapCube.isSkilled);
                        }
                        selectedTurretGo = mapCube.turretGo;
                    }
                }
            }
        }
    }
    //以下为点击不同的武将toggle选择不同的武将
    public void OnTurret1(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("turret1 is on");
            selectedTurretData = turret[0];
        }
    }
    public void OnTurret2(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[1];
        }
    }
    public void OnTurret3(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[2];
        }
    }
    public void OnTurret4(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[3];
        }
    }
    public void OnTurret5(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[4];
        }
    }
    public void OnTurret6(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[5];
        }
    }
    public void OnTurret7(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[6];
        }
    }
    public void OnTurret8(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[7];
        }
    }
    public void OnTurret9(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[8];
        }
    }
    public void OnTurret10(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[9];
        }
    }
    public void OnTurret11(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[10];
        }
    }
    public void OnTurret12(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret[11];
        }
    }
    //技能面板出现在指定位置
    void ShowskillUI(Vector3 pos, bool isDisableskill)
    {
        skillCanvas.SetActive(true);
        Vector3 offset = new Vector3(-2f, 10f, 2f);
        skillCanvas.transform.position = pos + offset;
        buttonskill.interactable = isDisableskill;
    }
    //隐藏技能面板
    void HideskillUI()
    {
        skillCanvas.SetActive(false);
    }
    //按下技能按钮
    public void OnskillButtonDown()
    {
        selectmapcube.SkillRelease();
        CD = 0;
        selectmapcube.isSkilled = false;
        HideskillUI();

    }
    //按下回收按钮
    public void OndestroyButtonDown()
    {
        selectmapcube.OnDestroy();
        HideskillUI();
    }
}
