using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dintDestroySC : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
