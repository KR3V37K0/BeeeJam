using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class bee_sound_SC : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] AudioSource source1, source2;
    [SerializeField] float maxSpeed,volume = 0.05f, pitch_standart = 1.05f, pitch_high = 1.3f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inyt();
    }
    async void inyt()
    {
        source1.volume = 0;
        source2.volume = 0;

        source1.Play();
        await Task.Delay(500);
        source2.Play();

        for (float i = 0; i < volume; i += 0.001f)
        {
            source1.volume = i;
            source2.volume = i;

            await Task.Delay(10);
        }
    }
    void Update()
    {
        float speed = rb.velocity.magnitude;

        // Нормализуем скорость в диапазон [0,1]
        float t = Mathf.Clamp01(speed / maxSpeed);

        // Интерполируем pitch
        source1.pitch = Mathf.Lerp(pitch_standart, pitch_high, t);
        source2.pitch = Mathf.Lerp(pitch_standart, pitch_high, t);
    }
}
