using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMoveSC : MonoBehaviour
{
    public Transform target;        // игрок или другой объект
    public float smoothSpeed = 0.125f;

    public Vector2 minBounds;       // минимальные координаты (X, Y)
    public Vector2 maxBounds;       // максимальные координаты (X, Y)

    public Vector3 offset;          // смещение от игрока (если нужно)
    [SerializeField] Levels_Letters levels;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += onLoaded;
    }
    public void onLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "START") return;
        if (SceneManager.GetActiveScene().name == "BASE")
        {
            minBounds = new Vector2(-3, 0);
            maxBounds = new Vector2(5, 4);
        }
        else
        {
            minBounds = levels.getCurrentMission().minBounds;
            maxBounds = levels.getCurrentMission().maxBounds;
        }
        
    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Ограничиваем позицию по границам
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        // Плавное движение
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
    
}
