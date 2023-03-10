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

    private GameObject lastLog;
    private bool reachedLogs = false;
    [HideInInspector] public bool movedSinceLog = false;

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
            if (_currentScore > _highestScore)
            {
                _highestScore = _currentScore;
                scoreDisplay.text = "Score: " + _highestScore;
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
                MoveChecker();
                CurrentScore++;
            }
            CheckForLog();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Physics.OverlapBox(transform.position + Vector3.back, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0)
            {
                transform.position += Vector3.back;
                MoveChecker();
                CurrentScore--;
            }
            CheckForLog();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Physics.OverlapBox(transform.position + Vector3.left, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0 && transform.position.x > -6) transform.position += Vector3.left;
            MoveChecker();
            CheckForLog();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Physics.OverlapBox(transform.position + Vector3.right, Vector3.one / 2, Quaternion.identity, obstacles).Length == 0 && transform.position.x < 6) transform.position += Vector3.right;
            MoveChecker();
            CheckForLog();
        }

        cam.transform.position += cameraSpeed * Time.deltaTime * Vector3.forward;
        CheckForLog();

        //Test Game Over States
        Collider[] underMeColliders = Physics.OverlapBox(transform.position + Vector3.down, new Vector3(0.65f, 1f, 0.5f));
        foreach(Collider c in underMeColliders)
        {
            Debug.Log(c);
        }
        if (cam.transform.position.z + 2.15f >= transform.position.z || (
            //Water
            underMeColliders.Any(x => x.CompareTag("Water")) && !underMeColliders.Any(x => x.CompareTag("Left") || x.CompareTag("Right"))
            ))
        {
            Debug.Log("Game Over!");
            GameManager.Instance.CurrentState = GameState.GameOver;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        HazardBehaviour hb = other.GetComponent<HazardBehaviour>();
        if (hb != null && hb.isCar)
        {
            Debug.Log("Game Over!");
            GameManager.Instance.CurrentState = GameState.GameOver;
        }
    }

    private void CheckForLog()
    {
        if (reachedLogs == true)
        {
            lastLog.GetComponent<HazardBehaviour>().chickenIsOn = false;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 4, obstacles))
        {
            if (hit.transform.CompareTag("Right"))
            {
                hit.transform.gameObject.GetComponent<HazardBehaviour>().chickenIsOn = true;
                lastLog = hit.transform.gameObject;
                reachedLogs = true;
            }
            else if (hit.transform.CompareTag("Left"))
            {
                hit.transform.gameObject.GetComponent<HazardBehaviour>().chickenIsOn = true;
                lastLog = hit.transform.gameObject;
                reachedLogs = true;
            }
        }

    }

    private void MoveChecker()
    {
        if (reachedLogs == true)
        {
            movedSinceLog = true;
        }
    }
}
