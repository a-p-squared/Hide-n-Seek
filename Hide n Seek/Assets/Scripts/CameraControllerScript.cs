using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    GameControllerScript gameController;

    public float rotationSpeed = 1.0f;
    public Transform target, player;
    float mouseX, mouseY;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (gameController.isPlaying)
        {
            cameraControl();
        }
    }

    void cameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
        player.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }


}
