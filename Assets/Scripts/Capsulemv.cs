using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsulemv : MonoBehaviour
{

    public float movementSpeed = 10f; // Speed at which the object moves
    public float rotationSpeed = 50f;

    void Update()
    {
        float forwardInput = 0f;
        float upInput = 0f;

        // Check arrow keys for forward movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            forwardInput = -1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            forwardInput = 1f;
        }

        // Check arrow keys for upward movement
        if (Input.GetKey(KeyCode.W)) //Move forwards
        {
            upInput = -1f;
        }
        else if (Input.GetKey(KeyCode.S)) //Move Backwards
        {
            upInput = 1f;
        }
        if (Input.GetKey(KeyCode.D)) // Rotate right 
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.A)) // Rotate left
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        // Calculate movement direction
        Vector3 movementDirection = (transform.forward * forwardInput) + (transform.up * upInput);
        movementDirection.Normalize();

        // Move the guidewire
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}



