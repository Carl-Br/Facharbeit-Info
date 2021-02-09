
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Person : MonoBehaviour
{
    Vector2 Zielposition;
    Vector2 currentPosition;
    public Vector2 home;
    public Vector2 vorDemHausPosition = new Vector2(0, 0);
    public List<Vector2> ZielListe; //Eine Liste von Zielpositionen, die die Person nacheinander abarbeiten soll.
    // Start is called before the first frame update
    void Start()
    {
        start();
        home = new Vector2(transform.position.x, transform.position.y);
        vorDemHausPosition = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);
        addZiel(vorDemHausPosition);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        if (ZielListe.Count >= 1)//Wenn es ein ziel gibt, außer das Ziel aus dem Haus zu gehen
        {
            Zielposition = ZielListe[0];//wähle das erste Ziel aus der Liste aus
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Zielposition, 0.95f * Time.deltaTime);
            if(currentPosition == Zielposition)//wenn an der Zielposition angekommen, dann aus liste entfernen
            {
                ZielListe.RemoveAt(0);//lösche erstes Element
                //ZielListe.
            }
        }

    }
    public void addZiel(Vector2 ziel)
    {
        ZielListe.Add(ziel);
    }
    private void start()
    {
        ZielListe = new List<Vector2>();
        Debug.Log(ZielListe.Count);
    }
    
    

}
