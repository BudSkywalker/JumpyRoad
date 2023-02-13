//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Logan Hickman
//Section: 2023SP.SGD.285.4171
//Instructor: Aurore Locklear
//Date: 2/13/2023
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private float cameraSpeed = 0.5f;

    private int _currentScore, _highestScore;
    public int CurrentScore 
    { 
        get
        {
            return _currentScore;
        }
        private set
        {
            _currentScore = value;
            if(_currentScore > _highestScore)
            {
                _highestScore = _currentScore;
                //scoreDisplay.text = "Score: " + _highestScore;
                cam.transform.position = new Vector3(0, 20, -5);
            }
        }
    }
    public int HighestScore { get { return _highestScore; } }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            EnvironmentManager.Instance.MoveTerrain();
            CurrentScore++;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            EnvironmentManager.Instance.MoveTerrain(true);
            CurrentScore--;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(transform.position.x > -6) transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x < 6) transform.position += Vector3.right;
        }

        cam.transform.position += cameraSpeed * Time.deltaTime * Vector3.forward;
    }
}