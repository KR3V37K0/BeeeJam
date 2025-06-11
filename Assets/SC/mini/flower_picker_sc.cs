using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class flower_picker_sc : MonoBehaviour
{
    [SerializeField] int winCount;
    bool wined = false;
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
        Debug.LogError("WIN");

        await Task.Delay(2000);
        Events_SC.TriggerWin();
    }
}
