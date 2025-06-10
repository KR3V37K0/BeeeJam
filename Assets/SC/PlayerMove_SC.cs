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
    private Vector3 targetPosition;
    private bool isMoving = false, isRotating=false;

    private Vector3 velocity;
    [SerializeField] private float smoothTime = 0.2f;
    
    // Фиксированная Z-позиция пчелы (например, 0)
    [SerializeField] private float fixedZPosition = -2f; 

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
            RotateTowards();
        }

        //if(transform.rotation.z!=0 && isRotating==false)Z_rotator();
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

    async void Z_rotator(){
        isRotating=true;
        float a=transform.rotation.z/30;
        for(int b=0;b<30;b++){
            Quaternion quat= transform.rotation;
            quat.z+=a;
            transform.rotation =quat;
            await Task.Delay(30);
            if(transform.rotation.z==0){
                isRotating=false;
                return;
            }
        }
        isRotating=false;
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
