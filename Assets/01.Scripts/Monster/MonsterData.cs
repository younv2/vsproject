using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData",  menuName = "Game/MonsterData")]
public class MonsterData : ScriptableObject
{
    [SerializeField]private int monsterId;
    [SerializeField]private string monsterName;
    [SerializeField]private float maxHp;
    [SerializeField]private int attackPower;
    [SerializeField]private float moveSpeed;
    [SerializeField]private int exp;

    public int MonsterId { get { return monsterId; } }
    public string MonsterName { get {  return monsterName; } }
    public float MaxHp { get { return maxHp; } }
    public int AttackPower { get { return attackPower; } }
    public float MoveSpeed { get { return moveSpeed;} }
    public int Exp { get { return exp; } }
}
