using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Joel Hill
//Section: 2023SP.SGD.285.4171
//Instructor: Aurore Locklear
/////////////////////////////////////////////
///
public class HazardBehaviour : MonoBehaviour
{
    private int carSpeed = 5;
    public int logSpeed = 4;
    public bool isCar;
    HazardManager hazardManager;
    private float checkDistance = 8;

    private void Start()
    {
        hazardManager = transform.parent.GetComponent<HazardManager>();
        //checkDistance = carManager.despawnDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the hazard is a log or a car
        if(isCar)
        {
            if (gameObject.CompareTag("Left"))
            {
                transform.Translate(Vector3.up * Time.deltaTime * carSpeed);
                if (-checkDistance > transform.position.x && hazardManager != null)
                {
                    hazardManager.RemoveHazard(gameObject);
                }
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * carSpeed);
                if (checkDistance < transform.position.x && hazardManager != null)
                {
                    hazardManager.RemoveHazard(gameObject);
                }
            }
        }
        else
        {
            if (gameObject.CompareTag("Left"))
            {
                transform.Translate(Vector3.left * Time.deltaTime * logSpeed);
                if (-checkDistance > transform.position.x && hazardManager != null)
                {
                    hazardManager.RemoveHazard(gameObject);
                }
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * logSpeed);
                if (checkDistance < transform.position.x && hazardManager != null)
                {
                    hazardManager.RemoveHazard(gameObject);
                }
            }
        }
        

    }
}
