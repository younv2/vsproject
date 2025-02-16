using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolable
{
    [SerializeField] private MonsterData baseData;
    private MonsterStat stat = new MonsterStat();
    public void OnEnable()
    {
        stat.InitStat(baseData);
    }
    /// <summary>
    /// 매니저 클래스에서 업데이트를 관리하기 위함.
    /// </summary>
    public void ManualFixedUpdate()
    {
        transform.position  = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").gameObject.transform.position, baseData.MoveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 몬스터 체력 감소
    /// </summary>
    public void TakeDamage(float damage)
    {
        Debug.Log($"{damage}의 데미지를 입음");
        stat.TakeDamage(damage);
        if(stat.IsDead())
        {
            OnDeath();
        }


    }
    /// <summary>
    /// 몬스터 사망 처리
    /// </summary>
    public void OnDeath()
    { 
        ObjectPoolManager.Instance.GetPool<Monster>(name.Replace("(Clone)","")).ReleaseObject(this);
    }
}
