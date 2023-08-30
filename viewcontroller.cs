using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewcontroller : MonoBehaviour
{
    public float speed = 4;
    public float mousespeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h*speed, mouse*mousespeed, v*speed)*Time.deltaTime,Space.World);
       
       
    }
}
