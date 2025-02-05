using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.AddressableAssets.Addressables;

public class AddressablesLoader
{
    public void LoadAssetAsync<T>(AssetReference list)
    {
        T result = default(T);

        Addressables.LoadAssetAsync<T>(list).Completed += (op) =>
        {
            Debug.Log(op.ToString());
        };
    }
    public void LoadAssetListAsync<T>(List<AssetReference> list, Action<List<T>> callback = null)
    {
        List<T> result  = new List<T>();

        Addressables.LoadAssetsAsync<T>(list, asset =>
        {
            result.Add(asset);
            callback?.Invoke(result);
        }, MergeMode.Union).Completed += HandleCompletion;
    }

    private void HandleCompletion<T>(AsyncOperationHandle<IList<T>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("All assets loaded successfully!");
        }
        else
        {
            Debug.LogError("Failed to load assets.");
        }
    }
}
