using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolable
{
    [SerializeField] private MonsterData baseData;
    private MonsterStat stat = new MonsterStat();
    private GameObject player;
    private HPBarUI hPBarUI;
    public void OnEnable()
    {
        stat.InitStat(baseData);
        player = GameObject.FindWithTag("Player");
    }
    /// <summary>
    /// 매니저 클래스에서 업데이트를 관리하기 위함.
    /// </summary>
    public void ManualFixedUpdate()
    {
        if (player == null)
            return;
        transform.position  = Vector3.MoveTowards(transform.position, player.transform.position, baseData.MoveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 몬스터 체력 감소
    /// </summary>
    public void TakeDamage(float damage)
    {
        Debug.Log($"{damage}의 데미지를 입음");
        stat.TakeDamage(damage);

        var text = ObjectPoolManager.Instance.GetPool<DamageTextUI>("DamageText").GetObject();
        text.transform.position = transform.position + new Vector3(0,1.5f,0);
        text.Setup(damage);
        hPBarUI ??= ObjectPoolManager.Instance.GetPool<HPBarUI>("HPBar").GetObject();
        hPBarUI.Setup(this.transform,stat.GetCurrentHPPercent());
        if (stat.IsDead())
        {
            OnDeath();
        }
    }
    /// <summary>
    /// 몬스터 사망 처리
    /// </summary>
    public void OnDeath()
    {
        hPBarUI.Remove();
        var expItem = ObjectPoolManager.Instance.GetPool<Item>("ExpItem1").GetObject();
        expItem.transform.position = transform.position;
        ObjectPoolManager.Instance.GetPool<Monster>(name.Replace("(Clone)","")).ReleaseObject(this);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayableCharacter>();

        if(player != null)
        {
            player.TakeDamage(stat.AttackPower);
        }
    }
}
