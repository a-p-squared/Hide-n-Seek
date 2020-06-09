using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AIScript : MonoBehaviour
{
    GameControllerScript gameController;
    public Animator animator;
    Animator AiFSM;

    public Material untaggedMat, taggedMat;
    public GameObject TagSphere;
    bool isTagged = false;
    bool isPlaying = true;

    GameObject player;
    GameObject[] hidingSpots;
    GameObject currentHidingSpot;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        hidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
        AiFSM = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject getPlayer()
    {
        return player;
    }

    public Animator getAnimator()
    {
        return animator;
    }

    public void hide(bool nearest = true)
    {
        if (nearest == true)
        {
            currentHidingSpot = hidingSpots[0];
            for (int i = 1; i < hidingSpots.Length; i++)
            {
                if (Vector3.Distance(transform.position, hidingSpots[i].transform.position) < Vector3.Distance(transform.position, currentHidingSpot.transform.position))
                {
                    currentHidingSpot = hidingSpots[i];
                }
            }
        }
        else
        {
            currentHidingSpot = hidingSpots[Random.Range(0, hidingSpots.Length)];
        }

        AiFSM.SetTrigger("Hide");
    }

    public GameObject getHidingSpot()
    {
        return currentHidingSpot;
    }

    public GameObject getSpawnPoint()
    {
        return GameObject.FindGameObjectWithTag("SpawnPoint");
    }

    public void tagAI()
    {
        if (!isTagged && player.GetComponent<PlayerControllerScript>().canTag)
        {
            MeshRenderer mesh;
            mesh = TagSphere.GetComponent<MeshRenderer>();
            mesh.material = taggedMat;
            isTagged = true;
            AiFSM.SetTrigger("Tagged");
            gameController.updateTagged();
        }
    }

    public void stopPlaying()
    {
        if(isTagged)
        {
            Destroy(TagSphere);
            isPlaying = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {;
        if(other.CompareTag("FlagPost") && player.GetComponent<PlayerControllerScript>().canTag && isPlaying)
        {
            gameController.gameOver();
        }
    }
}
