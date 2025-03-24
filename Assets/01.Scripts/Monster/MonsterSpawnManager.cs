using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoSingleton<MonsterSpawnManager>
{
    private int maxMonsterCnt = 10;

    private float maxSpawnDistance = 20;
    private float minSpawnDistance = 10;

    private Dictionary<int,Monster> monsterDic = new Dictionary<int,Monster>();
    public Dictionary<int, Monster> MonsterDic { get { return monsterDic; } }

    public void ManualUpdate()
    {
        if (monsterDic.Count >= maxMonsterCnt)
        {
            return;
        }
        if(GameObject.FindWithTag(Global.PLAYER) == null)
        {
            return;
        }
        SpawnMonster(0);
        maxMonsterCnt = DataManager.Instance.TimeBasedBattleScalers.GetCurrentMonsterCountLimit();
    }
    public void SpawnMonster(int monsterId)
    {
        Monster temp = ObjectPoolManager.Instance.GetPool<Monster>(Global.PoolKey.SLIME).GetObject();
        temp.transform.position = GetSpawnPosition();

        monsterDic.Add(temp.gameObject.GetInstanceID(),temp);
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
