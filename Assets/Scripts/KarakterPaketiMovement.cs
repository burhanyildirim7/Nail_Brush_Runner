using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterPaketiMovement : MonoBehaviour
{

    public static KarakterPaketiMovement instance;

    [SerializeField] private float _speed;

    private float _ilkHiz;

    private void Awake()
    {
        if (instance == null) instance = this;

        _ilkHiz = _speed;
        //else Destroy(this);
    }

    void Start()
    {

    }


    void FixedUpdate()
    {
        if (GameController.instance.isContinue == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        else
        {

        }

    }

    public void KarakterYavaslat()
    {
        _speed = _ilkHiz / 2;
    }

    public void KarakterHizlandir()
    {
        _speed = _ilkHiz * 2;
    }

    public void KarakterHizNormal()
    {
        _speed = _ilkHiz;
    }

}
