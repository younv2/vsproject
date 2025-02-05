using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour
{
    
    CharacterStat stat;
    Scanner scanner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scanner = transform.GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
