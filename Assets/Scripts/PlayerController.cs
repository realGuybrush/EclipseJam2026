using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction move, attack, action;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float speed, decompileTime;

    private float decompileTimer;

    [SerializeField]
    private GameObject hammer, screwdriver;

    [SerializeField]
    private Transform barLoc;

    private void Awake()
    {
        move = playerInput.actions.FindAction("Move");
        attack = playerInput.actions.FindAction("Attack");
        action = playerInput.actions.FindAction("Interact");
        move?.Enable();
        attack?.Enable();
        action?.Enable();
        //move.performed += HandleMove;
        //move.canceled += HandleMove;
        attack.started += HandleAttack;
        action.started += HandleActStart;
        action.canceled += HandleActEnd;
    }

    void Update()
    {
        if (decompileTimer > 0)
        {
            decompileTimer -= Time.deltaTime;
            if(decompileTimer <= 0)
                screwdriver.SetActive(false);
        }
        HandleMove();
    }

    private void HandleMove()//(InputAction.CallbackContext callbackContext)
    {
        if (decompileTimer > 0 || hammer.activeSelf) return;
        Move(move.ReadValue<Vector2>());
    }

    private void Move(Vector2 dir)
    {
        rigidBody.linearVelocity = dir * speed;
        TryFlip(move.ReadValue<Vector2>());
        //animator.SetBool("Decompiling", false);
        //animator.SetBool("Walk", rigidBody.linearVelocity.magnitude > 0f);
        //armsAnimator.SetBool("Walk", rigidBody.linearVelocity.magnitude > 0f);
    }

    private void TryFlip(Vector2 dir)
    {
        if (dir.x > 0 && transform.eulerAngles.y > 0)
            transform.eulerAngles = Vector3.zero;
        else
        if (dir.x < 0 && transform.eulerAngles.y == 0)
            transform.eulerAngles = new Vector3(0, 180f, 0);
    }

    private void HandleAttack(InputAction.CallbackContext callbackContext)
    {
        Move(Vector2.zero);
        StartCoroutine("AttackTimer");
    }

    private IEnumerator AttackTimer()
    {
        hammer.SetActive(true);
        yield return new WaitForSeconds(1f);
        hammer.SetActive(false);
    }

    private void HandleActStart(InputAction.CallbackContext callbackContext)
    {
        decompileTimer = decompileTime;
        if (true) //if touching device
        {
            Move(Vector2.zero);
            screwdriver.SetActive(true);
            screwdriver.transform.position = Camera.main.WorldToScreenPoint(barLoc.position);
            //animator.SetBool("Decompiling", true);
        } else
        {
            //activate that shop/other crap
        }
    }

    private void HandleActEnd(InputAction.CallbackContext callbackContext)
    {
        if (true) //if touching device
        {
            screwdriver.SetActive(false);
            decompileTimer = 0;
            //animator.SetBool("Decompiling", false);
        }
    }

    private void OnDestroy()
    {
        attack.started -= HandleAttack;
        action.started -= HandleActStart;
        action.canceled -= HandleActEnd;
        move?.Disable();
        attack?.Disable();
        action?.Disable();
    }
}
