using UnityEngine;

public class LoadingUI : MonoSingleton<LoadingUI>
{
    [SerializeField] private GameObject loadingPanel;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void ShowLoading()
    {
        loadingPanel.SetActive(true);
    }

    public void HideLoading()
    {
        loadingPanel.SetActive(false);
    }
}