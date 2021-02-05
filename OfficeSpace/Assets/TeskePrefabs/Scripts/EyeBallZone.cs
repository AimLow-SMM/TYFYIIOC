using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallZone : MonoBehaviour
{
    GameObject player;
    public float radarRange = 3;
    public GameObject eyeballLights;
    public float lightIntensity = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SpookyEyeballs();
    }

    void SpookyEyeballs()
    {
        Vector3 distanceToPlayer = player.transform.position - transform.position;

        if (distanceToPlayer.magnitude < radarRange)
        {

            lightIntensity = 50;

        }

        if (distanceToPlayer.magnitude >= radarRange)
        {

        }
    }
}
