using UnityEngine;
using System;

public class Saveable : MonoBehaviour
{

    [Header("Optional Built-in State")]
    public bool saveTransform = false;

    [SerializeField, HideInInspector]
    private string _uniqueId;
    public string UniqueId => _uniqueId;

#if (UNITY_EDITOR)
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(_uniqueId))
        {
            _uniqueId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif

    private void Awake()
    {
        SaveManager.Register(this);
    }
}
