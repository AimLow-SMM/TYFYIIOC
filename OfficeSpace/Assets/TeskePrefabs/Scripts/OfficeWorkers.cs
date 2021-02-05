using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeWorkers : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject neck;
    public float radarRange;
    public Light eyeBall_Prefab1;
    public Light eyeBall_Prefab2;
    public GameObject computerScreen;
    public AudioSource clacking;
    public AudioClip keyboardSound;
    GameStatesManager gameController;
    PlayerStates isPlayerSafe;
    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        clacking.clip = keyboardSound;
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = FindObjectOfType<GameStatesManager>();
        neck.transform.LookAt(computerScreen.transform);
        clacking.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        SpookyLook();
    }

    //if player is within range, turn and spook player
    void SpookyLook()
    {
        Vector3 distanceToPlayer = player.transform.position - transform.position;

        if(distanceToPlayer.magnitude < radarRange && gameController.currentState == PlayerStates.UNSAFE)
        {
            neck.transform.LookAt(player.transform);
        }

        if(distanceToPlayer.magnitude < radarRange && gameController.currentState == PlayerStates.DANGER)
        {

            neck.transform.LookAt(player.transform);
            eyeBall_Prefab1.intensity = 50;
            eyeBall_Prefab2.intensity = 50;
            if(clacking.isPlaying == false)
            {
                clacking.Play();
            }

        }

        if(distanceToPlayer.magnitude >= radarRange)
        {

            neck.transform.LookAt(computerScreen.transform);
            eyeBall_Prefab1.intensity = 0;
            eyeBall_Prefab2.intensity = 0;
            clacking.Stop();

        }
    }
}
