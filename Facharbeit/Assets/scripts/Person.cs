
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Person : MonoBehaviour
{
    Vector2 Zielposition;
    Vector2 currentPosition;
    Vector2 home;
    Vector2 vorDemHausPosition;
    public float geschwindigkeit = 0.95f;
    List<Vector2> ZielListe = new List<Vector2>(); //Eine Liste von Zielpositionen, die die Person nacheinander abarbeiten soll.
    
    // Start is called before the first frame update

    void Start()
    {
        vorDemHausPosition = new Vector2(0, 0);
        home = new Vector2(transform.position.x, transform.position.y);
        vorDemHausPosition = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);

        
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        Bewegen();
    }
    private void Bewegen()
    {
        if (ZielListe.Count >= 1)//Wenn es ein ziel gibt, außer das Ziel aus dem Haus zu gehen
        {
            Zielposition = ZielListe[0];//wähle das erste Ziel aus der Liste aus
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Zielposition, geschwindigkeit * Time.deltaTime);
            if (currentPosition == Zielposition)//wenn an der Zielposition angekommen, dann aus liste entfernen
            {
                ZielListe.RemoveAt(0);//lösche erstes Element
                
            }
        }
    }
    
    
    public void Starts(List<GameObject> hotspotListe, int hotspots)
    {
        System.Random rnd = new System.Random();
        
        ZielListe.Add(vorDemHausPosition);
        for(int i = 0;i <= hotspots; i++)
        {
            GameObject randomHotSpot = hotspotListe[rnd.Next(0, hotspotListe.Count-1)];//wählt einen zufälligen hotspot, wo die gerade ausgewählte person hin gehen soll
            ZielListe.Add(new Vector2(randomHotSpot.transform.position.x, randomHotSpot.transform.position.y));
        }
        ZielListe.Add(vorDemHausPosition);
        ZielListe.Add(home);

    }


    
      
}
