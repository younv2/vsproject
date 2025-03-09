using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManager;
    TimeManager timeManager;
    MonsterSpawnManager monsterSpawnManager;
    PlayableCharacter playableCharacter;
    public List<Projectile> projectileList;
    private bool isPause = false;

    protected override void Awake()
    {
        base.Awake();
        skillManager = SkillManager.Instance;
        timeManager = TimeManager.Instance;
        monsterSpawnManager = MonsterSpawnManager.Instance;
        playableCharacter = ObjectPoolManager.Instance.GetPool<PlayableCharacter>(Global.PoolKey.CHARACTER).GetObject();
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
        playableCharacter.ManualFixedUpdate();
        foreach (var data in monsterSpawnManager.MonsterList)
        {
            data.ManualFixedUpdate();
        }
        for(int i = projectileList.Count-1; i >= 0; i--)
        {
            projectileList[i].ManualFixedUpdate();
        }

        //TODO - Physics Settings의 Simulation Monde 추후 Script로 수정해서 직접 Physics 관리 할 것 
    }
    public PlayableCharacter GetPlayableCharacter()
    {
        return playableCharacter;
    }
    /// <summary>
    /// 게임 일시정지
    /// </summary>
    /// <param name="flag"></param>
    public void Pause(bool flag)
    {
        isPause = flag;
    }
}
