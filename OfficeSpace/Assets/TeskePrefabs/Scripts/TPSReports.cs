using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSReports : Interactable
{

    public GameObject holdPosition;
    public GameObject turnInPoint;
    public bool isPickedUp = false;
    GameStatesManager manager;
    public GameObject[] validPoints;

    void Start()
    {
        manager = FindObjectOfType<GameStatesManager>();
        validPoints = GameObject.FindGameObjectsWithTag("NeedsReport");
    }

    void Update()
    {
        Interact();
        FindClosestReportBoi();
    }

    public override void Interact()
    {
        if (Input.GetMouseButtonDown(0) && (holdPosition.transform.position - transform.position).magnitude <= radius && !manager.isHoldingThing)
        {
            transform.position = holdPosition.transform.position;
            transform.SetParent(player.transform);
            manager.isHoldingThing = true;
            manager.ResetToSafe(15, 120);
            Debug.Log("Report picked up.");
        }

        if (Input.GetMouseButtonDown(0) && (turnInPoint.transform.position - transform.position).magnitude <= radius && manager.isHoldingThing && turnInPoint.tag == "NeedsReport")
        {
            manager.ResetToSafe(35, 120);
            manager.isHoldingThing = false;
            Destroy(gameObject);
            turnInPoint.GetComponent<CoffeeCubicle>().SwitchMen();
            Debug.Log("Report is turned in.");
        }
    }

    public void FindClosestReportBoi()
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

