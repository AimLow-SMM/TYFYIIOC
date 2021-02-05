using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : Interactable
{
    public GameObject holdPosition;
    public GameObject turnInPoint;
    public bool isPickedUp = false;
    GameStatesManager manager;
    public GameObject[] validPoints;
    public string nextSceneName;

    void Start()
    {
        manager = FindObjectOfType<GameStatesManager>();
        validPoints = GameObject.FindGameObjectsWithTag("NeedsKey");
    }

    void Update()
    {
        Interact();
        FindClosestDoor();
    }

    public override void Interact()
    {
        if (Input.GetMouseButtonDown(0) && (holdPosition.transform.position - transform.position).magnitude <= radius && !manager.isHoldingThing)
        {
            transform.position = holdPosition.transform.position;
            transform.SetParent(player.transform);
            manager.isHoldingThing = true;
            Debug.Log("Key picked up.");
        }

        if (Input.GetMouseButtonDown(0) && (turnInPoint.transform.position - transform.position).magnitude <= radius && manager.isHoldingThing && turnInPoint.tag == "NeedsKey")
        {
            SceneManager.LoadScene(nextSceneName);
            manager.isHoldingThing = false;
            Destroy(gameObject);
            turnInPoint.GetComponent<CoffeeCubicle>().SwitchMen();
            Debug.Log("Key is turned in.");
        }
    }

    public void FindClosestDoor()
    {
        float distance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject boi in validPoints)
        {
            Vector3 diff = boi.transform.position - currentPosition;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                turnInPoint = boi;
                distance = curDistance;
            }
        }
    }
}
