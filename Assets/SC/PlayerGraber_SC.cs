using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraber_SC : MonoBehaviour
{
    [SerializeField]ConfigurableJoint joint;

    void Update()
    {
        if(!Input.GetMouseButton(1))joint.connectedBody=null;
    }
    void OnTriggerStay(Collider col)
    {
        if (Input.GetMouseButton(1)){
            if(col.tag=="grabable"){
                joint.connectedBody=col.gameObject.GetComponent<Rigidbody>();
            }
        }
        
    }
}
