using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    bool left, right = false;
    CarManager carManager;
    List<GameObject> carPool = new List<GameObject>();

    bool beginSpawning = true;

    private void Start()
    {
        carManager = transform.parent.GetComponent<CarManager>();

        if(gameObject.CompareTag("Right"))
        {
            right = true;
        }
        else
        {
            left = true;
        }
    }

    private void Update()
    {
        if(beginSpawning && gameObject.activeSelf)
        {
            StartCoroutine(SpawnCar());
        }
        
    }

    IEnumerator SpawnCar()
    {
        beginSpawning = false;
        carManager.SpawnCar(gameObject, right, left);
        yield return new WaitForSeconds(Random.Range(3, 6));
        beginSpawning = true;
    }
}
