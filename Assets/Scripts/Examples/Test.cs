using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float Health;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Random.InitState(2345);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);

            //Debug.Log(Random.Range(0f,1f));
            Debug.Log(Random.value);

        }
      
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Health > 0)
    //    {
    //        Health -= Time.deltaTime;
    //    }
    //    else if (Health <= 0)
    //    {
    //        Debug.Log("Player destroyed!");
    //        Destroy(gameObject);
    //    }
    //    Debug.Log(Health);
    //}
}
