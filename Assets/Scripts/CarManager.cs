using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] List<GameObject> rightSpawners = new List<GameObject>();
    [SerializeField] List<GameObject> leftSpawners = new List<GameObject>();
    public List<GameObject> carPool = new List<GameObject>();
    public GameObject vehicle;

    private void Start()
    {
        //Grab all the cars in the car pool and set them inactive by default
        foreach(Transform car in GameObject.Find("CarPool").transform)
        {
            car.gameObject.SetActive(false);
            carPool.Add(car.gameObject);
        }

        //Right spawners are set as the active default spawner
        foreach (GameObject spawner in rightSpawners)
        {
            spawner.SetActive(true);
        }
        foreach (GameObject spawner in leftSpawners)
        {
            spawner.SetActive(false);
        }

        InvokeRepeating("ChooseActiveSpawner", 1, 15);
    }

    private void ChooseActiveSpawner()
    {
        int randomNum = 0;

        for(int x = 0; x < rightSpawners.Count; x++)
        {
            randomNum = Random.Range(0, 2);
            if (randomNum == 0)
            {
                //Right side is active
                rightSpawners[x].SetActive(true);
                leftSpawners[x].SetActive(false);
            }
            else
            {
                //Left side is active
                rightSpawners[x].SetActive(false);
                leftSpawners[x].SetActive(true);
            }
        }
    }

    public void SpawnCar(GameObject spawner, bool left, bool right)
    {
        GameObject activeCar = null;

        foreach (GameObject car in carPool)
        {
            if (!car.activeSelf)
            {
                activeCar = car;
                break;
            }
        }

        if(left)
        {
            activeCar.tag = "Left";
        }
        else
        {
            activeCar.tag = "Right";
        }

        activeCar.transform.position = spawner.transform.position;
        activeCar.transform.parent = gameObject.transform;
        activeCar.SetActive(true);
    }
}
