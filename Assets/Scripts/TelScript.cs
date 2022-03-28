using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelScript : MonoBehaviour
{

    public static TelScript instance;

    public int _myId;

    public Vector3 _idleRotation;

    public float _aciDegisimMiktari;


    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }



    void Update()
    {
        if (_myId == 0)
        {

        }
        else
        {
            if (FircaCogalmaScripti.instance._durum == true && transform.eulerAngles != _idleRotation)
            {
                if (_myId > 0)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(_myId * 4, 0, 0), _aciDegisimMiktari);
                }
                else
                {
                    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _idleRotation, _aciDegisimMiktari);
                }
            }
            else if (FircaCogalmaScripti.instance._durum == false && transform.eulerAngles != _idleRotation)
            {
                if (_myId > 0)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _idleRotation, _aciDegisimMiktari);
                }
                else
                {
                    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(_myId * 4, 0, 0), _aciDegisimMiktari);
                }
            }
            else
            {

            }
        }


    }
}
