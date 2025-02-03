using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData",  menuName = "Scriptable Object/MonsterData")]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private int monsterId;
    public int MonsterId { get { return monsterId; } }
    [SerializeField]
    private string monsterName;
    public string MonsterName { get {  return monsterName; } }
    [SerializeField]
    private float maxHp;
    public float MaxHp { get { return maxHp; } }
    [SerializeField]
    private int attackPower;
    public int AttackPower { get { return attackPower; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed;} }
}
