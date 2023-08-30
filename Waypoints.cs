﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] positions;
    // Start is called before the first frame update
    void Awake()
    {
       positions = new Transform[transform.childCount];
        for(int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    
}
