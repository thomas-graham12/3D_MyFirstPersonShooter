using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _controller;
    Vector3 _velocity;
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float gravity;
    [SerializeField] float crouchTimer;
    [ReadOnly] float origMoveSpeed;
    bool lerpCrouch;
    bool crouching;
    public bool sprinting;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        origMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = _controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                _controller.height = Mathf.Lerp(_controller.height, 1, p); // Changes the controller's height to 1 if crouching.
            }
            else
            {
                _controller.height = Mathf.Lerp(_controller.height, 2, p); // Changes the controller's height to 2 if not crouching.
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }

        if (sprinting)
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = origMoveSpeed;
        }
    }

    public void ProcessMove(Vector2 input) // receives input from InputManager.cs and applies them to the character controller. 
    {
        Vector3 _moveDir = Vector3.zero;
        _moveDir.x = input.x;
        _moveDir.z = input.y; // moveDir.z is input.y because input is a Vec2
        _controller.Move(transform.TransformDirection(_moveDir * moveSpeed * Time.deltaTime)); // Moves the character towards the moveDir
        _velocity.y += gravity * Time.deltaTime; 
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Locks gravity at a max of -2
        }
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void SprintPressed()
    {
        sprinting = true;
    }

    public void SprintReleased()
    {
        sprinting = false;
    }
}
