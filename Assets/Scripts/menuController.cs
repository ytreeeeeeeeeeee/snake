using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    [SerializeField] GameObject cam;
    void Start()
    {
        cam.GetComponent<DBManager>().show();
    }
}
