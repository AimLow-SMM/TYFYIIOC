using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSpot : Interactable
{
    [SerializeField] private GameObject sitPosition;
    [SerializeField] private GameObject clockToLookAt;
    [SerializeField] private Vector3 oldPosition;
    [SerializeField] private GameObject cameraUsed;
    GameStatesManager manager;

    private bool isSitting = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>().gameObject;
        manager = FindObjectOfType<GameStatesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SitDown();
    }

    public void SitDown()
    {
        if (Input.GetMouseButtonDown(0) && (player.transform.position - transform.position).magnitude <= radius && manager.currentState != PlayerStates.SAFE)
        {
            oldPosition = player.transform.position;
            player.transform.position = sitPosition.transform.position;
            player.transform.LookAt(clockToLookAt.transform.position);
            cameraUsed.transform.LookAt(clockToLookAt.transform.position);
            manager.ResetToSafe(20, 100);
            manager.currentState = PlayerStates.WORKING;
            Debug.Log("Break Taken");
            isSitting = true;
        }

        if (manager.currentState == PlayerStates.UNSAFE && isSitting == true)
        {
            ReturnPlayerAfterBreak();
        }
    }

    public void ReturnPlayerAfterBreak()
    {
        manager.ResetToSafe(20, 100);
        transform.position = oldPosition;
        Destroy(gameObject);
        Debug.Log("Player is no longer taking a break: " + manager.currentState);
    }
}
