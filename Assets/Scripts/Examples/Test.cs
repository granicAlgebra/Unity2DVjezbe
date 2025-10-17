using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
            Health -= Time.deltaTime;
        }
        else if (Health <= 0)
        {
            Debug.Log("Player destroyed!");
            Destroy(gameObject);
        }
        Debug.Log(Health);
    }
}
