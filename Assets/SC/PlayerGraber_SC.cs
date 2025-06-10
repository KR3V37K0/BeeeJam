using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraber_SC : MonoBehaviour
{
    [SerializeField]HingeJoint joint;
    [SerializeField]Vector3 jointOffset;
    [SerializeField]GameObject obj_dot,grabed;
    void Update()
    {
        //joint.connectedAnchor = Vector3.zero;
        joint.anchor = transform.InverseTransformPoint(transform.position-jointOffset);
        if(!Input.GetMouseButton(1)&&grabed!=null)
        {
            grabed.transform.SetParent(null);
            grabed.GetComponent<Rigidbody>().isKinematic=false;
            grabed=null;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (Input.GetMouseButton(1)&&grabed==null){
            if(col.tag=="grabable"){
                col.transform.SetParent(obj_dot.transform);
                grabed=col.gameObject;
                col.GetComponent<Rigidbody>().isKinematic=true;
            }
        }
        
    }
}
