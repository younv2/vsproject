using JetBrains.Annotations;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolable
{
    [SerializeField]private MonsterData baseData;
    private MonsterStat stat;

    public void ManualFixedUpdate()
    {
        transform.position  = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").gameObject.transform.position, baseData.MoveSpeed * Time.deltaTime);
    }
    public void TakeDamage(float damage)
    {
        Debug.Log($"{damage}의 데미지를 입음");

    }
}
