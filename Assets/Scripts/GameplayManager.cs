using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance {get; private set;}

    [SerializeField] TextMeshProUGUI _coinText;
    [SerializeField] Transform _heartContainer;
    [SerializeField] GameObject _heartPrefab;
    [SerializeField] GameObject _player;

    private int _coins = 0;
    private int _lives = 3;
    List<GameObject> _hearts = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        for (int  i = 0; i < _lives;  i++)
        {
            _hearts.Add(Instantiate(_heartPrefab, _heartContainer));
        }
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        _coinText.text = _coins.ToString(); 
    }

    public void AddHeart()
    {
        _hearts.Add(Instantiate(_heartPrefab, _heartContainer));
        _lives++;
    }

    public void RemoveHeart()
    {
        if (_lives <= 0)
            return;
       
        Destroy(_hearts[0]);    
        _hearts.RemoveAt(0);
        _lives--;

        if (_lives == 0)
        {
            Destroy(_player);
        }
    }
}
