using Unity.VisualScripting;
using UnityEngine;

public static class Global
{
    public static class UnityTag
    {
        public const string PLAYER_TAG = "Player";
    }
    public static class UnityLayer
    {
        public const string EXP_LAYER = "Exp";
        public const string ENEMY_LAYER = "Enemy";
    }
    public static class PoolKey
    {
        public const string CHARACTER = "Character";
        public const string SLIME = "Slime";
        public const string DAMAGE_TEXT = "DamageText";
        public const string HPBAR = "HPBar";
        public const string EXP_ITEM = "ExpItem1";
        public const string GARLIC_SKILL = "Garlic";
        public const string WHIP_SKILL = "Whip";
    }
    public static class CollectionCapacity
    {
        public const int ITEM_DIC_CAPACITY_INIT_VALUE = 100;
        public const int MONSTER_DIC_CAPACITY_INIT_VALUE = 300;
        public const int CHARACTER_DIC_CAPACITY_INIT_VALUE = 10;
        public const int PROJECTILE_LIST_CAPACITY_INIT_VALUE = 300;
    }
    public const string PLAYER = "Player";
    public const string PLAYER_FIRST_SKILL_NAME = "ThrowRock";
    public const string EXP_TABLE_PATH = "Assets/@Resources/Data/ExpTable.json";
    public const string TIME_BASED_BATTLE_SCALER_TABLE_PATH = "Assets/@Resources/Data/TimeBasedBattleScaler.csv";

    public const string WIN = "승 리";
    public const string LOSE = "패 배";
    public const int CLEAR_TIME = 300;
}
public static class Formatter
{
    /// <summary>
    /// 시간 포맷 문자열 반환
    /// </summary>
    public static string TimeFormat(float time)
    {
        int m = (int)time / 60;
        int s = (int)time % 60;
        int h = m / 60;
        string temp = h == 0 ? $"{m}:{s}" : $"{h}:{m}:{s}";
        return temp;
    }
}
