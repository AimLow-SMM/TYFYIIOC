using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 3f;
    public GameObject player;
    bool hasInteracted = false;
    GameStatesManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameStatesManager>();    
    }
    void Update()
    {
        if (!hasInteracted)
        {
            //if the player hasn't interacted with the object, mark the object as interacted with and put it in an inactive state
        }
    }

    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
        hasInteracted = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }



}
