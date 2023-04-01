using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput controls;
    bool walkCoroutineRunning = false;
    Vector2 walkInput;
    Vector2 deltaWalk;
    Vector3 tempPositionWalk;
    [SerializeField] float walkSpeed;
    bool inventoryOpen = false;
    public GameObject inventoryPanel;
    public PauseUI pauseUI;
    


    private void Awake()
    {
        controls = new PlayerInput();
        controls.Overworld.Movement.performed += ctx => Walk();
        controls.Overworld.Inventory.performed += ctx => ToggleInventoryUI();
        controls.Overworld.Pause.performed += ctx => pauseUI.TogglePause();
    }

    private void Walk()
    {
        if (walkCoroutineRunning == false)
            StartCoroutine(OnInputWalk());
    }

    private IEnumerator OnInputWalk()
    {
        /*
        if (GameManager.gameEnded)
        {
            this.enabled = false;
            yield break;
        } */
        walkCoroutineRunning = true;
        while (controls.Overworld.Movement.IsPressed())
        {
            walkInput = controls.Overworld.Movement.ReadValue<Vector2>();
            deltaWalk = walkInput * walkSpeed * Time.deltaTime;
            tempPositionWalk = transform.position;
            tempPositionWalk.x += deltaWalk.x;
            tempPositionWalk.z += deltaWalk.y;
            transform.position = tempPositionWalk;
            //transform.Translate(panInput.y * panSpeed * Time.deltaTime, 0f, -panInput.x * panSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        walkCoroutineRunning = false;
    }

    public void ToggleInventoryUI()
    {
        if (inventoryOpen)
        {
            inventoryPanel.SetActive(false);
            inventoryOpen = false;
            PauseUI.isPaused = false;
        } else
        {
            inventoryPanel.SetActive(true);
            inventoryOpen = true;
            PauseUI.isPaused = true;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
