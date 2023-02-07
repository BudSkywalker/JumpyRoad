using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject groundPlaneW, groundPlaneG, environmentTop; //temporary Test
    [SerializeField] private GameObject[] environmentPlanes; //temporary Test
    // Start is called before the first frame update
    void Start()
    {
        environmentPlanes = GameObject.FindGameObjectsWithTag("Plane");

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            GameObject bottomObject = environmentPlanes[0];
            for (int i = 0; i < environmentPlanes.Length; i++)
            {
                if (environmentPlanes[i].gameObject.transform.position.z <= bottomObject.transform.position.z)
                {
                    bottomObject = environmentPlanes[i];
                }
            }
            SpawnAtTop(bottomObject);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < environmentPlanes.Length; i++)
            {
                if (environmentPlanes[i].gameObject.activeInHierarchy)
                {
                    environmentPlanes[i].gameObject.transform.position = new Vector3(environmentPlanes[i].gameObject.transform.position.x, environmentPlanes[i].gameObject.transform.position.y, environmentPlanes[i].gameObject.transform.position.z - 1);
                }
            }
           
        }
    }


    private void SpawnAtTop(GameObject nextInSpawn)
    {
        nextInSpawn.SetActive(false);
        nextInSpawn.gameObject.transform.position = environmentTop.transform.position;

        nextInSpawn.SetActive(true); //THIS NEEDS TO CHANGE FOR RANDOMIZATION!!!
    }
}
