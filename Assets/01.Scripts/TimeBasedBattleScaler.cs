using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class TimeBasedBattleScaler
{
    private List<float> time;
    private List<int> monsterCountLimit;
    private List<float> monsterPowerMultiple;
    private List<float> monsterExpMultiple;

    private int currentLevel = 0;

    public void DataSetting(List<Dictionary<string, object>> datas)
    {
        time = new List<float>();
        monsterCountLimit = new List<int>();
        monsterPowerMultiple = new List<float>();
        monsterExpMultiple = new List<float>();

        foreach (var data in datas)
        {
            time.Add(float.Parse(data["Time"].ToString()));
            monsterCountLimit.Add((int)data["MonsterCountLimit"]);
            monsterPowerMultiple.Add(float.Parse(data["MonsterPowerMultiple"].ToString()));
            monsterExpMultiple.Add(float.Parse(data["MonsterExpMultiple"].ToString()));
        }
    }

    public void SetCurrentLevel(float time)
    {
        for (int i = 0; i < this.time.Count; i++)
        {
            if (time < this.time[i])
            {
                currentLevel = i - 1;
                return;
            }
        }
    }
    public int GetCurrentMonsterCountLimit()
    {
        return monsterCountLimit[currentLevel];
    }
    public float GetCurrentMonsterPowerMultiple()
    {
        return monsterPowerMultiple[currentLevel];
    }
    public float GetCurrentMonsterExpMultiple()
    {
        return monsterExpMultiple[currentLevel];
    }

}
