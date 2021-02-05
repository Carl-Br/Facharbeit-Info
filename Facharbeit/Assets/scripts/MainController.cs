using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MainController : MonoBehaviour
{
    public GameObject Welt;
    public GameObject Haus;
    public GameObject HotSpot;
    public double Ansteckungsradius= 4.5;
    public int hotSpots = 3;//mindestens 1 (darf nicht null sein)=
    public int Personen = 30; //zwischen 30 und 2000

    // Start is called before the first frame update
    void Start()
    {
        erstelleWelt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Welt Bereich
    private void erstelleWelt()
    {
        //erstellt die Fläche
        int Häuser = Personen + hotSpots;
        double seitenLänge = Math.Sqrt(Personen+hotSpots);
        seitenLänge = Math.Ceiling(seitenLänge);//rundet seitenLänge auf
        Welt.transform.localScale = new Vector3((float)seitenLänge, (float)seitenLänge,1f);//ändere die Welt größe

        //setzte Häuser
        int HotSpotPositions = (int)Math.Round((double)Häuser / hotSpots);
        int gebauteHäuser = 0;
        float xPosition = (((float)seitenLänge / 2) * -1) + 0.5f;
        float yPosition = ((float)seitenLänge / 2) - 0.5f;

        //Wenn man coole Muster mit den Häusern und HotSpots sehen will, dann sollte man die Random Zahlen weg lassen
        System.Random rnd = new Random();
        int randomZahl1 = (int)Math.Round(seitenLänge / 5);
        int randomZahl2 = rnd.Next(3, 14);
        while (Häuser!= gebauteHäuser)
        {
            
            Instantiate(Haus, new Vector3(xPosition, yPosition, 1f), new Quaternion(0f, 0f, 0f, 0f));
            xPosition++;
            if (xPosition > seitenLänge/2)
            {
                yPosition--;
                xPosition = (((float)seitenLänge / 2) * -1) + 0.5f;
            }
            gebauteHäuser++;
        }
        
          
    }
}
