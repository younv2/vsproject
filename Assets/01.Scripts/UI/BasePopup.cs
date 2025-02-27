using UnityEngine;
using UnityEngine.UI;

public class BasePopup : MonoBehaviour
{
    public Button closeBtn;
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
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
