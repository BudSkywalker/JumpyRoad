using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    private static int width = 13;
    private static int height = 5;
    [SerializeField]
    private int numberOfTrees = 91;
    [SerializeField]
    private int percentOfTrees = 30;
    [SerializeField]
    private GameObject[,] childTrees = new GameObject[width, height];
    
   List<int> xValues = new();
   List<int> yValues = new();

    // Start is called before the first frame update
    void Start()
    {
      FindAllTrees();
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindAllTrees()
    {
      int currentTree = 0;
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
            childTrees[i,j] = this.gameObject.transform.GetChild(currentTree).gameObject;
            this.gameObject.transform.GetChild(currentTree).gameObject.SetActive(false);
            currentTree++;
            }
        }

    }

    public void SpawnTrees()
    {
        
        int numberToSpawn = numberOfTrees * (percentOfTrees/100);
        
        for(int i = 0; i <= numberToSpawn; i++)
        {
        int randY = Random.Range(0, height);
        int randX = Random.Range(0,width);

        yValues.Add(randY);
        xValues.Add(randX);

        childTrees[randX,randY].gameObject.SetActive(true);
        }

        //start checking for population density
        int numberSpawned = xValues.Count;

        for(int i = 0; i<numberSpawned; i++ ) 
        {
            int treeToCheck = Random.Range(0, xValues.Count);
            CheckOverpop(xValues[treeToCheck], yValues[treeToCheck]);
            xValues.RemoveAt(treeToCheck);
            yValues.RemoveAt(treeToCheck);
        }

    }
    private void CheckOverpop(int x, int y)
    {
        
        int nearTrees = 0;

            //UP, DOWN, RIGHT, LEFT check
         if (y+1 < height) 
         {
            if(childTrees[x,y+1].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }
        if (y-1>=0) 
         {
            if(childTrees[x,y-1].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         } 
        if (x+1 < width) 
         {
            if(childTrees[x+1,y].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }
        if (x-1>=0) 
         {
            if(childTrees[x-1,y].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }
            //DIAGONAL Check

        if(x+1 < width && y+1<height)
         {
            if(childTrees[x+1,y+1].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }
        if(x+1 < width && y-1>=0)
         {
            if(childTrees[x+1,y-1].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }
        if(x-1 >=0 && y+1<height)
         {
            if(childTrees[x-1,y+1].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }
        if(x-1>=0 && y-1>=0)
         {
            if(childTrees[x-1,y-1].gameObject.activeInHierarchy == true)
            {
            nearTrees++;
            }
         }


        if(nearTrees > 1)
        {
        childTrees[x,y].gameObject.SetActive(false);
        }
    }
}
