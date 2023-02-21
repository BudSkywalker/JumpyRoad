using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public int speed = 5;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("Left"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}
