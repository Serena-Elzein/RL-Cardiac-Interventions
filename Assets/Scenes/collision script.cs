using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionscript : MonoBehaviour
{

    public float bounceForce = 5f;
    public float bounceCooldown = 1f;

    private Rigidbody rb;
    private bool canBounce = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canBounce)
        {
            HandleKeyboardInput();
        }
    }

    private void HandleKeyboardInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canBounce && collision.gameObject.CompareTag("Cube (1)"))
        {
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collisionNormal);
            rb.velocity = bounceDirection * bounceForce;

            StartCoroutine(BounceCooldown());
        }
    }

    private System.Collections.IEnumerator BounceCooldown()
    {
        canBounce = false;
        yield return new WaitForSeconds(bounceCooldown);
        canBounce = true;
    }
}

