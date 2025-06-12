using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;
using UnityEditor.UI;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor;
public class Levels_Letters : MonoBehaviour
{
[Header("System")]
    [SerializeField] int LevelProgress;
    [SerializeField] Mission_SCO[] allMissions;
    [SerializeField] GameObject canvas_home, canvas_level;
[Header("UI BASE")]
    [SerializeField] TMP_Text txt_letter;

[Header("UI LEVEL")]
    [SerializeField] Image img_fade;
    [SerializeField] TweenSettings<float> float_fade_on, float_fade_off;
    [SerializeField] GameObject img_win;


    private void OnEnable()
    {
        Events_SC.OnLevelWin += onWin;
    }

    private void OnDisable()
    {
        Events_SC.OnLevelWin -= onWin;
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += event_LevelStarted;
        img_fade.color = Color.black;
    }
    async public void btn_OpenLetter()
    {
        txt_letter.text = allMissions[LevelProgress].letter;
    }
    public Mission_SCO getCurrentMission()
    {
        return allMissions[LevelProgress];
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
    async public void onWin()
    {
        LevelProgress++;
        img_win.SetActive(true);
        await Tween.Scale(img_win.transform, 0f, 0.01f);
        await Tween.Scale(img_win.transform, 1f, 0.4f);
        await Tween.Scale(img_win.transform, 0.8f, 0.4f);
        await Tween.Scale(img_win.transform, 1.1f, 0.4f);
        await Tween.Scale(img_win.transform, 1f, 0.4f);
        await Task.Delay(4000);
        img_win.SetActive(false);
        btn_Home();
    }
}
