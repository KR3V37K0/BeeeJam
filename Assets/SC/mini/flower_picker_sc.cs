using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class flower_picker_sc : MonoBehaviour
{
    [SerializeField] int winCount;
    bool wined = false;
    [SerializeField] Sprite sprt_win;
    [SerializeField] SpriteRenderer nps_render;
    [SerializeField] GameObject txt_win;
    private List<GameObject> flowers = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "grabable")
        {
            if (!flowers.Contains(col.gameObject))
            {
                flowers.Add(col.gameObject);
            }
            if (flowers.Count >= winCount) win();

        }
    }
    async void win()
    {
        if (wined) return;
        wined = true;


        await Task.Delay(2000);
        Debug.LogError("WIN");
        nps_render.sprite = sprt_win;
        txt_win.SetActive(true);
        await Task.Delay(4000);

        Events_SC.TriggerWin();
    }
}
