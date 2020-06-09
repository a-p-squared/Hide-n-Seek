using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerControllerScript : MonoBehaviour
{
    GameControllerScript gameController;

    public bool canTag = false;
    public float speed;

    public Animator playerAnimator;
    public GameObject target;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    void Update()
    {
        
        
    }

    public void playerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(hor, 0.0f, ver).normalized * speed * Time.deltaTime;
        transform.Translate(moveVector, Space.Self);
        playerAnimation(hor, ver);
    }

    void playerAnimation(float horizontal, float vertical)
    {
        playerAnimator.SetFloat("HorizontalSpeed", horizontal);
        playerAnimator.SetFloat("VerticalSpeed", vertical);
    }

    public void checkPlayers()
    {
        RaycastHit hit;

        if (Physics.Raycast(target.transform.position, transform.TransformDirection(Vector3.forward) * 10f, out hit))
        {
            if (hit.collider.gameObject.CompareTag("AI"))
            {
                Debug.DrawRay(target.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                hit.collider.gameObject.GetComponent<AIScript>().tagAI();

            }
            else
            {
                Debug.DrawRay(target.transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.white);
            }
        }
        else
        {
            Debug.DrawRay(target.transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.white);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("FlagPost"))
        {
            GameObject[] AIs = GameObject.FindGameObjectsWithTag("AI");
            foreach(GameObject ai in AIs)
            {
                ai.GetComponent<AIScript>().stopPlaying();
            }
        }
        gameController.updateScore();
    }

}
