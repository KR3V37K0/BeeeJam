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
        Events_SC.OnLevelChange += onWin;
    }

    private void OnDisable()
    {
        Events_SC.OnLevelChange -= onWin;
    }
    void Update()
    {
        //joint.connectedAnchor = Vector3.zero;
        joint.anchor = transform.InverseTransformPoint(transform.position - jointOffset);
        if (!Input.GetMouseButton(1) && grabed != null)
        {
            grabed.transform.SetParent(null);
            //grabed.GetComponent<Rigidbody>().isKinematic = false;
            grabed.layer = LayerMask.NameToLayer("Default");
            picked_rb.constraints = picked_rbconst;
            grabed = null;
        }
    }
    RigidbodyConstraints picked_rbconst;
    Rigidbody picked_rb;
    void OnTriggerStay(Collider col)
    {
        if (Input.GetMouseButton(1) && grabed == null)
        {
            if (col.tag == "grabable")
            {
                col.transform.SetParent(obj_dot.transform);
                grabed = col.gameObject;
                //col.GetComponent<Rigidbody>().isKinematic = true;

                picked_rb = col.GetComponent<Rigidbody>();
                picked_rb.velocity = Vector3.zero;
                picked_rb.angularVelocity = Vector3.zero;
                picked_rbconst = picked_rb.constraints;
                picked_rb.constraints = RigidbodyConstraints.FreezeAll;
                grabed.layer = LayerMask.NameToLayer("onHand");

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
        grabeds.Clear();
        Debug.Log("now " + grabeds.Count);
    }
}
