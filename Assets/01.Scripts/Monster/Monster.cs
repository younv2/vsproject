using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, IPoolable
{
    [SerializeField] private MonsterData baseData;
    private MonsterStat stat = new MonsterStat();
    private GameObject player;
    private HPBarUI hPBarUI;
    private ObjectPoolManager poolManager;
    public void Start()
    {
        poolManager = ObjectPoolManager.Instance;
    }
    public void OnEnable()
    {
        stat.InitStat(baseData);
        player = GameObject.FindWithTag(Global.Unity.PLAYER_TAG);
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

        var text = poolManager.GetPool<DamageTextUI>(Global.PoolKey.DAMAGE_TEXT).GetObject();
        text.transform.position = transform.position + new Vector3(0,1.5f,0);
        text.Setup(damage);
        hPBarUI ??= poolManager.GetPool<HPBarUI>(Global.PoolKey.HPBAR).GetObject();//Todo: 20번 나오고 안나오는 문제 확인
        hPBarUI.Setup(this.transform,stat.GetCurrentHPPercent());
        if (stat.IsDead()&&this.isActiveAndEnabled)
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
        hPBarUI = null;
        DropExp();
        poolManager.GetPool<Monster>(name).ReleaseObject(this);
        MonsterSpawnManager.Instance.MonsterList.Remove(this);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayableCharacter>();

        if(player != null)
        {
            player.TakeDamage(stat.AttackPower);
        }
    }
    /// <summary>
    /// 경험치 드랍
    /// </summary>
    public void DropExp()
    {
        var expItem = poolManager.GetPool<Item>(Global.PoolKey.EXP_ITEM).GetObject();
        expItem.transform.position = transform.position;
    }
}
