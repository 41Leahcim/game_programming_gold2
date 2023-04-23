using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour{
    public GameManager manager;
    [SerializeField] private float movementRange = 5;
    [field:SerializeField] public int value{get;private set;} = 1;

    private Vector3 lastPosition;
    private Rigidbody2D boxRigidbody;
    private bool dropped = false;
    private bool landed = false;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start(){
        // Retrieve the rigidbody of the box
        boxRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // if the box has landed and just stopped, stop movement and tell the game manager this box landed
        if(landed && !stopped && Stopped()){
            stopped = true;
            manager.BoxLanded();
            boxRigidbody.bodyType = RigidbodyType2D.Static;
        }
        lastPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision){
        // If the box hits an object, it landed
        landed = true;

        // If it landed on the ground, the game is over
        if(collision.gameObject.CompareTag("Ground")){
            manager.GameOver();
        }
    }

    public void Drop(){
        // Drop the box
        dropped = true;
        boxRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Move(float movement) {
        // The box should only move on user input, if the box hasn't been dropped yet
        if (!dropped) {
            // Calculate the movement
            Vector3 worldMovement = Vector3.right * movement;
            worldMovement = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * worldMovement;

            // Calculate and constrain the new position
            Vector3 newPosition = transform.position + worldMovement;
            if(newPosition.x < -movementRange){
                newPosition.x = -movementRange;
            }else if(newPosition.x > movementRange){
                newPosition.x = movementRange;
            }

            // Move the object
            transform.position = newPosition;
        }
    }

    public void Rotate(float rotation){
        // The box should only rotate on user input, if the box hasn't been dropped yet
        if(!dropped){
            transform.Rotate(rotation * Vector3.forward);
        }
    }

    bool Stopped(){
        // The box stopped, if it hasn't moved since the last frame
        return Vector3.Distance(lastPosition, transform.position) == 0.0f;
    }
}
