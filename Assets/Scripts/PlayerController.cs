using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using PaintIn3D;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;

    [SerializeField] private List<Color> _renkler = new List<Color>();

    [SerializeField] private GameObject _fircaUcu;

    [SerializeField] private GameObject _fircaUcuParent;

    [SerializeField] private GameObject _starObject;
    [SerializeField] private GameObject _teddyObject;
    [SerializeField] private GameObject _kalpObject;
    [SerializeField] private GameObject _moneyObject;

    private GameObject _degdigiObje;




    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    void Start()
    {
        StartingEvents();
        //Color renk = new Color(55f, 55f, 55f, 255f);

    }

    /// <summary>
    /// Playerin collider olaylari.. collectible, engel veya finish noktasi icin. Burasi artirilabilir.
    /// elmas icin veya baska herhangi etkilesimler icin tag ekleyerek kontrol dongusune eklenir.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("collectible"))
        {
            FircaCogalmaScripti.instance.FircaCogalt();
            // COLLECTIBLE CARPINCA YAPILACAKLAR...
            GameController.instance.SetScore(collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku

            Destroy(other.gameObject);

        }
        else if (other.CompareTag("engel"))
        {
            // ENGELELRE CARPINCA YAPILACAKLAR....
            GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
            {
                // FAİL EVENTLERİ BURAYA YAZILACAK..
                GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                                                             // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
            }


        }
        else if (other.CompareTag("finish"))
        {
            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            //GameController.instance.isContinue = false;
            GameController.instance._finisheGeldi = true;
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            GameController.instance.ScoreCarp(1);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
                                                   // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
                                                   //UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
                                                   // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..


        }
        else if (other.CompareTag("HavuzaGiris"))
        {
            transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1f, transform.localPosition.z);

            GameController.instance._havuzda = true;

            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("HavuzunOrtasi"))
        {
            // transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1f, transform.localPosition.z);

            int deger = other.gameObject.GetComponent<HavuzRengiScript>()._renkDegeri;
            P3dPaintDecal.instance.color = _renkler[deger];

            for (int i = 0; i < _fircaUcuParent.transform.childCount; i++)
            {
                _fircaUcuParent.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<P3dPaintDecal>().color = _renkler[deger];
            }


            _fircaUcu.GetComponent<MeshRenderer>().material.color = _renkler[deger];

            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("HavuzdanCikis"))
        {
            //transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);

            GameController.instance._havuzda = false;

            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("CogalmaKapisi"))
        {
            //transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);

            FircaCogalmaScripti.instance.eksilmeAdeti = 0;

            int kapideger = other.gameObject.GetComponent<CogalmaKapiScript>()._kapiDegeri;

            for (int i = 0; i < kapideger; i++)
            {
                FircaCogalmaScripti.instance.FircaCogalt();
            }

            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("EksilmeKapisi"))
        {
            //transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);

            FircaCogalmaScripti.instance.eksilmeAdeti = 0;


            int kapideger = other.gameObject.GetComponent<CogalmaKapiScript>()._kapiDegeri;

            if (FircaCogalmaScripti.instance.fircaAdeti >= kapideger)
            {
                for (int i = 0; i < kapideger; i++)
                {
                    FircaCogalmaScripti.instance.FircaEksilt();
                }
            }
            else
            {
                GameController.instance.isContinue = false;
                Invoke("LoseBaslat", 1);
            }



            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("StarKapisi"))
        {
            GameController.instance._playerMoney = false;
            GameController.instance._playerKalp = false;
            GameController.instance._playerTeddy = false;
            GameController.instance._playerStar = true;

            _moneyObject.SetActive(false);
            _kalpObject.SetActive(false);
            _teddyObject.SetActive(false);
            _starObject.SetActive(true);
        }
        else if (other.CompareTag("TeddyKapisi"))
        {
            GameController.instance._playerStar = false;
            GameController.instance._playerMoney = false;
            GameController.instance._playerKalp = false;
            GameController.instance._playerTeddy = true;

            _starObject.SetActive(false);
            _moneyObject.SetActive(false);
            _kalpObject.SetActive(false);
            _teddyObject.SetActive(true);
        }
        else if (other.CompareTag("KalpKapisi"))
        {
            GameController.instance._playerTeddy = false;
            GameController.instance._playerStar = false;
            GameController.instance._playerMoney = false;
            GameController.instance._playerKalp = true;

            _teddyObject.SetActive(false);
            _starObject.SetActive(false);
            _moneyObject.SetActive(false);
            _kalpObject.SetActive(true);
        }
        else if (other.CompareTag("MoneyKapisi"))
        {
            GameController.instance._playerKalp = false;
            GameController.instance._playerTeddy = false;
            GameController.instance._playerStar = false;
            GameController.instance._playerMoney = true;

            _kalpObject.SetActive(false);
            _teddyObject.SetActive(false);
            _starObject.SetActive(false);
            _moneyObject.SetActive(true);
        }

    }


    private IEnumerator HavuzaGirisNumerator()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_degdigiObje.gameObject.transform.position.x, transform.position.y, transform.position.z), 10);
        yield return new WaitForSeconds(0.1f);
    }


    private void WinBaslat()
    {
        UIController.instance.ActivateWinScreen();
    }

    private void LoseBaslat()
    {
        UIController.instance.ActivateLooseScreen();
    }

    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {

        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance._finisheGeldi = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, transform.position.y, 0);
        GetComponent<Collider>().enabled = true;


        GameController.instance._playerKalp = false;
        GameController.instance._playerTeddy = false;
        GameController.instance._playerStar = false;
        GameController.instance._playerMoney = false;

        _kalpObject.SetActive(false);
        _teddyObject.SetActive(false);
        _starObject.SetActive(false);
        _moneyObject.SetActive(false);


        FircaCogalmaScripti.instance.fircaAdeti = 0;
        FircaCogalmaScripti.instance.eksilmeAdeti = 0;
        FircaCogalmaScripti.instance.FircayiSifirla();

        P3dPaintDecal.instance.color = _renkler[5];

        for (int i = 0; i < _fircaUcuParent.transform.childCount; i++)
        {
            _fircaUcuParent.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<P3dPaintDecal>().color = _renkler[5];
        }


        _fircaUcu.GetComponent<MeshRenderer>().material.color = _renkler[5];

    }

}
