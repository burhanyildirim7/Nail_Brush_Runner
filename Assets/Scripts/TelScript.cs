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



    void FixedUpdate()
    {
        if (_myId == 0)
        {

        }
        else
        {
            if (FircaCogalmaScripti.instance._durum == true && transform.eulerAngles != new Vector3(-90 + (_myId * 4), 90, -180))
            {
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(-90 + (_myId * 4), 90, -180), 1f);
                StartCoroutine(AciArtirma());
            }
            else if (FircaCogalmaScripti.instance._durum == false && transform.eulerAngles != _idleRotation)
            {
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _idleRotation, 1f);
                StartCoroutine(AciAzaltma());
            }
            else
            {

            }
        }

        IEnumerator AciArtirma()
        {
            transform.eulerAngles = transform.eulerAngles + new Vector3(-90f + (_myId * 4 * 0.01f), 90, -180);
            yield return new WaitForSeconds(0.01f);
        }


        IEnumerator AciAzaltma()
        {
            transform.eulerAngles = transform.eulerAngles - new Vector3(-90f + (_myId * 4 * 0.01f), 90, -180);
            yield return new WaitForSeconds(0.01f);
        }

    }
}
