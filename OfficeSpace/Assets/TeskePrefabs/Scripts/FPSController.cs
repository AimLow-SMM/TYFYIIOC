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
    private Vector3 inputVector;
    private Vector3 moveVector;
    private Camera cam;
    // private BoxCollider playerCollider;
    // Vector3 oldPosition;
    // private Rigidbody rb;
    public CharacterController controller;


    [SerializeField] private LayerMask clickablesLayer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main;
        // playerCollider = GetComponent<BoxCollider>();
        // rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Should Rotation() be inside of the .working if statement?
        Rotation(); // FPS camera and body rotation

        if (currentState != PlayerStates.WORKING)
        {
            Movement(); // Classic WASD movement
            Interactable(); // Check for interactable objects
        }
    }
    void Interactable()
    {
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
    void Movement()
    {
        inputVector = new Vector3(0f, 0f, 0f);

        // Input.GetAxis() Takes a bit of setup but works with WASD, arrow keys, joystick, controller
        // float x = Input.GetAxis("Horizontal"); 
        // float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.W))
        {
            inputVector += new Vector3(0f, 0f, 1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector += new Vector3(0f, 0f, -1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector += new Vector3(1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector += new Vector3(-1f, 0f, 0f);
        }
        inputVector = inputVector.normalized;

        moveVector = transform.right * inputVector.x + transform.forward * inputVector.z;

        controller.Move(moveVector * playerSpeed * Time.deltaTime);

        // To stop player from climbing on objects
        float badHardCodedfloorHeight = 1.39f;
        if (transform.position.y > badHardCodedfloorHeight)
        {
            transform.position = new Vector3(transform.position.x, badHardCodedfloorHeight, transform.position.z);
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
