using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabable_outline_SC : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private GameObject currentObject;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("grabable"))
            {
                GameObject hovered = hit.collider.gameObject;

                // Если навели на другой объект
                if (hovered != currentObject)
                {
                    ClearOutline(); // Убираем прошлую обводку
                    currentObject = hovered;
                    SetOutline(currentObject, true); // Включаем новую
                }
                // Иначе — остаёмся на том же объекте, ничего не делаем
            }
            else
            {
                // Навели на что-то другое — убираем обводку
                ClearOutline();
            }
        }
        else
        {
            // Если никуда не навели — тоже убираем обводку
            ClearOutline();
        }
    }

    void SetOutline(GameObject obj, bool state)
    {
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = state;
    }

    void ClearOutline()
    {
        if (currentObject != null)
        {
            SetOutline(currentObject, false);
            currentObject = null;
        }
    }
}
