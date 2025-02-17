using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(float damage)
    {
        text.text = damage.ToString();
        var target = transform.position;
        target.y += 0.5f;
        transform.DOMove(target, 1f).OnComplete(
            () =>
            ObjectPoolManager.Instance.GetPool<DamageTextUI>("DamageText").ReleaseObject(this));
    }
}
