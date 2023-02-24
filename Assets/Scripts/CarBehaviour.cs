using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public int speed = 5;
    CarManager carManager;
    private float checkDistance = 8;

    private void Start()
    {
        carManager = transform.parent.GetComponent<CarManager>();
        //checkDistance = carManager.despawnDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("Left"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (-checkDistance > transform.position.x && carManager != null)
            {
                carManager.RemoveCar(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            if (checkDistance < transform.position.x && carManager != null)
            {
                carManager.RemoveCar(gameObject);
            }
        }

        
    }
}
