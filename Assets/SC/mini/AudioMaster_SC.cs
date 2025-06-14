using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMaster_SC : MonoBehaviour
{
 public AudioMixer audioMixer;      // Подключи AudioMixer
    public Slider volumeSlider;        // Подключи Slider
    public string exposedParam = "MasterVolume"; // Название параметра в AudioMixer

    private void Start()
    {
        // Получаем сохранённое значение (если есть)
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Подписываемся на событие слайдера
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Конвертируем значение (0–1) в децибелы
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(exposedParam, dB);
        PlayerPrefs.SetFloat("Volume", volume); // Сохраняем значение
    }
}
