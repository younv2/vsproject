using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManagerInstance;
    TimeManager timeManagerInstance;
    MonsterSpawnManager monsterSpawnManagerInstance;
    PlayableCharacter playableCharacter;
    public List<Projectile> projectileList;
    private bool isPause = false;

    void Awake()
    {
        skillManagerInstance = SkillManager.Instance;
        timeManagerInstance = TimeManager.Instance;
        monsterSpawnManagerInstance = MonsterSpawnManager.Instance;
        playableCharacter = ObjectPoolManager.Instance.GetPool<PlayableCharacter>(Global.CHARACTER).GetObject();
    }

    void Update()
    {
        if (isPause)
            return;
        skillManagerInstance.ManualUpdate();
        timeManagerInstance.ManualUpdate();
        monsterSpawnManagerInstance.ManualUpdate();
    }
    private void FixedUpdate()
    {
        if (isPause)
            return;
        playableCharacter.gameObject.GetComponent<PlayerController>().ManualFixedUpdate();
        playableCharacter.ManualFixedUpdate();
        foreach (var data in monsterSpawnManagerInstance.MonsterList)
        {
            data.ManualFixedUpdate();
        }
        foreach (var data in projectileList)
        {
            data.ManualFixedUpdate();
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
