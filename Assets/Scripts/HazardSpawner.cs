using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Joel Hill
//Section: 2023SP.SGD.285.4171
//Instructor: Aurore Locklear
/////////////////////////////////////////////
///
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
            if(hazardManager.isARoad)
            {
                StartCoroutine(ReadySpawnCar());
            }
            else
            {
                StartCoroutine(ReadySpawnLog());
            }
            
        }
        
    }

    IEnumerator ReadySpawnCar()
    {
        beginSpawning = false;
        hazardManager.SpawnHazard(gameObject, right, left, speed);
        yield return new WaitForSeconds(Random.Range(1, 4));
        beginSpawning = true;
    }

    IEnumerator ReadySpawnLog()
    {
        beginSpawning = false;
        hazardManager.SpawnHazard(gameObject, right, left, speed);
        yield return new WaitForSeconds(Random.Range(2, 4));
        beginSpawning = true;
    }
}
