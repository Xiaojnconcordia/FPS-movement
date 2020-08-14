using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;
    float xRotation = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // independent from the framerate (Time.deltaTime)
        float MouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //rotate the player if move mouse left or right (the camera will rotate too as a part of player)
        player.Rotate(Vector3.up * MouseX);

        // rotate camera only if move mouse up and down
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limit the rotation angle
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
