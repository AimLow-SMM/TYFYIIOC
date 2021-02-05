using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    public PlayerStates currentState;
    public float movementDirection;
    public float playerSpeed = 2.25f;
    public float cameraSpeedX = 2f;
    public float cameraSpeedY = 2f;
    public float minPitch = -30f;
    public float maxPitch = 60f;
    private float cameraY;
    private float cameraX;
    private Camera cam;
    private BoxCollider playerCollider;
    Vector3 oldPosition;
    private Rigidbody rb;


    [SerializeField] private LayerMask clickablesLayer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main;
        playerCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotation();

        if (currentState != PlayerStates.WORKING)
        {
            Movement();
        }

    }

    void Movement()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized * playerSpeed * Time.deltaTime * -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized * playerSpeed * Time.deltaTime * -1;
        }
        else
        {
            transform.position += new Vector3(0, 0, 0) * playerSpeed * Time.deltaTime;
        }
        



        //Check for interactable objects
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, clickablesLayer))
            {
                Interactable currentInteraction = rayHit.collider.GetComponent<Interactable>();
                if (currentInteraction != null)
                {
                    currentInteraction.Interact();
 
                }
            }
        }
    }

    void Rotation()
    {
        cameraX += cameraSpeedX * Input.GetAxis("Mouse X");
        cameraY -= cameraSpeedY * Input.GetAxis("Mouse Y");
        cameraY = Mathf.Clamp(cameraY, minPitch, maxPitch);
        transform.eulerAngles = new Vector3(0, cameraX, 0.0f);
        cam.transform.eulerAngles = new Vector3(cameraY, cameraX, 0);
    }


/*    public void PlayerIsUnsafe()
    {
        currentState = PlayerStates.UNSAFE;
        Debug.Log("Player is UNSAFE");
    }

    public void PlayerIsSafe()
    {
        currentState = PlayerStates.SAFE;
        Debug.Log("Player is SAFE");
        
    }

    public void PlayerIsWorking()
    {
        currentState = PlayerStates.WORKING;
        Debug.Log("Player is WORKING");
    }
*/
/*
    public void SetCameraForMinigame(Vector3 newPosition)
    {
        oldPosition = transform.position;
        playerCollider.enabled = !playerCollider.enabled;
        transform.position = newPosition;
    }

    public void ReturnPlayerAfterMinigame()
    {
        transform.position = oldPosition;
        //playerCollider.enabled = playerCollider.enabled;
        FindObjectOfType<GameStatesManager>().ResetToSafe();
        
    }
*/
}
