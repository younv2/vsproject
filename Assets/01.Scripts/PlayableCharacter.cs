using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayableCharacter : MonoBehaviour, IPoolable
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
    public Transform GetNearstTarget()
    {
        return scanner.nearstObject.transform;
    }
}
