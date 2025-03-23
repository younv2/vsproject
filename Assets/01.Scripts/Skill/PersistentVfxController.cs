using System;
using UnityEngine;

public class PersistentVfxController : MonoBehaviour
{
    public Action OnOwnerDestroyed;

    private void OnDisable()
    {
        Clean();
    }

    private void OnDestroy()
    {
        Clean();
    }
    private void Clean()
    {
        if (OnOwnerDestroyed != null)
        {
            OnOwnerDestroyed.Invoke();
            OnOwnerDestroyed = null;
        }
    }
}
