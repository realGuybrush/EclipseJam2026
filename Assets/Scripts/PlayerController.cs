using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    private InputAction move, attack, action;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Health health;

    [SerializeField]
    private float speed;

    private float cashOnHand;

    [SerializeField]
    private Hammer hammer;

    [SerializeField]
    private Screwdriver screwdriver;

    [SerializeField]
    private Transform barLoc;

    private List<Device> devices = new List<Device>();
    private List<BaseShop> shops = new List<BaseShop>();

    private void Awake()
    {
        move = playerInput.actions.FindAction("Move");
        attack = playerInput.actions.FindAction("Attack");
        action = playerInput.actions.FindAction("Interact");
        move?.Enable();
        attack?.Enable();
        action?.Enable();
        attack.started += HandleAttack;
        action.started += HandleActStart;
        action.canceled += HandleActEnd;
        screwdriver.OnFinish += Decompile;
        health.OnDead += Die;
    }

    void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        if (screwdriver.Working || hammer.gameObject.activeSelf)
            Move(Vector2.zero);
        else
            Move(move.ReadValue<Vector2>());
    }

    private void Move(Vector2 dir)
    {
        rigidBody.linearVelocity = dir * speed;
        TryFlip(dir);
        animator.SetBool("Walk", rigidBody.linearVelocity.magnitude > 0f);
    }

    private void TryFlip(Vector2 dir)
    {
        if (dir.x > 0 && transform.eulerAngles.y > 0)
            transform.eulerAngles = Vector3.zero;
        else if (dir.x < 0 && transform.eulerAngles.y == 0)
            transform.eulerAngles = new Vector3(0, 180f, 0);
    }

    public void UpgradeScrewdriver(Tools newTool)
    {
        screwdriver.Upgrade(newTool);
    }

    private void HandleAttack(InputAction.CallbackContext callbackContext)
    {
        if (screwdriver.gameObject.activeSelf) return;
        Move(Vector2.zero);
        animator.SetTrigger("Attack");
    }

    private void HandleActStart(InputAction.CallbackContext callbackContext)
    {
        if (hammer.gameObject.activeSelf) return;
        if (devices.Count > 0 && screwdriver.Instruments >= (int)Tools.Screwdriver)
        {
            screwdriver.StartDecompiling(devices[0].DecompileTime);
            Move(Vector2.zero);
            screwdriver.transform.position = Camera.main.WorldToScreenPoint(barLoc.position);
            animator.SetBool("Decompiling", true);
        } else
        {
            if(shops.Count > 0)
                shops[0].Spend(this);
        }
    }

    private void HandleActEnd(InputAction.CallbackContext callbackContext)
    {
        if (screwdriver.gameObject.activeSelf)
        {
            screwdriver.Stop();
            animator.SetBool("Decompiling", false);
        }
    }

    private void Decompile()
    { 
        screwdriver.gameObject.SetActive(false);
        animator.SetBool("Decompiling", false);
        if(devices.Count > 0)
            devices[0].Drop(screwdriver.Instruments);
    }

    private void Die()
    {
        animator.SetBool("Dead", true);
        enabled = false;
    }

    public bool TryToPay(float price)
    {
        if (price > cashOnHand) return false;
        cashOnHand -= price;
        return true;
    }

    public void MaxPower()
    {
        hammer.Damage *= 3;
    }

    public void PayUp(ref float debt)
    {
        if (debt >= cashOnHand)
        {
            debt -= cashOnHand;
            cashOnHand = 0;
        } else
        {
            cashOnHand -= debt;
            debt = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var device = other.GetComponent<Device>();
        if(device != null)
            devices.Add(device);
        else
        {
            var shop = other.GetComponent<BaseShop>();
            if(shop != null)
                shops.Add(shop);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var device = other.GetComponent<Device>();
        if(device != null)
        {
            if(devices.Contains(device))
                devices.Remove(device);
        }
        else
        {
            var shop = other.GetComponent<BaseShop>();
            if(shop != null)
                if(shops.Contains(shop))
                    shops.Remove(shop);
        }
    }

    public void GetMoney(float profit)
    {
        cashOnHand += profit;
    }

    private void OnDestroy()
    {
        health.OnDead -= Die;
        screwdriver.OnFinish -= Decompile;
        attack.started -= HandleAttack;
        action.started -= HandleActStart;
        action.canceled -= HandleActEnd;
        move?.Disable();
        attack?.Disable();
        action?.Disable();
    }
}
