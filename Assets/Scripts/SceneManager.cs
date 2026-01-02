using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [SerializeField] private List<string> _sceneNames = new List<string>();

    private UnityEngine.SceneManagement.Scene _currentScene;  

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public async void ChangeSceneAsync(string sceneName)
    {
        if (_currentScene.Equals(sceneName))
        {
            return;
        }
        else if (_currentScene.IsValid() && _currentScene.isLoaded)
        {
            await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_currentScene);
        }

        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _currentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
    }

    public void ChangeScene(string sceneName)
    {
        ChangeSceneAsync(sceneName);
    }
}