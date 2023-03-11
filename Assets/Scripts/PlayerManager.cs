using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    Vector2 movement;

    public Animator animator;
    
    public GameManager gameManager;
    
    void Start()
    {
        // Getting all the player's needed components
        rb = GetComponent<Rigidbody2D>();

        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            movement.y = 0;

            if (movement.x > 0)
            {
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", true);
            }
            else
            {
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isWalkingLeft", true);
            }
        }
        else
        {
            movement.x = 0;
        }

        animator.SetFloat("Speed", Mathf.Abs(movement.x));
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Cartel1" || col.gameObject.name == "Cartel2" || col.gameObject.name == "Cartel3")
        {
            gameManager.ActivateCartelPanel(col.gameObject.name);
        } 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.name == "Witch" && gameManager.GetWitchState() == true)
        {
            gameManager.DisplayFoundWitchMessage();
        }
    }
}