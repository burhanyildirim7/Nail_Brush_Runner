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


    [SerializeField] private ParticleSystem _confetti1, _confetti2;



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
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);


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
            Time.timeScale = 1.5f;
            //GameController.instance.ScoreCarp(1);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
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
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

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
            if (GameController.instance._havuzda == true)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);

                GameController.instance._havuzda = false;
            }
            else
            {

            }


            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("CogalmaKapisi"))
        {
            //transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

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

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

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

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

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

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

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

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

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

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            GameController.instance._playerKalp = false;
            GameController.instance._playerTeddy = false;
            GameController.instance._playerStar = false;
            GameController.instance._playerMoney = true;

            _kalpObject.SetActive(false);
            _teddyObject.SetActive(false);
            _starObject.SetActive(false);
            _moneyObject.SetActive(true);
        }
        else if (other.CompareTag("X1"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti <= 2)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(1);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X2"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 2 && FircaCogalmaScripti.instance.fircaAdeti <= 4)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(2);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X3"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 4 && FircaCogalmaScripti.instance.fircaAdeti <= 6)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(3);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X4"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 6 && FircaCogalmaScripti.instance.fircaAdeti <= 8)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(4);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X5"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 8 && FircaCogalmaScripti.instance.fircaAdeti <= 10)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(5);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X6"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 10 && FircaCogalmaScripti.instance.fircaAdeti <= 12)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(6);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X7"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 12 && FircaCogalmaScripti.instance.fircaAdeti <= 14)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(7);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X8"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 14 && FircaCogalmaScripti.instance.fircaAdeti <= 16)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(8);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X9"))
        {
            if (FircaCogalmaScripti.instance.fircaAdeti > 16 && FircaCogalmaScripti.instance.fircaAdeti <= 18)
            {
                GameController.instance.isContinue = false;
                GameController.instance.ScoreCarp(9);
                Time.timeScale = 1;
                _confetti1.gameObject.SetActive(true);
                _confetti2.gameObject.SetActive(true);
                _confetti1.Play();
                _confetti2.Play();
                Invoke("WinBaslat", 2);

            }
            else
            {

            }
        }
        else if (other.CompareTag("X10"))
        {

            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(10);
            Time.timeScale = 1;
            _confetti1.gameObject.SetActive(true);
            _confetti2.gameObject.SetActive(true);
            _confetti1.Play();
            _confetti2.Play();
            Invoke("WinBaslat", 2);


        }
        else if (other.CompareTag("TirnakObjesi"))
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        }
        else
        {

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

        _confetti1.gameObject.SetActive(false);
        _confetti2.gameObject.SetActive(false);

    }

}
