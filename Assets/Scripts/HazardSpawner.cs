using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    bool left, right = false;
    public int speed;
    HazardManager hazardManager;

    public bool beginSpawning = true;

    private void Start()
    {
        hazardManager = transform.parent.GetComponent<HazardManager>();

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
        if(beginSpawning && hazardManager.managerIsReady)
        {
            StartCoroutine(ReadySpawnHazard());
        }
        
    }

    IEnumerator ReadySpawnHazard()
    {
        beginSpawning = false;
        hazardManager.SpawnHazard(gameObject, right, left, speed);
        yield return new WaitForSeconds(Random.Range(1, 4));
        beginSpawning = true;
    }
}
