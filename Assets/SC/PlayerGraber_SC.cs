using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerGraber_SC : MonoBehaviour
{
    [SerializeField] HingeJoint joint;
    [SerializeField] Vector3 jointOffset;
    [SerializeField] GameObject obj_dot, grabed;
    private List<GameObject> grabeds = new List<GameObject>();
    private void OnEnable()
    {
        Events_SC.OnLevelWin += onWin;
    }

    private void OnDisable()
    {
        Events_SC.OnLevelWin -= onWin;
    }
    void Update()
    {
        //joint.connectedAnchor = Vector3.zero;
        joint.anchor = transform.InverseTransformPoint(transform.position - jointOffset);
        if (!Input.GetMouseButton(1) && grabed != null)
        {
            grabed.transform.SetParent(null);
            grabed.GetComponent<Rigidbody>().isKinematic = false;
            grabed = null;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (Input.GetMouseButton(1) && grabed == null)
        {
            if (col.tag == "grabable")
            {
                col.transform.SetParent(obj_dot.transform);
                grabed = col.gameObject;
                col.GetComponent<Rigidbody>().isKinematic = true;

                if (!grabeds.Contains(col.gameObject))
                {
                    grabeds.Add(col.gameObject);
                }
            }
        }

    }
    async void onWin()
    {
        await Task.Delay(6000);
        Debug.Log("touched " + grabeds.Count);
        for (int i = 0; i < grabeds.Count; i++)
        {
            Destroy(grabeds[i]);
        }
        Debug.Log("now " + grabeds.Count);
    }
}
