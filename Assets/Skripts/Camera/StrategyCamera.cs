using UnityEngine;
using UnityEngine.InputSystem;

public class StrategyCamera : MonoBehaviour {
    [Header("Настройки движения (WASD)")]
    public float moveSpeed = 20f;
    public Vector2 mapBounds = new(50f, 50f);

    [Header("Настройки перетаскивания (ПКМ)")]
    public float dragSpeed = 0.05f; // Скорость перетаскивания (обычно маленькая, т.к. дельта в пикселях)

    [Header("Настройки зума")]
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 20f;

    private CameraControls controls;
    private Vector2 moveInput;
    private float zoomInput;
    private Vector2 dragDelta;

    void Awake() {
        controls = new CameraControls();
    }

    void OnEnable() {
        controls.Player.Enable();
    }

    void OnDisable() {
        controls.Player.Disable();
    }

    void FixedUpdate() {
        if (controls.Player.Move.IsPressed()) 
            MoveCamera();
        if (controls.Player.Zoom.IsPressed())
            HandleZoom();

        // Проверяем, зажата ли правая кнопка мыши
        if (controls.Player.RightClick.IsPressed()) 
            HandleDrag();
        
    }

    void MoveCamera() {
        // Считываем позицию камеры
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        // Переводим ее в новую позицию
        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void HandleDrag() {
        // Важный момент: чтобы карта двигалась "за мышкой", 
        // камеру нужно двигать в ПРОТИВОПОЛОЖНУЮ сторону от движения мыши.
        // Поэтому ставим минус перед dragDelta.
        dragDelta = controls.Player.DragDelta.ReadValue<Vector2>();
        Vector3 dragDirection = new Vector3(-dragDelta.x, -dragDelta.y, 0);

        // Здесь Time.deltaTime обычно не нужен, так как Delta уже зависит от частоты кадров,
        // но dragSpeed помогает настроить чувствительность.
        transform.Translate(dragDirection * dragSpeed, Space.World);

        //ClampPosition();
    
    }

    void HandleZoom() {
        zoomInput = controls.Player.Zoom.ReadValue<float>();
        if (zoomInput != 0) {
            Camera cam = GetComponent<Camera>();

            if (cam.orthographic) {
                // Для 2D Ортографической камеры
                cam.orthographicSize -= zoomInput * zoomSpeed * Time.deltaTime;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            }
            else {
                // Для Перспективной камеры (движение по Z)
                Vector3 pos = transform.position;
                pos.z += zoomInput * zoomSpeed * Time.deltaTime;
                pos.z = Mathf.Clamp(pos.z, minZoom, maxZoom);
                transform.position = pos;
            }
        }
    }
}
