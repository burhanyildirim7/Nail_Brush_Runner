using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogalmaKapiScript : MonoBehaviour
{
    public static CogalmaKapiScript instance;

    public int _kapiDegeri;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }
}
