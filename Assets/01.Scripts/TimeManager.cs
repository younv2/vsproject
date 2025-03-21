using System;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private float gameTime;
    public float GameTime {  get { return gameTime; } }
    public void ManualUpdate()
    {
        gameTime += Time.fixedDeltaTime;

        if(gameTime > Global.CLEAR_TIME)
        {
            UIManager.Instance.gameResultPopup.Show(true);
        }
    }

    internal void Reset()
    {
        gameTime = 0;
    }
}
