using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;
using UnityEditor.UI;
using UnityEngine.UI;
using System.Threading.Tasks;
public class Levels_Letters : MonoBehaviour
{
    [Header("System")]
    [SerializeField] int LevelProgress;
    [SerializeField] Mission_SCO[] allMissions;
    [Header("UI")]
    [SerializeField] TMP_Text txt_letter;
    [SerializeField] Image img_fade;
    [SerializeField] TweenSettings<float> float_fade_on, float_fade_off;
        [SerializeField] GameObject canvas_home,canvas_level;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += event_LevelStarted;
    }
    async public void btn_OpenLetter()
    {
        txt_letter.text = allMissions[LevelProgress].letter;
    }


    async public void btn_StartLevel()
    {
        await Tween.Alpha(img_fade, float_fade_on);
        SceneManager.LoadScene(allMissions[LevelProgress].scene_name);
    }
    async public void btn_Home()
    {
        await Tween.Alpha(img_fade, float_fade_on);
        SceneManager.LoadScene("BASE");
    }
    void switch_ui()
    {
        canvas_home.SetActive(false);
        canvas_level.SetActive(false);
        if (SceneManager.GetActiveScene().name == "BASE" || SceneManager.GetActiveScene().name == "START")
        {
            canvas_home.SetActive(true);
            canvas_home.transform.Find("_img_letter").gameObject.SetActive(false);
        }
        else canvas_level.SetActive(true);
    }
    async public void event_LevelStarted(Scene scene, LoadSceneMode mode)
    {
        switch_ui();
        await Task.Delay(2000);
        await Tween.Alpha(img_fade, float_fade_off);
    }
}
