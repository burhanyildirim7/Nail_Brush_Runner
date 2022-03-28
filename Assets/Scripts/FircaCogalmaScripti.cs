using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FircaCogalmaScripti : MonoBehaviour
{

    public static FircaCogalmaScripti instance;

    [HideInInspector] public int fircaAdeti;
    //[SerializeField] string okunacakObjeninTagi;
    [SerializeField] GameObject cogalacakObje, cogalacakObjeParent;
    [SerializeField] int degerRotasyon;

    int donusDegeri;

    [HideInInspector] public int eksilmeAdeti;

    private int _toplamSayi;

    private int _fircaSayisi;

    private bool _aciArtir, _aciAzalt, _tirnaktaMi;

    public bool _durum;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        fircaAdeti = 0;
        eksilmeAdeti = 0;
        _durum = false;
        _aciArtir = false;
        _tirnaktaMi = false;
        _aciAzalt = false;
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == okunacakObjeninTagi)
        {
            fircaAdeti++;
            donusDegeri = fircaAdeti * degerRotasyon;
            // tempObj1
            GameObject tempObj1 = Instantiate(cogalacakObje, cogalacakObje.transform.position, Quaternion.identity);
            tempObj1.transform.parent = cogalacakObjeParent.transform;
            tempObj1.transform.position = cogalacakObje.transform.position;
            // tempObj2
            GameObject tempObj2 = Instantiate(cogalacakObje, cogalacakObje.transform.position, Quaternion.identity);
            tempObj2.transform.parent = cogalacakObjeParent.transform;
            tempObj2.transform.position = cogalacakObje.transform.position;
            //rotasyon ayarlama
            tempObj1.transform.eulerAngles = new Vector3((-90 + donusDegeri), 90, -180);
            tempObj2.transform.eulerAngles = new Vector3((-90 - donusDegeri), 90, -180);

        }
    }

    */
    public void FircaCogalt()
    {
        fircaAdeti++;
        donusDegeri = fircaAdeti * degerRotasyon;
        // tempObj1
        GameObject tempObj1 = Instantiate(cogalacakObje, cogalacakObje.transform.position, Quaternion.identity);
        tempObj1.transform.parent = cogalacakObjeParent.transform;
        tempObj1.transform.position = cogalacakObje.transform.position;
        tempObj1.transform.localScale = cogalacakObje.transform.localScale;
        // tempObj2
        GameObject tempObj2 = Instantiate(cogalacakObje, cogalacakObje.transform.position, Quaternion.identity);
        tempObj2.transform.parent = cogalacakObjeParent.transform;
        tempObj2.transform.position = cogalacakObje.transform.position;
        tempObj2.transform.localScale = cogalacakObje.transform.localScale;
        //rotasyon ayarlama
        tempObj1.transform.eulerAngles = new Vector3((-90 + donusDegeri), 90, -180);
        tempObj2.transform.eulerAngles = new Vector3((-90 - donusDegeri), 90, -180);

        tempObj1.GetComponent<TelScript>()._idleRotation = new Vector3((-90 + donusDegeri), 90, -180);
        tempObj2.GetComponent<TelScript>()._idleRotation = new Vector3((-90 - donusDegeri), 90, -180);

        tempObj1.GetComponent<TelScript>()._myId = -fircaAdeti;
        tempObj2.GetComponent<TelScript>()._myId = fircaAdeti;

        //eksilmeAdeti = 0;
    }

    public void FircaEksilt()
    {
        for (int i = 0; i < 2; i++)
        {
            Destroy(cogalacakObjeParent.transform.GetChild(cogalacakObjeParent.transform.childCount - 1 - eksilmeAdeti).gameObject);
            eksilmeAdeti++;
        }

        fircaAdeti--;
    }

    public void FircayiSifirla()
    {
        for (int i = 0; i < cogalacakObjeParent.transform.childCount; i++)
        {
            Destroy(cogalacakObjeParent.transform.GetChild(cogalacakObjeParent.transform.childCount - 1 - i).gameObject);

        }
    }

    public void TirnagaDegiyor()
    {

        //fircaAdeti = 0;
        _durum = true;

        // _aciAzalt = false;
        // _aciArtir = true;
        // _tirnaktaMi = true;

        //StartCoroutine(FircaHareketAcilma());
        /*
        for (int i = 0; i < cogalacakObjeParent.transform.childCount; i += 2)
        {


            fircaAdeti++;
            donusDegeri = fircaAdeti * 3;

            cogalacakObjeParent.transform.GetChild(i).transform.eulerAngles = new Vector3((-90 + donusDegeri), 90, -180);
            cogalacakObjeParent.transform.GetChild(i + 1).transform.eulerAngles = new Vector3((-90 - donusDegeri), 90, -180);

        }
        */

    }

    public void TirnagaDegmiyor()
    {
        //_fircaSayisi = fircaAdeti;
        //fircaAdeti = 0;
        _durum = false;

        // _aciArtir = false;
        // _aciAzalt = true;
        // _tirnaktaMi = true;

        //StartCoroutine(FircaHareketKapanma());

        /*
        for (int i = 0; i < cogalacakObjeParent.transform.childCount; i += 2)
        {

            fircaAdeti++;
            donusDegeri = fircaAdeti * degerRotasyon;

            cogalacakObjeParent.transform.GetChild(i).transform.eulerAngles = new Vector3((-90 + donusDegeri), 90, -180);
            cogalacakObjeParent.transform.GetChild(i + 1).transform.eulerAngles = new Vector3((-90 - donusDegeri), 90, -180);

        }
        */
    }

    private IEnumerator FircaHareketKapanma()
    {
        for (int i = cogalacakObjeParent.transform.childCount - 1; i > 0; i -= 2)
        {
            yield return new WaitForSeconds(0.1f);

            fircaAdeti++;
            donusDegeri = fircaAdeti * degerRotasyon;
            _fircaSayisi--;

            cogalacakObjeParent.transform.GetChild(i).transform.eulerAngles = new Vector3((-90 + donusDegeri), 90, -180);
            cogalacakObjeParent.transform.GetChild(i - 1).transform.eulerAngles = new Vector3((-90 - donusDegeri), 90, -180);

        }

    }

    private IEnumerator FircaHareketAcilma()
    {
        for (int i = cogalacakObjeParent.transform.childCount - 1; i > 0; i -= 2)
        {
            yield return new WaitForSeconds(0.1f);

            fircaAdeti++;
            donusDegeri = fircaAdeti * 4;

            cogalacakObjeParent.transform.GetChild(i).transform.eulerAngles = new Vector3((-90 + donusDegeri), 90, -180);
            cogalacakObjeParent.transform.GetChild(i - 1).transform.eulerAngles = new Vector3((-90 - donusDegeri), 90, -180);

        }

    }
}