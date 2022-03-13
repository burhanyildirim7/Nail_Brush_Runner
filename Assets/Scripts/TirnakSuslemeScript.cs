using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirnakSuslemeScript : MonoBehaviour
{

    [SerializeField] private bool _star;
    [SerializeField] private bool _teddy;
    [SerializeField] private bool _kalp;
    [SerializeField] private bool _money;

    [SerializeField] private GameObject _particle;

    private MeshRenderer _mesh;

    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;
        _particle.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("FircaBolumu"))
        {
            if (GameController.instance._playerStar)
            {
                if (_star)
                {
                    _mesh.enabled = true;
                    _particle.SetActive(true);
                }
                else
                {

                }
            }
            else if (GameController.instance._playerTeddy)
            {
                if (_teddy)
                {
                    _mesh.enabled = true;
                    _particle.SetActive(true);
                }
                else
                {

                }
            }
            else if (GameController.instance._playerKalp)
            {
                if (_kalp)
                {
                    _mesh.enabled = true;
                    _particle.SetActive(true);
                }
                else
                {

                }
            }
            else if (GameController.instance._playerMoney)
            {
                if (_money)
                {
                    _mesh.enabled = true;
                    _particle.SetActive(true);
                }
                else
                {

                }
            }
            else
            {

            }

        }
        else
        {

        }
    }
}
