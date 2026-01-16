using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectSaveData
{
    public string Id;
    public List<SaveFieldEntry> Fields = new List<SaveFieldEntry>();
    public Vector3 Position;
    public Quaternion Rotation;
    public bool Enabled;
}

[System.Serializable]
public class SaveFieldEntry
{
    public string Key;
    public string Value;
    public string Type;
}

[System.Serializable]
public class SaveSnapshot
{
    public string SceneName;
    public List<ObjectSaveData> Objects;
}