using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapCube : MonoBehaviour
{
    //记录是否有炮台，不需要显示在inspector上
    [HideInInspector]
    public GameObject turretGo;
    private TurretData turretData;
    //建造特效
    public GameObject buildEffect;
    //建造武将需要根据Cube产生的位移
    Vector3 offset = new Vector3(0, 0.7f, 0);

    //获取cube原有颜色，设置响应颜色
    private new Renderer renderer;
    public Color hoverColor;
    private Color initColor;
    //武将数量
    const int N = 12;
    //创建武将数据
    public GameObject[] turret = new GameObject[N];
    public GameObject[] toggle = new GameObject[N];
    //技能是否可用的判定条件
    [HideInInspector]
    public bool isSkilled = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        initColor = renderer.material.color;

    }
    //建造武将
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isSkilled = false;

        if (turretData.turretPrefab == turret[0])
            toggle[0].SetActive(false);
        else if (turretData.turretPrefab == turret[1])
            toggle[1].SetActive(false);
        else if (turretData.turretPrefab == turret[2])
            toggle[2].SetActive(false);
        else if (turretData.turretPrefab == turret[3])
            toggle[3].SetActive(false);
        else if (turretData.turretPrefab == turret[4])
            toggle[4].SetActive(false);
        else if (turretData.turretPrefab == turret[5])
            toggle[5].SetActive(false);
        else if (turretData.turretPrefab == turret[6])
            toggle[6].SetActive(false);
        else if (turretData.turretPrefab == turret[7])
            toggle[7].SetActive(false);
        else if (turretData.turretPrefab == turret[8])
            toggle[8].SetActive(false);
        else if (turretData.turretPrefab == turret[9])
            toggle[9].SetActive(false);
        else if (turretData.turretPrefab == turret[10])
            toggle[10].SetActive(false);
        else if (turretData.turretPrefab == turret[11])
            toggle[11].SetActive(false);
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position + offset, Quaternion.identity); //建造武将
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position + offset, Quaternion.identity);//建造特效
        Destroy(effect, 1);//结束特效
    }
    //鼠标放在Cube上
    private void OnMouseEnter()
    {
        if ((turretGo == null) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = hoverColor;
        }
    }
    //鼠标离开Cube
    private void OnMouseExit()
    {
        renderer.material.color = initColor;
    }


    void Update()
    {
        DeadRestore();
    }
    //死亡武将回收
    void DeadRestore()
    {
        if (turretData.type == TurretType.charater1)
        {
            if (GetComponent<Character>().isDead)
            {
                if (turretData.turretPrefab == turret[1])
                {
                    toggle[1].SetActive(true);
                    toggle[1].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[2])
                {
                    toggle[2].SetActive(true);
                    toggle[2].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[3])
                {
                    toggle[3].SetActive(true);
                    toggle[3].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
            }
        }
        else if (turretData.type == TurretType.charater2)
        {
            if (GetComponent<Character2>().isDead)
            {
                if (turretData.turretPrefab == turret[0])
                {
                    toggle[0].SetActive(true);
                    toggle[0].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[5])
                {
                    toggle[5].SetActive(true);
                    toggle[5].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[10])
                {
                    toggle[10].SetActive(true);
                    toggle[10].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
            }

        }
        else if (turretData.type == TurretType.charater3)
        {

            if (turretData.turretPrefab.GetComponent<Character3>().isDead)
                Debug.Log("true");
            else
                Debug.Log("false");


            if (turretData.turretPrefab.GetComponent<Character3>().isDead)
            {

                if (turretData.turretPrefab == turret[4])
                {
                    toggle[4].SetActive(true);
                    toggle[4].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[6])
                {
                    toggle[6].SetActive(true);
                    toggle[6].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[7])
                {
                    toggle[7].SetActive(true);
                    toggle[7].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
            }

        }
        else if (turretData.type == TurretType.charater4)
        {
            if (GetComponent<Character4>().isDead)
                if (turretData.turretPrefab == turret[6])
                {
                    toggle[6].SetActive(true);
                    toggle[6].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[9])
                {
                    toggle[9].SetActive(true);
                    toggle[9].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
                else if (turretData.turretPrefab == turret[11])
                {
                    toggle[11].SetActive(true);
                    toggle[11].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                }
        }
    }
    //回收武将
    public void OnDestroy()
    {
        if (turretData.turretPrefab == turret[0])
        {
            toggle[0].SetActive(true);
            toggle[0].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[1])
        {
            toggle[1].SetActive(true);
            toggle[1].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[2])
        {
            toggle[2].SetActive(true);
            toggle[2].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[3])
        {
            toggle[3].SetActive(true);
            toggle[3].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[4])
        {
            toggle[4].SetActive(true);
            toggle[4].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[5])
        {
            toggle[5].SetActive(true);
            toggle[5].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[6])
        {
            toggle[6].SetActive(true);
            toggle[6].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[7])
        {
            toggle[7].SetActive(true);
            toggle[7].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[8])
        {
            toggle[8].SetActive(true);
            toggle[8].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[9])
        {
            toggle[9].SetActive(true);
            toggle[9].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[10])
        {
            toggle[10].SetActive(true);
            toggle[10].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        else if (turretData.turretPrefab == turret[11])
        {
            toggle[11].SetActive(true);
            toggle[11].transform.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }

        Destroy(turretGo);
    }
    //释放技能
    public void SkillRelease()
    {
        //根据武将类型释放相对应的技能
        if (turretData.type == TurretType.charater1)
            turretGo.GetComponent<Character>().Skill();
        else if (turretData.type == TurretType.charater2)
            turretGo.GetComponent<Character2>().Skill();
        else if (turretData.type == TurretType.charater3)
            turretGo.GetComponent<Character3>().Skill();
        else if (turretData.type == TurretType.charater4)
            turretGo.GetComponent<Character4>().Skill();


    }
}

