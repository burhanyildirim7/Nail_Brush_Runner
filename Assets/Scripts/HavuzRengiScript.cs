using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavuzRengiScript : MonoBehaviour
{

    public static HavuzRengiScript instance;

    public int _renkDegeri;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }
}
