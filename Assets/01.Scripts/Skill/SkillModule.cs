using UnityEngine;

/// <summary>
/// 스킬/투사체가 실행할 "행동"을 정의하는 추상 클래스
/// (스킬 발동 시점, 투사체 충돌 시점 등에 호출)
/// </summary>
public abstract class SkillModule : ScriptableObject
{
    // 1) 스킬 발동 시점에 실행되는 메서드
    public virtual void Execute(SkillRuntime skill) { }

    // 2) 투사체 이벤트(충돌, 만료 등) 시점에 실행되는 메서드
    public virtual void Execute(ProjectileContext context) { }
}