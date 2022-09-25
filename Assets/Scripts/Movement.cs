using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Range(1,3)] [SerializeField] private int numTiles = 1;
    [SerializeField] private float moveDuration = 0.25f;
    
    private Vector2 moveInput;
    private Vector3 ealryInputDirection;
    private bool isMoving = false;
    private bool isEarlyMove = false;

    void Update()
    {
        CheckIsStuck();
    }

    // Parses the input from the player
    private void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();

        // Gets the corresponding direction from the input
        Vector3 inputDirection = new Vector3(moveInput.x * numTiles, 0, moveInput.y * numTiles);
        
        // If the player is currently moving they cannot move but can set an early input for the next move 
        if (isMoving)
        {
            isEarlyMove = true;
            ealryInputDirection = inputDirection;
            return;
        }
        else
        {
            MovePlayer(inputDirection);
        }  
    }

    // Moves the player in the designated direction
    private void MovePlayer(Vector3 direction)
    {
        if (!CanMove(direction))
        {
            return;
        }
        else
        {
            isMoving = true;
            Vector3 newPosition = transform.position + direction;

            float moveDirectionX = transform.position.x + direction.x;
            float moveDirectionZ = transform.position.z + direction.z;

            // Leens the player to the input direction
            // On completing the move a check is made to move the player early
            Rotate(direction);
            transform.LeanMoveLocalX(moveDirectionX, moveDuration).setEaseInOutSine();
            transform.LeanMoveLocalZ(moveDirectionZ, moveDuration).setEaseInOutSine().setOnComplete(CheckEarlyInput);
        }
    }

    // Checks if the player has inputed a new direction before current movement is finished
    private void CheckEarlyInput()
    {
        isMoving = false;
        if (isEarlyMove == true)
        {
            isEarlyMove = false;
            MovePlayer(ealryInputDirection);
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction;
        Vector3 above = new Vector3(0, 20, 0);

        Debug.DrawRay(newPosition + above, Vector3.down * 20, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(newPosition + above, Vector3.down, out hit, 20) && hit.collider.gameObject.tag == "Obstacle")
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    private bool CheckIsStuck()
    {
        if (!CanMove(Vector3.forward) && !CanMove(Vector3.back) && !CanMove(Vector3.left) && !CanMove(Vector3.right))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private void Rotate(Vector3 direction)
    {
        Vector3 newRotation = new Vector3(direction.z, direction.y, -direction.x);
        transform.LeanRotateAround(newRotation, 90, moveDuration).setEaseInOutSine();
        transform.LeanMoveLocalY(transform.position.y + SquareCircleDifference() / 2, moveDuration / 2).setEaseInOutSine().setOnComplete(MoveDown);
    }

    private float SquareCircleDifference()
    {
        float y = Mathf.Sqrt(2 * (transform.localScale.y * transform.localScale.y)) - transform.localScale.y;
        return y;
    }

    private void MoveDown()
    {
        transform.LeanMoveLocalY(transform.position.y - SquareCircleDifference() / 2, moveDuration / 2).setEaseInOutSine();
    }
}
