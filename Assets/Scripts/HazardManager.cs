using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> rightSpawners = new List<GameObject>();
    [SerializeField] List<GameObject> leftSpawners = new List<GameObject>();
    List<GameObject> hazardPool = new List<GameObject>();
    List<GameObject> activeHazardList = new List<GameObject>();
    [SerializeField] Transform hazardPoolObject;

    public bool managerIsReady = false;
    public bool isCarManager;

    private void Start()
    {

        //Grab all the cars in the car pool and set them inactive by default
        foreach(Transform hazard in hazardPoolObject)
        {
            hazard.gameObject.SetActive(false);
            hazardPool.Add(hazard.gameObject);
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
    public void ChooseActiveSpawners()
    {
        int randomNum = 0;
        int randomSpeed = 0;

        for(int x = 0; x < rightSpawners.Count; x++)
        {
            randomNum = Random.Range(0, 2);
            randomSpeed = Random.Range(3, 5);
            if (randomNum == 0)
            {
                //Right side is active
                rightSpawners[x].SetActive(true);
                leftSpawners[x].SetActive(false);
                rightSpawners[x].GetComponent<HazardSpawner>().speed = randomSpeed;
            }
            else
            {
                //Left side is active
                rightSpawners[x].SetActive(false);
                leftSpawners[x].SetActive(true);
                leftSpawners[x].GetComponent<HazardSpawner>().speed = randomSpeed;
            }
        }

        managerIsReady = true;
    }

    public void SpawnHazard(GameObject spawner, bool left, bool right, int speed)
    {

        //Find an inactive car in the car pool
        GameObject activeHazard = null;

        foreach (GameObject hazard in hazardPool)
        {
            if (!hazard.activeSelf)
            {
                activeHazard = hazard;
                break;
            }
        }

        //Set it going left or right
        if(left)
        {
            activeHazard.tag = "Left";
        }
        else
        {
            activeHazard.tag = "Right";
        }

        //Activate the car and set its parent and position
        activeHazard.transform.position = spawner.transform.position;
        activeHazard.transform.parent = gameObject.transform;
        activeHazard.GetComponent<HazardBehaviour>().logSpeed = speed;
        activeHazard.SetActive(true);

        activeHazardList.Add(activeHazard);
    }

    public void RemoveHazard(GameObject hazard)
    {
        //Deactivate the car and reset its parent and position
        hazard.SetActive(false);
        hazard.transform.parent = hazardPoolObject;
        hazard.transform.position = hazardPoolObject.position;

        activeHazardList.Remove(hazard);
    }

    public void RemoveActiveHazards()
    {
        foreach(GameObject hazard in activeHazardList)
        {
            hazard.SetActive(false);
            hazard.transform.parent = hazardPoolObject;
            hazard.transform.position = hazardPoolObject.position;
        }

        activeHazardList.Clear();

        foreach(GameObject spawner in leftSpawners)
        {
            spawner.GetComponent<HazardSpawner>().beginSpawning = true;
        }

        foreach (GameObject spawner in rightSpawners)
        {
            spawner.GetComponent<HazardSpawner>().beginSpawning = true;
        }
    }
}