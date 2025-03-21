using System;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private float gameTime;
    public float GameTime {  get { return gameTime; } }

    public Action OnTimeChanged;
    public void ManualUpdate()
    {
        gameTime += Time.deltaTime;
        OnTimeChanged?.Invoke();
        if (gameTime > Global.CLEAR_TIME)
        {
            UIManager.Instance.gameResultPopup.Show(true);
        }
    }

    internal void Reset()
    {
        OnTimeChanged?.Invoke();
        gameTime = 0;
    }
}
