using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManager;
    TimeManager timeManager;
    MonsterSpawnManager monsterSpawnManager;
    public Dictionary<int,PlayableCharacter> playableCharacter;
    public Dictionary<int, Item> itemDic;
    public List<Projectile> projectileList;
    private bool isPause = false;

    protected override void Awake()
    {
        base.Awake();
        skillManager = SkillManager.Instance;
        timeManager = TimeManager.Instance;
        monsterSpawnManager = MonsterSpawnManager.Instance;
        itemDic = new Dictionary<int, Item>();
        playableCharacter = new Dictionary<int, PlayableCharacter>();
        var character = ObjectPoolManager.Instance.GetPool<PlayableCharacter>(Global.PoolKey.CHARACTER).GetObject();
        playableCharacter.Add(character.gameObject.GetInstanceID(),character);
        skillManager.LearnSkill(DataManager.Instance.GetSkillData(Global.PLAYER_FIRST_SKILL_NAME));
    }

    void Update()
    {
        if (isPause)
            return;
        skillManager.ManualUpdate();
        timeManager.ManualUpdate();
        monsterSpawnManager.ManualUpdate();
    }
    private void FixedUpdate()
    {
        if (isPause)
            return;
        foreach(var data in playableCharacter)
        {
            data.Value.ManualFixedUpdate();
        }
        foreach (var data in monsterSpawnManager.MonsterDic)
        {
            data.Value.ManualFixedUpdate();
        }
        for(int i = projectileList.Count-1; i >= 0; i--)
        {
            projectileList[i].ManualFixedUpdate();
        }

    }
    /// <summary>
    /// 게임 리셋
    /// </summary>
    public void GemeReset()
    {
        foreach (var key in playableCharacter.Keys.ToList())
        {
            playableCharacter[key].Remove();
        }
        foreach (var key in monsterSpawnManager.MonsterDic.Keys.ToList())
        {
            monsterSpawnManager.MonsterDic[key].Remove();
        }
        foreach (var key in itemDic.Keys.ToList())
        {
            itemDic[key].Remove();
        }
        for (int i = projectileList.Count - 1; i >= 0; i--)
        {
            projectileList[i].Remove();
        }
        var character = ObjectPoolManager.Instance.GetPool<PlayableCharacter>(Global.PoolKey.CHARACTER).GetObject();
        playableCharacter.Add(character.gameObject.GetInstanceID(),character);
        SkillManager.Instance.Reset();
        TimeManager.Instance.Reset();
        skillManager.LearnSkill(DataManager.Instance.GetSkillData(Global.PLAYER_FIRST_SKILL_NAME));

        
    }
    /// <summary>
    /// 게임 일시정지
    /// </summary>
    /// <param name="flag"></param>
    public void Pause(bool flag)
    {
        isPause = flag;
    }
    #region Gettter
    /// <summary>
    /// 캐릭터 반환
    /// </summary>
    /// <returns></returns>
    public PlayableCharacter GetPlayableCharacter()
    {
        return playableCharacter.Values.First();
    }
    /// <summary>
    /// 인스턴스 ID를 통해 캐릭터 반환
    /// </summary>
    /// <param name="instanceId"></param>
    /// <returns></returns>
    public PlayableCharacter GetCharacterFromInstanceId(int instanceId)
    {
        if(playableCharacter.ContainsKey(instanceId))
            return playableCharacter[instanceId];
        else
            return null;
    }
    /// <summary>
    /// 인스턴스 ID를 통해 몬스터 반환
    /// </summary>
    /// <param name="instanceId"></param>
    /// <returns></returns>
    public Monster GetMonsterFromInstanceId(int instanceId)
    {
        if (monsterSpawnManager.MonsterDic.ContainsKey(instanceId))
            return monsterSpawnManager.MonsterDic[instanceId];
        else
            return null;
    }
    /// <summary>
    /// 인스턴스 ID를 통해 아이템 반환
    /// </summary>
    /// <param name="instanceId"></param>
    /// <returns></returns>
    public Item GetItemFromInstanceId(int instanceId)
    {
        if (itemDic.ContainsKey(instanceId))
            return itemDic[instanceId];
        else
            return null;
    }
    #endregion
}
