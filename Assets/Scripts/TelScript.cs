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
        if (FircaCogalmaScripti.instance._durum == true &&_myId !=0)
        {
            transform.eulerAngles = new Vector3(-90 + (_myId * 3),90,-180);
        }
        else if(FircaCogalmaScripti.instance._durum == false && _myId != 0)
        {
            transform.eulerAngles = _idleRotation;
        }
        else
        {
                
        }

        /*
        if (_myId == 0)
        {

        }
        else
        {
            if (_myId<0)
            {
                if (FircaCogalmaScripti.instance._durum == true && transform.eulerAngles.x <= (-90 + (_myId * 4)))
                {
                    //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(-90 + (_myId * 4), 90, -180), 1f);
                    StartCoroutine(AciArtirma());
                }
                else if (FircaCogalmaScripti.instance._durum == false && transform.eulerAngles.x >= _idleRotation.x)
                {
                    //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _idleRotation, 1f);
                    StartCoroutine(AciAzaltma());
                }
                else
                {

                }

            }
            else
            {
                if (FircaCogalmaScripti.instance._durum == true && transform.eulerAngles.x >= (-90 + (_myId * 4)))
                {
                    //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(-90 + (_myId * 4), 90, -180), 1f);
                    StartCoroutine(AciArtirma());
                }
                else if (FircaCogalmaScripti.instance._durum == false && transform.eulerAngles.x <= _idleRotation.x)
                {
                    //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _idleRotation, 1f);
                    StartCoroutine(AciAzaltma());
                }
                else
                {

                }

            }
        }

        IEnumerator AciArtirma()
        {
            transform.eulerAngles = transform.eulerAngles - new Vector3((_myId * 4 * 0.01f), 0, 0);
            yield return new WaitForSeconds(0.01f);
        }


        IEnumerator AciAzaltma()
        {
            transform.eulerAngles = transform.eulerAngles + new Vector3((_myId * 4 * 0.01f), 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        */
    }
}
