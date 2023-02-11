using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject environmentTop; //temporary Test
    [SerializeField] private GameObject[] environmentPlanes; //temporary Test
    [SerializeField] private int numberOfPLanes = 4;

    private int movesSinceRepop = 0; //the number of moves since the last time the top environment tile was repopulated
    // Start is called before the first frame update
    void Start()
    {
       // GameObject[] grass
        environmentPlanes = GameObject.FindGameObjectsWithTag("Grass").Concat(GameObject.FindGameObjectsWithTag("Water").Concat(GameObject.FindGameObjectsWithTag("Road"))).ToArray();
        SpawnStartTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
                TerrainMoves();
         
        }
    }

    // this uses the other functions to automatically move the terrain down until it reaches a lower maximum on the z position, then it will be deleted
    //and a new terrain item will be placed at the top of the viewable area
    private void TerrainMoves()
    {
        MoveAllDown();
        movesSinceRepop = movesSinceRepop + 1;

        if(movesSinceRepop >= 7)
        {
            RemoveBottom();
            movesSinceRepop = 0;
        }

    }

    //this moves all active terrain down one unit
    private void MoveAllDown()
    {
        for (int i = 0; i < environmentPlanes.Length; i++)
            {
                if (environmentPlanes[i].gameObject.activeInHierarchy)
                {
                    environmentPlanes[i].gameObject.transform.position = new Vector3(environmentPlanes[i].gameObject.transform.position.x, environmentPlanes[i].gameObject.transform.position.y, environmentPlanes[i].gameObject.transform.position.z - 1);
                }
            }
    }
    
    //this finds the bottom terrain and uses the MoveToTop function to remove it from the bottom of the terrain stack
    private void RemoveBottom()
    {
         GameObject bottomObject = environmentPlanes[0];
            for (int i = 0; i < environmentPlanes.Length; i++)
            {
                if (environmentPlanes[i].gameObject.transform.position.z <= bottomObject.transform.position.z)
                {
                    bottomObject = environmentPlanes[i];
                }
            }
            MoveToTop(bottomObject);
    }

    //this moves removes the bottom terrain and hides it at the top of the terrain tool to be spawned later
    private void MoveToTop(GameObject nextInSpawn)
    {
        nextInSpawn.SetActive(false);
        nextInSpawn.gameObject.transform.position = environmentTop.transform.position;

       // nextInSpawn.SetActive(true); //THIS NEEDS TO CHANGE FOR RANDOMIZATION!!!
        SpawnNextTerrain();
    }

    private void SpawnNextTerrain()
    {
        bool nextActivated = false;

        while(nextActivated == false)
        {
            int i = Random.Range(0, environmentPlanes.Length);
            if (environmentPlanes[i].gameObject.activeInHierarchy == false)
            {
                environmentPlanes[i].gameObject.SetActive(true);
                nextActivated = true;
            }
        }

    }

    //This randomly generates the first four (or however many we will need)terrain tiles for the start of the game
    private void SpawnStartTerrain()
    {
        int unitsDown = 0;
        for (int i = 0; i < environmentPlanes.Length; i++)
            {
                environmentPlanes[i].gameObject.SetActive(false);
            }

        for(int i = 0; i< numberOfPLanes; i++)
        {
            bool anotherActivated = false;
            while(anotherActivated == false)
            {
                int j = Random.Range(0, environmentPlanes.Length);
                if (environmentPlanes[j].gameObject.activeInHierarchy == false)
                {
                 environmentPlanes[j].gameObject.transform.position = new Vector3(environmentPlanes[j].gameObject.transform.position.x, environmentPlanes[i].gameObject.transform.position.y, environmentPlanes[i].gameObject.transform.position.z - unitsDown);
                 environmentPlanes[j].gameObject.SetActive(true);
                 unitsDown = unitsDown + 7; //MAGIC NUMBER based on size of planes
                 anotherActivated = true;
                }
            }

        }
    }
}
