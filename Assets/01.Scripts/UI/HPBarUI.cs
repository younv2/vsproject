using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Slider slider;
    Transform target;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + new Vector3(0,-1.5f,0);
        }
    }
    /// <summary>
    /// HPBar 초기 세팅
    /// </summary>
    public void Setup(Transform target,float percent)
    {
        this.target ??= target;
        transform.position = target.position + new Vector3(0, -1.5f, 0);
        slider.value = percent;
    }
    /// <summary>
    /// HPBar 제거
    /// </summary>
    internal void Remove()
    {
        target = null;
        ObjectPoolManager.Instance.GetPool<HPBarUI>(name.Replace("(Clone)", "")).ReleaseObject(this);
    }
}
