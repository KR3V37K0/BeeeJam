using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class hats_SC : MonoBehaviour
{
    [SerializeField] MeshFilter hat_mesh;
    Mesh getted;
    string name_onBee;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetToMousePosition();
        }
    }
    private void SetTargetToMousePosition()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
    	RaycastHit hit;

        // Проверяем, пересекается ли луч с чем-либо на сцене
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name+"  "+hit.transform.name.Contains("hat"));
            if (hit.transform.name.Contains("hat"))
            {
                getted = hit.transform.GetComponent<MeshFilter>().mesh;
                if (name_onBee == hit.transform.name)
                {
                    hat_mesh.mesh = null;
                    name_onBee = "";
                }
                else
                {
                    hat_mesh.mesh = getted;
                    name_onBee = hit.transform.name;
                }

            }
    	}
    }
}
