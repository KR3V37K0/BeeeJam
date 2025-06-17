using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMove_SC : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float fixedZPosition = -2f;
    
    [Header("Tilt Settings")] 
    [SerializeField] private float tiltAmount = 5f;
    [SerializeField] private float tiltSmoothness = 2f;
    
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float currentTilt;
    private GameObject spawn;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += onLoaded;
    }
    public void onLoaded(Scene scene, LoadSceneMode mode)
    {
        spawn = GameObject.FindWithTag("Respawn");
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = rb.position; // Инициализация цели
    }

    private void Update()
    {
        // Установка новой цели по клику
        if (Input.GetMouseButton(0))
        {
            SetTargetToMousePosition();
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        // Обработка наклона
        HandleTilt();
    }

    private void FixedUpdate()
    {
        if (!isMoving) 
        {
            //transform.rotation = Quaternion.Euler(0,math.round(transform.rotation.y),0);
            return;
        }
        
        // Движение к цели
        MoveToTarget();
        
        // Поворот в сторону движения
        RotateTowards();
        
        // Проверка достижения цели
        CheckIfReached();
    }

    private void SetTargetToMousePosition()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, Vector3.forward * fixedZPosition);
        
        if (plane.Raycast(ray, out float distance))
        {
            targetPosition = ray.GetPoint(distance);
            targetPosition.z = fixedZPosition; // Фиксируем Z
            isMoving = true;
        }
    }

    private void MoveToTarget()
    {
        // Рассчитываем направление
        Vector3 direction = (targetPosition - rb.position).normalized;
        
        // Простое движение с постоянной скоростью
        rb.velocity = direction * moveSpeed;
        
        // Гарантируем фиксированную Z-позицию
        if (Mathf.Abs(rb.position.z - fixedZPosition) > 0.01f)
        {
            rb.position = new Vector3(rb.position.x, rb.position.y, fixedZPosition);
        }
    }
    private void RotateTowards()
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.z = 0;

        if (direction.magnitude < 0.01f) return;

        direction.Normalize();

        // Разворачиваем в противоположную сторону (лицом к камере)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        Quaternion baseRotation = Quaternion.Euler(0, targetAngle, 0);
        Quaternion tiltRotation = Quaternion.Euler(0, 0, currentTilt);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            baseRotation * tiltRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void CheckIfReached()
    {
        if (Vector3.Distance(rb.position, targetPosition) <= stopDistance)
        {
            rb.velocity = Vector3.zero;
            isMoving = false;
        }
    }

    private void HandleTilt()
{
    // Только если есть движение и мы не врезались
    if (isMoving && rb.velocity.magnitude > 0.1f)
    {
        // Берем направление к цели, а не текущую velocity
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        float targetTilt = -1*math.abs( targetDirection.x) * tiltAmount;
        
        // Ограничиваем максимальный наклон
        targetTilt = Mathf.Clamp(targetTilt, -tiltAmount, tiltAmount);
        
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmoothness * Time.deltaTime);
    }
    else
    {
        // Плавный возврат в исходное положение
        currentTilt = Mathf.Lerp(currentTilt, 0f, tiltSmoothness * 2 * Time.deltaTime);
    }

    // Применяем наклон ТОЛЬКО по оси Z
    transform.rotation = Quaternion.Euler(
        transform.rotation.eulerAngles.x,
        transform.rotation.eulerAngles.y,
        currentTilt
    );
}
}
