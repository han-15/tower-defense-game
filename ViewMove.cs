using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMove : MonoBehaviour
{
    public float moveSpeed = 50;
    public float scrollSpeed = 60;
    public float space = 60;
    public float Min_X = -40;
    public float Max_X = -20;
    public float Min_Y = 10;
    public float Max_Y = 30;
    public float Min_Z = -50;
    public float Max_Z = -30;

   

 
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            transform.position += Vector3.down * scroll * scrollSpeed * Time.deltaTime*50;
        }

        //update这一帧结束之后才会确定移动
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, Min_X, Max_X);
        pos.y = Mathf.Clamp(pos.y, Min_Y, Max_Y);
        pos.z = Mathf.Clamp(pos.z, Min_Z, Max_Z);
        transform.position = pos;
    }
}
