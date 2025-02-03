using UnityEngine;

public class MonsterSpawnManager : MonoSingleton<MonsterSpawnManager>
{
    private int curMonsterCnt = 0;
    private int maxMonsterCnt = 30;

    private float maxSpawnDistance = 10;
    private float minSpawnDistance = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (curMonsterCnt >= maxMonsterCnt)
        {
            return;
        }
        SpawnMonster(0);
    }
    public void SpawnMonster(int monsterId)
    {
        GameObject temp = ObjectPoolManager.Instance.Pools[monsterId + 1].GetObject();

        temp.transform.position = GetSpawnPosition();
        curMonsterCnt++;
    }

    private Vector2 GetSpawnPosition()
    {
        // 플레이어 위치 기준으로 랜덤 방향과 거리 계산
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 spawnPosition = (Vector2)GameObject.FindWithTag("Player").transform.position + direction * distance;

        return spawnPosition;
    }
}
