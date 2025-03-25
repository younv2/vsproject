using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "00.Scenes/02.InGame";

    private IEnumerator Start()
    {
        LoadingUI.Instance.ShowLoading();

        // 풀 초기화 진행
        yield return StartCoroutine(DataManager.Instance.LoadAllData());
        yield return StartCoroutine(ObjectPoolManager.Instance.InitPools());

        LoadingUI.Instance.HideLoading();

        // MainScene 로드
        SceneManager.LoadScene(nextSceneName);
    }
}