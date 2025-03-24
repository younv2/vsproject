using UnityEngine;
using UnityEngine.UI;

public class BasePopup : MonoBehaviour
{
    public Button closeBtn;
    public bool isOn;
    /// <summary>
    /// 팝업 초기 설정
    /// </summary>
    public void Initialize()
    {
        if (transform.Find("Close") != null)
        {
            closeBtn = transform.Find("Close").GetComponent<Button>();
            closeBtn.onClick.AddListener(() =>
            {
                Close();
            });
        }
        Close();
    }
    /// <summary>
    /// 팝업 오브젝트 활성화
    /// </summary>
    public virtual void Show()
    {
        gameObject.SetActive(true);
        isOn = true;
    }
    /// <summary>
    /// 팝업 오브젝트 비활성화
    /// </summary>
    public virtual void Close()
    {
        gameObject.SetActive(false);
        isOn = false;
    }
}
