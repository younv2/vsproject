using System;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private float gameTime;
    public float GameTime {  get { return gameTime; } }

    public Action<float> OnTimeChanged;

    private void Start()
    {
        OnTimeChanged += (GameTime) => DataManager.Instance.TimeBasedBattleScalers.SetCurrentLevel(GameTime);
    }
    public void ManualUpdate()
    {
        gameTime += Time.deltaTime;
        OnTimeChanged?.Invoke(GameTime);
        if (gameTime > Global.CLEAR_TIME)
        {
            UIManager.Instance.gameResultPopup.Show(true);
        }
    }

    internal void Reset()
    {
        OnTimeChanged?.Invoke(GameTime);
        gameTime = 0;
    }
}
