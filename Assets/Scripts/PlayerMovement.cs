using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling the player movement
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.25f;
    [SerializeField] float rayLength = 1.4f;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private bool isMoving;
    private GameManager gameManager;

    void Start() 
    {
        gameManager = GameManager.instance;    
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        HandleInput(); 
    }

    // Moves the player to the target location if the player is currently not moving
    void Move()
    {
        if (isMoving)
        {
            // If the player is one square away from where they started they stop moving
            if (Vector3.Distance(startPosition, transform.position) > 1f )
            {
                transform.position = targetPosition;
                isMoving = false;
                return;
            }
            // Move player to target location
            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;
        }
    }
    
    // Takes in input from the player and assigns a new target location
    void HandleInput()
    {
        // If the player is trapped by obstacles the game is over
        if (!CanMove()) 
        { 
            gameManager.GameOver();
        }
        // If the player is currently moving then they will not be able to input any movement
        else if (isMoving)
        {
            return;
        }

        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, rayLength) && hit.collider.gameObject.tag == "Obstacle")
            {
                GameManager.instance.RemoveScore();
                return;
            }
            else
            {
                targetPosition = transform.position + Vector3.forward;
                startPosition = transform.position;
                isMoving = true;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (Physics.Raycast(transform.position, Vector3.back, out hit, rayLength) && hit.collider.gameObject.tag == "Obstacle")
            {
                GameManager.instance.RemoveScore();
                return;
            }
            else
            {
                targetPosition = transform.position + Vector3.back;
                startPosition = transform.position;
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (Physics.Raycast(transform.position, Vector3.left, out hit, rayLength) && hit.collider.gameObject.tag == "Obstacle")
            {
                GameManager.instance.RemoveScore();
                return;
            }
            else
            {
                targetPosition = transform.position + Vector3.left;
                startPosition = transform.position;
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Physics.Raycast(transform.position, Vector3.right, out hit, rayLength) && hit.collider.gameObject.tag == "Obstacle")
            {
                GameManager.instance.RemoveScore();
                return;
            }
            else
            {
                targetPosition = transform.position + Vector3.right;
                startPosition = transform.position;
                isMoving = true;
            }
        }
    }

    // Checks if the player is able to move in any direction
    bool CanMove() 
    {
        if (Physics.Raycast(transform.position, Vector3.forward, rayLength) &&
            Physics.Raycast(transform.position, Vector3.back, rayLength) && 
            Physics.Raycast(transform.position, Vector3.left, rayLength) &&
            Physics.Raycast(transform.position, Vector3.right, rayLength)) 
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
}
