using System;
using UnityEngine;

public class PersistentVfxController : MonoBehaviour
{
    public Action OnOwnerDestroyed;

    public void Clean()
    {
        if (OnOwnerDestroyed != null)
        {
            OnOwnerDestroyed.Invoke();
            OnOwnerDestroyed = null;
        }
    }
}
