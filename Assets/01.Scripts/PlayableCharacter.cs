using UnityEngine;

public class PlayableCharacter : MonoBehaviour, IPoolable
{
    [SerializeField]private Scanner monsterScanner;
    [SerializeField]private Scanner expItemScanner;
    private CharacterStat stat = new CharacterStat();
    private PlayerController playerController;
    private HPBarUI hPBarUI;
    private bool isFlip = false;
    public CharacterStat Stat { get { return stat; } }
    public bool IsFlip { get {  return isFlip; } set { isFlip = value; } }

    public void ManualFixedUpdate()
    {
        DrainExp();
        playerController.ManualFixedUpdate();
    }
    void OnEnable()
    {
        Stat.Init();
        playerController = this.GetComponent<PlayerController>();
    }

    public void Remove()
    {
        transform.position = Vector3.zero;
        GetComponent<PersistentVfxController>()?.Clean();
        if(hPBarUI != null)
            hPBarUI.Remove();
        playerController.ResetInput();
        ObjectPoolManager.Instance.GetPool<PlayableCharacter>(name).ReleaseObject(this);
        BattleManager.Instance.playableCharacter.Remove(gameObject.GetInstanceID());
    }

    public void OnDeath()
    {
        Remove();
        UIManager.Instance.gameResultPopup.Show(false);
    }

    public void DrainExp()
    {
        expItemScanner.range = StatCalculator.CalculateModifiedDrop(SkillManager.Instance.GetAllPassiveStat(),Stat.DrainItemRange);
        if (expItemScanner.hitList != null)
        {
            foreach (var item in expItemScanner.hitList)
            {
                item.transform.Translate(Vector3.Normalize(transform.position - item.transform.position) * Time.deltaTime * 5f);
            }
        }
        var hit = Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask(Global.UnityLayer.EXP_LAYER));
        if (hit != null)
        {
            var item = BattleManager.Instance.GetItemFromInstanceId(hit.gameObject.GetInstanceID());
            item.Use();
        }
    }

    internal void TakeDamage(float damage)
    {
        stat.TakeDamage(damage);
        var text = ObjectPoolManager.Instance.GetPool<DamageTextUI>("DamageText").GetObject();
        text.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        text.Setup(damage);
        hPBarUI ??= ObjectPoolManager.Instance.GetPool<HPBarUI>("HPBar").GetObject();
        hPBarUI.Setup(this.transform, stat.GetCurrentHPPercent());
        if (stat.IsDead())
        {
            OnDeath();
        }
    }

    public Transform GetNearstTarget() => monsterScanner.nearstObject == null ? null : monsterScanner.nearstObject.transform;
}
