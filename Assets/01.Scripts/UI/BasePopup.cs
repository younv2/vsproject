using UnityEngine;
using UnityEngine.UI;

public class BasePopup : MonoBehaviour
{
    public Button closeBtn;
    public void Initialize()
    {
        if (transform.Find("CloseBtn") != null)
        {
            closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
            closeBtn.onClick.AddListener(() =>
            {
                Close();
            });
        }
        Close();
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
