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
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private LayerMask obstacles;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private float cameraSpeed = 0.5f;

    private int offsetSpeed = 0;
    private string direction = "";

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
                transform.position = new Vector3(transform.position.x, 0.5f, 0.5f);
                EnvironmentManager.Instance.MoveTerrain();
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
            if (Physics.OverlapBox(transform.position + Vector3.forward, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0)
            {
                transform.position += Vector3.forward;
                CurrentScore++;
            }
            CheckForLog();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Physics.OverlapBox(transform.position + Vector3.back, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0)
            {
                transform.position += Vector3.back;
                CurrentScore--;
            }
            CheckForLog();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(Physics.OverlapBox(transform.position + Vector3.left, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0 && transform.position.x > -6) transform.position += Vector3.left;
            CheckForLog();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Physics.OverlapBox(transform.position + Vector3.right, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0 && transform.position.x < 6) transform.position += Vector3.right;
            CheckForLog();
        }

        cam.transform.position += cameraSpeed * Time.deltaTime * Vector3.forward;
        CheckForLog();
        OffsetPlayer();

        //Test Game Over States
        Collider[] underMeColliders = Physics.OverlapBox(transform.position + Vector3.down, Vector3.one);
        if (cam.transform.position.z + 2.15f >= transform.position.z || (
            //Water
            underMeColliders.Any(x => x.CompareTag("Water")) && !underMeColliders.Any(x => x.CompareTag("Left") || x.CompareTag("Right"))
            ))
        {
            Debug.Log("Game Over!");
            //GameManager.Instance.CurrentState = GameState.GameOver;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        HazardBehaviour hb = other.GetComponent<HazardBehaviour>();
        if (hb != null && hb.isCar)
        {
            Debug.Log("Game Over!");
            //GameManager.Instance.CurrentState = GameState.GameOver;
        }
    }

    private void CheckForLog()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 4, obstacles))
        {
            if(hit.transform.CompareTag("Right"))
            {
                direction = "Right";
                offsetSpeed = hit.transform.GetComponent<HazardBehaviour>().logSpeed;
            }
            else if(hit.transform.CompareTag("Left"))
            {
                direction = "Left";
                offsetSpeed = hit.transform.GetComponent<HazardBehaviour>().logSpeed;
            }
        }
        else
        {
            direction = "";
            offsetSpeed = 0;
        }
    }

    private void OffsetPlayer()
    {
        if (direction == "Right")
        {
            transform.Translate(Vector3.right * Time.deltaTime * offsetSpeed);
        }
        else if (direction == "Left")
        {
            transform.Translate(Vector3.left * Time.deltaTime * offsetSpeed);
        }
        else
        {
            
        }

    }
}
