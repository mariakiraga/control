using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    // wynikowa lista waypointów
    public List<Vector3> waypoints { get; private set; }

    // zmienna przechowująca odwołanie do LevelManagera
    private level lm;



    /// <summary>
    /// Tworzy listę waypointów dla potworów
    /// </summary>
    public void BuildWaypoints()
    {
        waypoints = new List<Vector3>();
        lm = FindObjectOfType<level>();

        // współrzędne sprawdzanego obecnie kafelka
        // ZADANIE: zmienić na współrzędne portalu!
        int x_current = 0;
        int y_current = 0;

        // współrzędne poprzednio sprawdzanego kafelka
        int x_previous = -1;
        int y_previous = -1;

        // stałe opisujące kierunki
        const int GORA = 0;
        const int PRAWA = 1;
        const int DOL = 2;
        const int LEWA = 3;

        // zielony kafelek ma typ 0 (żeby uniknąć magicznych liczb w kodzie)
        const int ZIELONY = 0;

        // kody kierunków, żeby wyświetlanie 
        // w Debug.Log wypisywało kierunki a nie liczby
        // nieważne dla działania algorytmu
        var kierunki = new Dictionary<int, string>();
        kierunki.Add(0, "Góra");
        kierunki.Add(1, "Prawa");
        kierunki.Add(2, "Dół");
        kierunki.Add(3, "Lewa");


        // wielkość mapy
        int mapX = lm.mapX - 1;
        int mapY = lm.mapY - 1;

        // tablica przechowująca współrzędne sąsiednich pól
        Point[] sasiedzi = new Point[4];

        // czy znaleźliśmy nowy kafelek?
        bool znaleziony = true;

        // wykonujemy pętlę dopóki jesteśmy w stanie znaleźć następny zielony kafelek
        while (znaleziony)
        {
            // na razie nie znaleźliśmy
            znaleziony = false;

            // Dodajemy współrzędne środka obecnego kafelka do listy waypointów
            waypoints.Add(lm.Kafelki[new Point(x_current, y_current)].Srodek);


            // "zerujemy" sąsiadów - na początek każdy otrzymuje 
            // współrzędne (-1, -1), które oznaczają, że jest poza planszą
            for (int i = 0; i < 4; i++)
            {
                sasiedzi[i] = new Point(-1, -1);
            }


            // zapełniamy listę sąsadów współrzędnymi kafelków
            // sąsaidujących z naszym obecnym kafelkiem
            // pod warunkiem, że nie są one poza planszą
            if (y_current < mapY)
            {
                sasiedzi[GORA] = new Point(x_current, y_current + 1);
            }

            if (y_current > 0)
            {
                sasiedzi[DOL] = new Point(x_current, y_current - 1);
            }

            if (x_current < mapX)
            {
                sasiedzi[PRAWA] = new Point(x_current + 1, y_current);
            }

            if (x_current > 0)
            {
                sasiedzi[LEWA] = new Point(x_current - 1, y_current);
            }

            // sprawdzamy po kolei każdego sąsiada
            for (int i = 0; i < 4; i++)
            {
                // jeśli sąsiad nie jest poza planszą...
                if (sasiedzi[i].x != -1)
                {
                    // ...sprawdzamy czy dany sąsiad jest zielony...
                    if (lm.Kafelki[sasiedzi[i]].Type == ZIELONY)
                    {
                        // ...i sprawdzamy czy nie jest to nasz poprzedni kafelek
                        if (sasiedzi[i].x == x_previous && sasiedzi[i].y == y_previous)
                        {
                            // jeśli tak, to przeskakujemy do następnego sąsiada
                            print("Pomijam kafelek " + kierunki[i] + " - już tam byliśmy");
                            continue;
                        }

                        // jeśli nie...
                        print("Znalazłem zielony na pozycji " + kierunki[i]);

                        // ...zapisujemy obecne współrzędne jako stare...
                        x_previous = x_current;
                        y_previous = y_current;

                        // ... ustawiamy współrzędne znalezionego kafelka jako "current"...
                        x_current = sasiedzi[i].x;
                        y_current = sasiedzi[i].y;

                        // ... i ustawiamy flagę, że znaleźliśmy nowy kafelek!
                        znaleziony = true;

                        // nie musimy dalej kontynuować szukania odpowiedniego sąsiada
                        break;
                    }
                }
                // jeśli sąsiad jest poza planszą, to go nie sprawdzamy
                else
                {
                    print("Pomijam kafelek " + kierunki[i] + " - poza planszą");
                }
            }
        }

        // wyświetlamy znalezione waypointy
        //for (int i = 0; i < waypoints.Count; i++)
        //{
        //    print(waypoints[i]);
        //}
    }
}