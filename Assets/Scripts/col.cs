using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float resetTime = 1f;

    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void FixedUpdate()
    {
        HandleKeyboardInput();
    }

    private void HandleKeyboardInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 moveDirection = rb.velocity.normalized;
            Vector3 sweepVector = moveDirection * moveSpeed * Time.deltaTime;

            RaycastHit hit;
            if (collision.collider.Raycast(new Ray(transform.position, sweepVector), out hit, sweepVector.magnitude))
            {
                Vector3 reflection = Vector3.Reflect(sweepVector, hit.normal);
                rb.velocity = reflection.normalized * moveSpeed;
            }

            StartCoroutine(ResetPosition());
        }
    }

    private System.Collections.IEnumerator ResetPosition()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(resetTime);

        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}






