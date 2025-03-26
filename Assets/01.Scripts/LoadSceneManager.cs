using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "00.Scenes/02.InGame";

    private async void Start()
    {
        LoadingUI.Instance.ShowLoading();

        Task loadedData = DataManager.Instance.LoadAllData();
        Task loadedObjectPool =  ObjectPoolManager.Instance.InitPools();
        await Task.WhenAll(loadedData, loadedObjectPool);

        LoadingUI.Instance.HideLoading();

        // MainScene 로드
        SceneManager.LoadScene(nextSceneName);
    }
}