using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    private int level;
    private int currentExp;
    private int maxExp;
    private float maxHp;
    private float currentHp;
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    public void Init()
    {
        level = 1;
        currentExp = 0;
        maxExp = 0;
        maxHp = 0;
        currentHp = 0;
        moveSpeed = 3f;
    }
}
