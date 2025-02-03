using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    public List<SkillData> skillDataList;
    private void Start()
    {
        Resources.LoadAll<SkillData>("ScriptableObject/");
    }
}
