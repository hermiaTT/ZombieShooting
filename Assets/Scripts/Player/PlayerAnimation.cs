using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    GameInput input;

    [SerializeField]
    private Camera _Camera;

    private Vector2 mousePosition;

    private Vector2 moveDetect;

    bool facingRight = true;

    [SerializeField]
    private GameObject playerPosition;


    //Animation support
    private Animator _animator;

    private void Awake()
    {
        input = new GameInput();
        _animator = GetComponent<Animator>();

    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {

        //Player Flip
        if (mousePosition.x > playerPosition.transform.position.x && !facingRight)
        {
            flip();
        }
        if (mousePosition.x < playerPosition.transform.position.x && facingRight)
        {
            flip();
        }


        //Animation Control
        if (moveDetect != Vector2.zero)
        {
            _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }

    }

    // Input event delegate
    private void OnEnable()
    {
        input.Enable();

        input.Player.Aim.performed += OnAim;
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnAim(InputAction.CallbackContext context)
    {
        mousePosition = _Camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveDetect = context.ReadValue<Vector2>();
    }


    //flip Method
    private void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
