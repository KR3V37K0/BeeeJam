using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove_SC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f; // Скорость разворота
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float tiltAmount = 5f; // Максимальный угол наклона
    [SerializeField] private float tiltSmoothness = 2f; // Плавность наклона
    private Vector3 targetPosition;
    private bool isMoving = false;

    private Vector3 velocity;
    [SerializeField] private float smoothTime = 0.2f;
    
    // Фиксированная Z-позиция пчелы (например, 0)
    [SerializeField] private float fixedZPosition = -2f; 
    private float currentTilt = 0f; // Текущий наклон по Z
    private Quaternion initialRotation; // Начальный поворот без наклона

    private void Start()
    {
        initialRotation = transform.rotation;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetToMousePosition();
            isMoving = true;
        }

        if (isMoving)
        {
            MoveToTarget();
            ApplyMovementTilt();
            RotateTowards();
            
        }
        else
        {
            ReturnToNeutralTilt();
        }
    }
    private void ApplyMovementTilt()
    {
        // Вычисляем наклон на основе скорости
        float targetTilt = math.abs( velocity.x) * -1* tiltAmount;
        
        // Плавно изменяем наклон
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmoothness * Time.deltaTime);
        
        // Применяем наклон к текущему повороту
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, currentTilt);
    }

    private void ReturnToNeutralTilt()
    {
        // Плавно возвращаем наклон в 0
        currentTilt = Mathf.Lerp(currentTilt, 0f, tiltSmoothness * Time.deltaTime);
        
        // Сохраняем основной поворот, добавляя только наклон
        Quaternion baseRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.rotation = baseRotation * Quaternion.Euler(0, 0, currentTilt);
        
        // Альтернативный вариант - возврат к initialRotation
        // transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, tiltSmoothness * Time.deltaTime);
    }

    private void SetTargetToMousePosition()
    {
        // 1. Берём луч от камеры до точки мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // 2. Вычисляем плоскость, в которой летает пчела (Z = fixedZPosition)
        Plane beePlane = new Plane(Vector3.forward, new Vector3(0, 0, fixedZPosition));
        
        // 3. Находим пересечение луча и плоскости
        if (beePlane.Raycast(ray, out float distance))
        {
            targetPosition = ray.GetPoint(distance);
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime,
            moveSpeed
        );

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    private void RotateTowards()
    {
        Vector3 direction = (targetPosition - transform.position).normalized *-1;
        direction.z = 0; // Игнорируем глубину (ось Z), если движение 2D
        
        if (direction != Vector3.zero)
        {
            // Для вида сбоку поворачиваем объект по оси Y
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            
            // Корректируем угол под твою систему координат
            // (например, если 0° = "вправо", 180° = "влево")
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle + 90, 0);
            
            // Плавный поворот через Slerp (или RotateTowards)
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

}
