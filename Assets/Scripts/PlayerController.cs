using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Weapon CurrentWeapon;

    void Start()
    {
        InputManager.Instance.AttackInput += () => CurrentWeapon.Attack();
    }
}
