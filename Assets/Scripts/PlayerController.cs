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
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
                                                       // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..


        }
        else if (other.CompareTag("HavuzaGiris"))
        {
            transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1f, transform.localPosition.z);

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

            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }
        else if (other.CompareTag("CogalmaKapisi"))
        {
            //transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);

            int kapideger = other.gameObject.GetComponent<CogalmaKapiScript>()._kapiDegeri;

            for (int i = 0; i < kapideger; i++)
            {
                FircaCogalmaScripti.instance.FircaCogalt();
            }

            // _degdigiObje = other.gameObject;
            //StartCoroutine(HavuzaGirisNumerator());
        }

    }


    private IEnumerator HavuzaGirisNumerator()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_degdigiObje.gameObject.transform.position.x, transform.position.y, transform.position.z), 10);
        yield return new WaitForSeconds(0.1f);
    }


    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {

        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, transform.position.y, 0);
        GetComponent<Collider>().enabled = true;

    }

}
