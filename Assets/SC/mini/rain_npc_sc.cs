using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain_npc_sc : MonoBehaviour
{
    [SerializeField] float full_Wait, current_Wait;
    bool wined = false;
    void FixedUpdate()
    {
        if (current_Wait >= full_Wait) win();
        else current_Wait += Time.deltaTime;
    }
    void win()
    {
        if (wined) return;
        wined = true;
        Debug.LogError("WIN");

        Events_SC.TriggerWin();

    }
    void OnParticleCollision(GameObject coll)
    {
        if(coll.tag=="rain")current_Wait = 0;
    }
}
