using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10;
    public float gravity = -9.8f;
    Vector3 velocity;
    public float jumpHeight = 3f;


    public Transform groundCheck; // add an gameObject under player called groundCheck and drag it here in inspector
    public float checkRadius = 0.3f;
    public LayerMask groundLayer;  // make sure all the ground object in the level has a layer of ground, and choose layer ground here in inspector
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // a sphere with "checkRadius" centered at groundCheck.position
        // if this sphere collide with any game object that is the same layer with groundLayer, return true
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  //otherwise, even if the player on the ground, its velocity in y direction will keep increasing. 
            // we could set it to 0, but because the ground check is a sphere and this sphere touch the ground a little earlier than the player's body
            // set it to -2 is more proper, to force the player's body totally fall on the ground
            // otherwise the player will be in the air a little when the sphere touches the ground but not yet the player
        }


        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // In this situation, the character moves in world space
        // it will always move to one direction when press one of WASD no matter where it is facing
        //Vector3 move = new Vector3(horizontal, 0, vertical);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * speed * Time.deltaTime);


        // v = gt
        velocity.y += gravity * Time.deltaTime;
        // deltaY = (1/2)gt^2 = (1/2)vt, so we time one more Time.deltaTime
        //controller.Move(0.5f * velocity * Time.deltaTime); // feels slow when falling down
        controller.Move(velocity * Time.deltaTime);

        // v = sqrt(-2hg), with a velecity of v, we can jump up to h height
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }

    }
}
