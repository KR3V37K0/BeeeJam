using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class rain_npc_sc : MonoBehaviour
{
    [SerializeField] float full_Wait, current_Wait;
    [SerializeField] Sprite sprt_win;
    bool wined = false;
    void FixedUpdate()
    {
        if (current_Wait >= full_Wait) win();
        else current_Wait += Time.deltaTime;
    }
    async void win()
    {
        if (wined) return;
        wined = true;

        
        Debug.LogError("WIN");
        gameObject.transform.GetComponentInChildren<SpriteRenderer>().sprite = sprt_win;
        transform.Find("txt_win").gameObject.SetActive(true);
        await Task.Delay(3000);

        Events_SC.TriggerWin();

    }
    void OnParticleCollision(GameObject coll)
    {
        if(coll.tag=="rain")current_Wait = 0;
    }
}
