using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    private static readonly HashSet<Saveable> saveables = new();
    private static string SavePath =>
    System.IO.Path.Combine(Application.persistentDataPath, "savegame.json");

    public static Action OnLoad;

    public static void Register(Saveable saveable)
    {
        saveables.Add(saveable);
    }

    public static void Unregister(Saveable saveable)
    {
        saveables.Remove(saveable);
    }

    public static void SaveToDisk()
    {
        var snapshot = new SaveSnapshot
        {
            SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            Objects = CaptureState()
        };

        string json = JsonUtility.ToJson(snapshot, true);

        File.WriteAllText(SavePath, json);

        Debug.Log($"Game saved to: {SavePath}");
    }

    private static List<ObjectSaveData> CaptureState()
    {
        var result = new List<ObjectSaveData>();

        foreach (var saveable in saveables)
        {
            var data = new ObjectSaveData
            {
                Id = saveable.UniqueId
            };

            data.Enabled = saveable.gameObject.activeInHierarchy;

            if (saveable.saveTransform)
            {
                data.Position = saveable.transform.position;
                data.Rotation = saveable.transform.rotation;
            }

            CaptureFields(saveable, data);

            result.Add(data);
        }

        return result;
    }

    private static void CaptureFields(Saveable saveable, ObjectSaveData data)
    {
        var components = saveable.GetComponents<MonoBehaviour>();

        foreach (var component in components)
        {
            if (component == saveable) continue;

            var type = component.GetType();
            var fields = type.GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<SaveFieldAttribute>() == null)
                    continue;

                object value = field.GetValue(component);
                if (value == null) continue;

                var entry = new SaveFieldEntry
                {
                    Key = $"{type.FullName}.{field.Name}",
                    Value = value.ToString(),
                    Type = value.GetType().AssemblyQualifiedName
                };

                data.Fields.Add(entry);
            }
        }
    }

    public static void LoadState()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("Save file not found.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        var snapshot = JsonUtility.FromJson<SaveSnapshot>(json);

        if (snapshot == null)
        {
            Debug.LogError("Failed to deserialize save snapshot.");
            return;
        }

        RestoreState(snapshot);

        OnLoad?.Invoke();   
    }

    private static void RestoreState(SaveSnapshot snapshot)
    {
        foreach (var objectData in snapshot.Objects)
        {
            var saveable = FindSaveableById(objectData.Id);
            if (saveable == null)
            {
                Debug.LogWarning($"Saveable with id '{objectData.Id}' not found in scene.");
                continue;
            }

            saveable.gameObject.SetActive(objectData.Enabled);
            if (saveable.saveTransform)
                RestoreTransform(saveable, objectData);
            RestoreFields(saveable, objectData);
        }
    }

    private static void RestoreTransform(Saveable saveable, ObjectSaveData data)
    {
        saveable.transform.position = data.Position;
        saveable.transform.rotation = data.Rotation;
    }

    private static void RestoreFields(Saveable saveable, ObjectSaveData data)
    {
        var components = saveable.GetComponents<MonoBehaviour>();

        foreach (var entry in data.Fields)
        {
            foreach (var component in components)
            {
                var type = component.GetType();

                foreach (var field in type.GetFields(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance))
                {
                    if (field.GetCustomAttribute<SaveFieldAttribute>() == null)
                        continue;

                    string key = $"{type.FullName}.{field.Name}";
                    if (key != entry.Key)
                        continue;

                    object value = ParseValue(entry.Value, field.FieldType);
                    field.SetValue(component, value);
                }
            }
        }
    }

    private static object ParseValue(string value, Type type)
    {
        if (type == typeof(int))
            return int.Parse(value);

        if (type == typeof(float))
            return float.Parse(value, CultureInfo.InvariantCulture);

        if (type == typeof(bool))
            return bool.Parse(value);

        if (type == typeof(string))
            return value;

        throw new Exception($"Unsupported save type: {type}");
    }

    private static Saveable FindSaveableById(string id)
    {
        foreach (var saveable in saveables)
        {
            if (saveable.UniqueId == id)
                return saveable;
        }

        return null;
    }

    [System.Serializable]
    private class Wrapper
    {
        public object value;
        public Wrapper(object v) => value = v;
    }
}
