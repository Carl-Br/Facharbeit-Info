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
    public GameObject NormalePerson;
    public double Ansteckungsradius= 4.5;
    public int hotSpots = 3;//mindestens 1 (darf nicht null sein)=
    public int personen = 30; //zwischen 30 und 2000
    public List<GameObject> personenListe;

    // Start is called before the first frame update
    void Start()
    {
        personenListe= new List<GameObject>();
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
        int Häuser = personen + hotSpots;
        double seitenLänge = Math.Sqrt(personen+hotSpots);
        seitenLänge = Math.Ceiling(seitenLänge);//rundet seitenLänge auf
        Welt.transform.localScale = new Vector3((float)seitenLänge+1, (float)seitenLänge+1,1f);//ändere die Welt größe (das +1 ist der rand)


        //legt fest an welchen stellen überall HotSpots sein sollen
        System.Random rnd = new Random();
        int randomZahl1 = (int)Math.Round(seitenLänge / 5);
        int[] HotSpotsArray = new int[hotSpots];
        for(int i =0;i<hotSpots;i++)
        {
            int randomZahl = rnd.Next(1, Häuser);
            foreach (int j in HotSpotsArray)
            {
                if(j== randomZahl)//an dieser Position ist bereits ein HotSpot
                {
                    i--;//Zähler soll sich nicht veränder, sondern  nur eine neue Zahl generieren
                    continue;
                }
            }
            //wenn an der Stelle noch kein HotSpot ist setzte dort einen HotSpot
            HotSpotsArray[i] = randomZahl;
        }


        //setzte Häuser und personen
        int HotSpotPositions = (int)Math.Round((double)Häuser / hotSpots);
        int gebauteHäuser = 0;
        float xPosition = (((float)seitenLänge / 2) * -1) + 0.5f;
        float yPosition = ((float)seitenLänge / 2) - 0.5f;

        while (Häuser!= gebauteHäuser)
        {
            bool setztHotSpot = false;
            foreach (int i in HotSpotsArray)
            {
                if (i == gebauteHäuser)//an dieser Position ist bereits ein HotSpot
                {
                    setztHotSpot = true;
                }
            }

            if (setztHotSpot == true)//setzte hot stop ohne person
            {
                Instantiate(HotSpot, new Vector3(xPosition, yPosition, 1f), new Quaternion(0f, 0f, 0f, 0f));
            }
            else//setzt Haus mit person
            {
                GameObject gameObjectDerPerson = NormalePerson;
                personenListe.Add(gameObjectDerPerson);
                Instantiate(gameObjectDerPerson, new Vector3(xPosition, yPosition, 1f), new Quaternion(0f, 0f, 0f, 0f));
                Instantiate(Haus, new Vector3(xPosition, yPosition, 1f), new Quaternion(0f, 0f, 0f, 0f));
            }
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
