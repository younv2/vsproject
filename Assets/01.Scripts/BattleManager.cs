using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManager;
    TimeManager timeManager;
    MonsterSpawnManager monsterSpawnManager;
    Dictionary<int,PlayableCharacter> playableCharacter;
    public List<Projectile> projectileList;
    private bool isPause = false;

    protected override void Awake()
    {
        base.Awake();
        skillManager = SkillManager.Instance;
        timeManager = TimeManager.Instance;
        monsterSpawnManager = MonsterSpawnManager.Instance;
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
            data.Value.ManualFixedUpdate();
        foreach (var data in monsterSpawnManager.MonsterDic)
        {
            data.Value.ManualFixedUpdate();
        }
        for(int i = projectileList.Count-1; i >= 0; i--)
        {
            projectileList[i].ManualFixedUpdate();
        }

        //TODO - Physics Settings의 Simulation Monde 추후 Script로 수정해서 직접 Physics 관리 할 것 
    }
    public PlayableCharacter GetPlayableCharacter()
    {
        return playableCharacter.Values.First();
    }
    /// <summary>
    /// 게임 일시정지
    /// </summary>
    /// <param name="flag"></param>
    public void Pause(bool flag)
    {
        isPause = flag;
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
}
