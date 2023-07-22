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
                _controller.height = Mathf.Lerp(_controller.height, 1, p);
            }
            else
            {
                _controller.height = Mathf.Lerp(_controller.height, 2, p);
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
        _moveDir.z = input.y;
        _controller.Move(transform.TransformDirection(_moveDir * moveSpeed * Time.deltaTime));
        _velocity.y += gravity * Time.deltaTime;
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
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
