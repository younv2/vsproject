using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoSingleton<MonsterSpawnManager>
{
    private int maxMonsterCnt = 3;

    private float maxSpawnDistance = 10;
    private float minSpawnDistance = 5;

    private List<Monster> monsterList = new List<Monster>();
    public List<Monster> MonsterList { get { return monsterList; } }

    void Update()
    {
        if (monsterList.Count >= maxMonsterCnt)
        {
            return;
        }
        if(GameObject.FindWithTag(Global.PLAYER) == null)
        {
            return;
        }
        SpawnMonster(0);
    }
    public void SpawnMonster(int monsterId)
    {
        Monster temp = ObjectPoolManager.Instance.GetPool<Monster>(Global.SLIME).GetObject();

        temp.transform.position = GetSpawnPosition();

        monsterList.Add(temp);
    }

    private Vector2 GetSpawnPosition()
    {
        // 플레이어 위치 기준으로 랜덤 방향과 거리 계산
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 spawnPosition = (Vector2)GameObject.FindWithTag(Global.PLAYER).transform.position + direction * distance;

        return spawnPosition;
    }
}
