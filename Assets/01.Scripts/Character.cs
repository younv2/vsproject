using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour, IPoolable
{
    CharacterStat stat;
    Scanner scanner;
    void Start()
    {
        scanner = transform.GetComponent<Scanner>();
    }

    void Update()
    {

    }
}
