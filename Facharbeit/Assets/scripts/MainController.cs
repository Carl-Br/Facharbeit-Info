﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class MainController : MonoBehaviour
{
    public GameObject Welt;
    public GameObject Haus;
    public GameObject Hotspot;
    public GameObject NormalePerson;
    public double Ansteckungsradius= 4.5;
    public int hotspots = 3;//mindestens 1 (darf nicht null sein)=
    public int personen = 30; //zwischen 30 und 2000
    public List<GameObject> personenListe;
    public List<GameObject> hotspotListe;
    public float geschwindigkeit = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        personenListe = new List<GameObject>();
        hotspotListe = new List<GameObject>();
        erstelleWelt();
        Debug.Log("Es exesieren "+personenListe.Count+" Personen");
        StartCoroutine(starteSimulation());
    }

    // Update is called once per frame
    void Update()
    {
        geschwindigkeitsEinsteller();
    }
    private void geschwindigkeitsEinsteller()
    {
        foreach (GameObject gameObject in personenListe)
        {
            Person p = gameObject.GetComponent<Person>();//Holt sich das c# script von dem gerade ausgewähltem Personenobject
            p.geschwindigkeit = geschwindigkeit;
        }
    }
    IEnumerator starteSimulation()//enumerator, damit gewartet wird bis alle Objekte erstellt wurden ( Zeile 44 gehört dazu)
    {
        
        Debug.Log("starte Simulation");
        foreach (GameObject gameObject in personenListe)
        {
            Person p = gameObject.GetComponent<Person>();//Holt sich das c# script von dem gerade ausgewähltem Personenobject
            p.Starts(hotspotListe,3);
            yield return new WaitForEndOfFrame();
        }
       
    }

    //Welt Bereich
    private void erstelleWelt()
    {
        //erstellt die Fläche
        int Häuser = personen + hotspots;//ANzahl aller Gebäude
        double seitenLänge = Math.Sqrt(personen+hotspots);
        seitenLänge = Math.Ceiling(seitenLänge);//rundet seitenLänge auf
        Welt.transform.localScale = new Vector3((float)seitenLänge+1, (float)seitenLänge+1,1f);//ändere die Welt größe (das +1 ist der rand)


        //legt fest an welchen stellen überall HotSpots sein sollen
        System.Random rnd = new Random();
        int randomZahl1 = (int)Math.Round(seitenLänge / 5);
        int[] HotSpotsArray = new int[hotspots];
        for(int i =0;i<hotspots;i++)
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
            //wenn an der Stelle noch kein Hotspot ist setzte dort einen HotSpot
            HotSpotsArray[i] = randomZahl;
        }


        //setzte Häuser und personen
        int HotSpotPositions = (int)Math.Round((double)Häuser / hotspots);
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

            if (setztHotSpot == true)//setzte hotspot ohne person
            {
                GameObject gameObjectDesHotspots = Instantiate(Hotspot, new Vector3(xPosition, yPosition, 1f), new Quaternion(0f, 0f, 0f, 0f));
                hotspotListe.Add(gameObjectDesHotspots);
            }
            else//setzt Haus mit person
            {
                GameObject gameObjectDerPerson = Instantiate(NormalePerson, new Vector3(xPosition, yPosition, 1f), new Quaternion(0f, 0f, 0f, 0f));
                personenListe.Add(gameObjectDerPerson);
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
