//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
//using UnityEngine;
//using static System.Runtime.CompilerServices.RuntimeHelpers;
//using Vector3 = UnityEngine.Vector3;

//public class CameraMove : MonoBehaviour
//{
//    public float moveSpeed = 50;
//    public float scrollSpeed = 60;
//    public float space = 60;
//    public float Min_X = 0;
//    public float Max_X = 60;
//    public float Min_Y = 10;
//    public float Max_Y = 30;
//    public float Min_Z = 20;
//    public float Max_Z = 20;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < space)
//        {
//            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
//        }
//        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - space)
//        {
//            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
//        }
//        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height - space)
//        {
//            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
//        }
//        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < space)
//        {
//            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
//        }
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (scroll != 0)
//        {
//            transform.position += Vector3.down * scroll * scrollSpeed * Time.deltaTime;
//        }

//        //update这一帧结束之后才会确定移动
//        Vector3 pos = transform.position;
//        pos.x = Mathf.Clamp(pos.x, Min_X, Max_X);
//        pos.y = Mathf.Clamp(pos.y, Min_Y, Max_Y);
//        pos.z = Mathf.Clamp(pos.z, Min_Z, Max_Z);
//        transform.position = pos;
//    }
//}
