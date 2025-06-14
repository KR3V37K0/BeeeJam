using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class bee_sound_SC : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] AudioSource source1, source2;
    [SerializeField] float maxSpeed,volume,pitch_s,pitch_m,random_strength;
    [SerializeField] int random_speed=1000;
    float   pitch_standart, pitch_high;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inyt();
    }
    async void inyt()
    {
        source1.volume = 0;
        source2.volume = 0;

        source1.pitch = pitch_s;
        source2.pitch = pitch_s;
        

        source1.Play();
        await Task.Delay(800);
        source2.Play();
        Randomizer_sound();



        for (float i = 0; i < volume; i += 0.001f)
        {
            source1.volume = i;
            source2.volume = i;

            await Task.Delay(60);
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
    async void Randomizer_sound()
    {
        while (true)
        {
            await Task.Delay(random_speed);
            pitch_standart = Random.Range(pitch_s - random_strength, pitch_s + random_strength);
            pitch_high = Random.Range(pitch_m - random_strength, pitch_m + random_strength);
        }
    }
}
