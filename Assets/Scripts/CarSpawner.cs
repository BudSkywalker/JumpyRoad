using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    bool left, right = false;
    CarManager carManager;

    public bool beginSpawning = true;

    private void Start()
    {
        carManager = transform.parent.GetComponent<CarManager>();

        if(gameObject.CompareTag("Left"))
        {
            left = true;
        }
        else
        {
            right = true;
        }
    }

    private void Update()
    {
        print(beginSpawning + " begin spawning");
        if(beginSpawning && carManager.managerIsReady)
        {
            print(carManager.managerIsReady);
            StartCoroutine(SpawnCar());
        }
        
    }

    IEnumerator SpawnCar()
    {
        beginSpawning = false;
        carManager.SpawnCar(gameObject, right, left);
        yield return new WaitForSeconds(Random.Range(1, 4));
        beginSpawning = true;
    }
}
