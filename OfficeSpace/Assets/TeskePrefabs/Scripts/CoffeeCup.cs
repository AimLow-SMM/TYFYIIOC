using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : Interactable
{

    public GameObject holdPosition;
    public GameObject turnInPoint;
    public bool isPickedUp = false;
    GameStatesManager manager;
    public GameObject[] validPoints;

    void Start()
    {
        manager = FindObjectOfType<GameStatesManager>();
        validPoints = GameObject.FindGameObjectsWithTag("NeedsCoffee");
    }

    void Update()
    {
        Interact();
        FindClosestCoffeeBoi();
    }

    public override void Interact()
    {
        if (Input.GetMouseButtonDown(0) && (holdPosition.transform.position - transform.position).magnitude <= radius && !manager.isHoldingThing)
        {
            transform.position = holdPosition.transform.position;
            transform.SetParent(player.transform);
            manager.isHoldingThing = true;
            manager.ResetToSafe(15, 130);
            Debug.Log("Coffee Cup picked up.");
        }

        if (Input.GetMouseButtonDown(0) && (turnInPoint.transform.position - transform.position).magnitude <= radius && manager.isHoldingThing && turnInPoint.tag == "NeedsCoffee")
        {
            manager.ResetToSafe(35, 130);
            manager.isHoldingThing = false;
            Destroy(gameObject);
            turnInPoint.GetComponent<CoffeeCubicle>().SwitchMen();
            Debug.Log("Coffee is turned in.");
        }
    }

    public void FindClosestCoffeeBoi()
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
