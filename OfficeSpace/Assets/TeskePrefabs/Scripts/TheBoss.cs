using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TheBoss : MonoBehaviour
{
    

    public Light eyeBall;
    private int currentWaypoint = 0;
    private NavMeshAgent agent;

    [SerializeField] Transform[] waypoints;
    GameObject player;

    Vector3 distanceToWaypoint;
    Vector3 distanceToPlayer;

    public float waypointTolerance = 0.5f;
    public float radarRange = 10f;

    Color red = Color.red;
    Color blue = Color.blue;

    [SerializeField] private AudioSource personalSound;
    [SerializeField] private AudioSource workSound;

    private bool platitudesPlaying = false;

    GameStatesManager gameController;

    public string nextSceneName;
    public float loseRadius = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        ShuffleWaypoint();
        gameController = FindObjectOfType<GameStatesManager>();

        personalSound.volume = 0.5f;
        workSound.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Chasing();

    }

    void Patroling()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);
        eyeBall.color = blue;
        //find distance between enemy and waypoint
        //if waypoint is close enough, choose another random one and move toward it
        distanceToWaypoint = transform.position - waypoints[currentWaypoint].position;
            

        if (distanceToWaypoint.magnitude < waypointTolerance)
        {
            int lastWaypoint = currentWaypoint;

            while (currentWaypoint == lastWaypoint)
            {
                ShuffleWaypoint();
                Debug.Log("Shuffling...");
            }
        }
    }



    void Chasing()
    {
        if(gameController.currentState == PlayerStates.WORKING || gameController.currentState == PlayerStates.SAFE)
        {
            Patroling();
            eyeBall.color = blue;

            if (!platitudesPlaying)
            {
                personalSound.Play();
                workSound.Stop();

                platitudesPlaying = true;
            }

        }

        if(gameController.currentState == PlayerStates.UNSAFE)
        {
            distanceToPlayer = player.transform.position - transform.position;
            if(distanceToPlayer.magnitude < radarRange)
            {
                agent.SetDestination(player.transform.position);
                eyeBall.color = red;

                if (platitudesPlaying)
                {
                    personalSound.Stop();
                    workSound.Play();

                    platitudesPlaying = false;
                }

                if(distanceToPlayer.magnitude < loseRadius)
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            }

            if(distanceToPlayer.magnitude >= radarRange)
            {
                Patroling();
                eyeBall.color = blue;

                if (!platitudesPlaying)
                {
                    personalSound.Play();
                    workSound.Stop();

                    platitudesPlaying = true;
                }
            }
        }

        if(gameController.currentState == PlayerStates.DANGER)
        {
            agent.SetDestination(player.transform.position);
            eyeBall.color = red;

            if (platitudesPlaying)
            {
                personalSound.Stop();
                workSound.Play();

                platitudesPlaying = false;
            }

            distanceToPlayer = player.transform.position - transform.position;
            if (distanceToPlayer.magnitude < loseRadius)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        
    }

    void ShuffleWaypoint()
    {
        int lastWaypoint = currentWaypoint;
        currentWaypoint = Random.Range(1, waypoints.Length);
    }

}
