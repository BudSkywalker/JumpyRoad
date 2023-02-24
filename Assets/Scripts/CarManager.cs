using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] List<GameObject> rightSpawners = new List<GameObject>();
    [SerializeField] List<GameObject> leftSpawners = new List<GameObject>();
    [SerializeField] List<GameObject> carPool = new List<GameObject>();
    List<GameObject> activeCarsList = new List<GameObject>();
    private GameObject carPoolObject;

    public bool managerIsReady = false;
    public float despawnDistance;

    private void Start()
    {
        despawnDistance = rightSpawners[0].transform.position.x;
        carPoolObject = GameObject.Find("CarPool");

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

        ChooseActiveSpawners();
    }

    //Randomize which spawns are active
    private void ChooseActiveSpawners()
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

        managerIsReady = true;
    }

    public void SpawnCar(GameObject spawner, bool left, bool right)
    {

        //Find an inactive car in the car pool
        GameObject activeCar = null;

        foreach (GameObject car in carPool)
        {
            if (!car.activeSelf)
            {
                activeCar = car;
                break;
            }
        }

        //Set it going left or right
        if(left)
        {
            activeCar.tag = "Left";
        }
        else
        {
            activeCar.tag = "Right";
        }

        //Activate the car and set its parent and position
        activeCar.transform.position = spawner.transform.position;
        activeCar.transform.parent = gameObject.transform;
        activeCar.SetActive(true);

        activeCarsList.Add(activeCar);
    }

    public void RemoveCar(GameObject car)
    {
        //Deactivate the car and reset its parent and position
        car.SetActive(false);
        car.transform.parent = carPoolObject.transform;
        car.transform.position = carPoolObject.transform.position;

        activeCarsList.Remove(car);
    }

    public void RemoveActiveCars()
    {
        foreach(GameObject car in activeCarsList)
        {
            car.SetActive(false);
            car.transform.parent = carPoolObject.transform;
            car.transform.position = carPoolObject.transform.position;
        }

        activeCarsList.Clear();

        foreach(GameObject spawner in leftSpawners)
        {
            spawner.GetComponent<CarSpawner>().beginSpawning = true;
        }

        foreach (GameObject spawner in rightSpawners)
        {
            spawner.GetComponent<CarSpawner>().beginSpawning = true;
        }
    }
}
