using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FircaCogalmaScripti : MonoBehaviour
{
    [SerializeField] int fircaAdeti;
    [SerializeField] string okunacakObjeninTagi;
    [SerializeField] GameObject cogalacakObje,cogalacakObjeParent;
    [SerializeField] int degerRotasyon;
    int donusDegeri;

    // Start is called before the first frame update
    void Start()
    {
        fircaAdeti = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== okunacakObjeninTagi)
        {
            fircaAdeti++;
            donusDegeri = fircaAdeti*degerRotasyon;
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
}