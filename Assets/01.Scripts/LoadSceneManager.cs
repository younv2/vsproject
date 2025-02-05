using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "00.Scenes/02.InGame";

    private IEnumerator Start()
    {
        // �ε� UI ǥ��
        LoadingUI.Instance.ShowLoading();

        // Ǯ �ʱ�ȭ ����
        yield return StartCoroutine(ObjectPoolManager.Instance.InitPools());

        yield return StartCoroutine(DataManager.Instance.LoadAllData());

        // �ε� UI ����
        LoadingUI.Instance.HideLoading();

        // MainScene �ε�
        SceneManager.LoadScene(nextSceneName);
    }
}