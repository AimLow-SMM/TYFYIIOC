using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicleInteraction : Interactable
{
    FPSController currentPlayerState;
    [SerializeField] private GameObject cameraPoint;
    Vector3 minigameCameraPosition;
    public bool gameComplete = false;
    public bool playerReturned = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentPlayerState = player.GetComponent<FPSController>();
        minigameCameraPosition = cameraPoint.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("The player variable is:" + player.tag);
    }
    /*
    public override void Interact()
    {
        //currentPlayerState.PlayerIsWorking();
        currentPlayerState.SetCameraForMinigame(minigameCameraPosition);
        SpawnMinigame();
        //Spawn mini game in prefab's canvas
    }

    public virtual void SpawnMinigame()
    {
        //Meant to be overriden, here to spawn the game in the specific cubicle's prefab
    }

    public virtual void ClearConditionMet()
    {
        //To be called when the minigame has reached completion
        gameComplete = true;
        Debug.Log("Clear condition met");
        GoBackToWork();
    }

    public void GoBackToWork()
    {
        //Give new target time, set player to safe, make them able to move again
        if(gameComplete)
        {
            currentPlayerState.ReturnPlayerAfterMinigame();
            playerReturned = true;
            Debug.Log("Player has been returned");
        }
    }
    */
}
