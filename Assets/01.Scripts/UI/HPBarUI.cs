using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    private Slider slider;
    private Transform target;
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

    public void Setup(Transform target,float percent)
    {
        this.target ??= target;
        transform.position = target.position + new Vector3(0, -1.5f, 0);
        slider.value = percent;
    }

    internal void Remove()
    {
        target = null;
        ObjectPoolManager.Instance.GetPool<HPBarUI>(name).ReleaseObject(this);
    }
}
