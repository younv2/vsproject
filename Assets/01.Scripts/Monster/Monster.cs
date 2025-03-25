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
        player = GameObject.FindWithTag(Global.UnityTag.PLAYER_TAG);
    }

    public void ManualFixedUpdate()
    {
        if (player == null) 
            return;

        transform.position  = Vector3.MoveTowards(transform.position, player.transform.position, baseData.MoveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        stat.TakeDamage(damage);

        var text = poolManager.GetPool<DamageTextUI>(Global.PoolKey.DAMAGE_TEXT).GetObject();
        text.transform.position = transform.position + new Vector3(0,1.5f,0);
        text.Setup(damage);
        hPBarUI ??= poolManager.GetPool<HPBarUI>(Global.PoolKey.HPBAR).GetObject();
        hPBarUI.Setup(this.transform,stat.GetCurrentHPPercent());
        if (stat.IsDead()&&this.isActiveAndEnabled)
        {
            OnDeath();
        }
    }

    public void Remove()
    {
        if(hPBarUI != null)
            hPBarUI.Remove();
        hPBarUI = null;
        poolManager.GetPool<Monster>(name).ReleaseObject(this);
        MonsterSpawnManager.Instance.MonsterDic.Remove(gameObject.GetInstanceID());
    }

    public void OnDeath()
    {
        Remove();
        DropExp();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var character = BattleManager.Instance.GetCharacterFromInstanceId(collision.gameObject.GetInstanceID());
        if (character != null)
        {
            character.TakeDamage(stat.AttackPower);
        }
    }

    public void DropExp()
    {
        var expItem = (ExpItem)poolManager.GetPool<Item>(Global.PoolKey.EXP_ITEM).GetObject();
        expItem.SetExp(Mathf.RoundToInt(baseData.Exp*DataManager.Instance.TimeBasedBattleScalers.GetCurrentMonsterExpMultiple()));
        expItem.transform.position = transform.position;
        BattleManager.Instance.itemDic.Add(expItem.gameObject.GetInstanceID(), expItem);
    }
}
