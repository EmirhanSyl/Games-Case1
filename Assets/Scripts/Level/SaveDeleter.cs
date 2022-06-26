using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDeleter : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
