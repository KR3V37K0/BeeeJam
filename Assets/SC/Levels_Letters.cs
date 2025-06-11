using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels_Letters : MonoBehaviour
{
    [Header("System")]
    [SerializeField] int LevelProgress;
    [SerializeField] Mission_SCO[] allMissions;
    [Header("UI")]
    [SerializeField] TMP_Text txt_letter;



    public void btn_OpenLetter()
    {
        txt_letter.text = allMissions[LevelProgress].letter;
    }
    public void btn_StartLevel()
    {
        SceneManager.LoadScene(allMissions[LevelProgress].scene_name);
    }
}
