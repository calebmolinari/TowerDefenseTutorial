using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PauseUI pauseUI;
    public PlayerInput controls;
    Vector3 tempPositionZoom;
    Vector3 tempPositionPan;
    Vector2 panInput;
    Vector2 deltaPan;
    bool panCoroutineRunning = false;
    public float panSpeed = 30f;
    [SerializeField] float zoomIncrement = 15f;
    [SerializeField] float minX = 10f;
    [SerializeField] float maxX = 60f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 60f;
    [SerializeField] float minZ = 10f;
    [SerializeField] float maxZ = 60f;


    private void Awake()
    {
        controls = new PlayerInput();
        controls.TowerDefense.Zoom.performed += _ => Zoom(_.ReadValue<float>());
        controls.TowerDefense.PanCamera.performed += ctx => PanCamera();
        controls.TowerDefense.Pause.performed += ctx => pauseUI.TogglePause();
    }

    private void PanCamera()
    {
        if (panCoroutineRunning == false)
            StartCoroutine(OnInputPanCamera());
    }

    private IEnumerator OnInputPanCamera()
    {
        if (GameManager.gameEnded)
        {
            this.enabled = false;
            yield break;
        }
        panCoroutineRunning = true;
        while (controls.TowerDefense.PanCamera.IsPressed())
        {
            panInput = controls.TowerDefense.PanCamera.ReadValue<Vector2>();
            deltaPan = panInput * panSpeed * Time.deltaTime;
            tempPositionPan = transform.position;
            tempPositionPan.x += deltaPan.y;
            tempPositionPan.x = Mathf.Clamp(tempPositionPan.x, minX, maxX);
            tempPositionPan.z += -deltaPan.x;
            tempPositionPan.z = Mathf.Clamp(tempPositionPan.z, minZ, maxZ);
            transform.position = tempPositionPan;
            //transform.Translate(panInput.y * panSpeed * Time.deltaTime, 0f, -panInput.x * panSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        panCoroutineRunning = false;
    }

    void Zoom(float zoom)
    {
        float deltaZoom = zoomIncrement * zoom * Time.deltaTime;
        tempPositionZoom = transform.position;
        tempPositionZoom.y -= deltaZoom;
        tempPositionZoom.y = Mathf.Clamp(tempPositionZoom.y, minY, maxY);
        transform.position = tempPositionZoom;
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
