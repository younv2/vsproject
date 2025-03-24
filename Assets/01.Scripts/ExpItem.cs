using UnityEngine;

public class ExpItem : Item
{
    private int exp;

    public override void Use()
    {
        BattleManager.Instance.GetPlayableCharacter().Stat.AddExp(exp);
        base.Use();
    }

    public void SetExp(int exp)
    {
        this.exp = exp;
    }

}