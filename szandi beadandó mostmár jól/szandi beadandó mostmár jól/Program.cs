using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;

static int tavolsag(Hely c, Hely d)
{
    int a = Math.Abs(c.x - d.x);
    int b = Math.Abs(c.y - d.y);
    return a + b;

}
var map = new Dictionary<int, Hely>();
map.Add(0, new Hely(456, 320));
map.Add(1, new Hely(228, 0));
map.Add(2, new Hely(912, 0));
map.Add(3, new Hely(0, 80));
map.Add(4, new Hely(114, 80));
map.Add(5, new Hely(570, 160));
map.Add(6, new Hely(792, 160));
map.Add(7, new Hely(342, 240));
map.Add(8, new Hely(684, 240));
map.Add(9, new Hely(570, 400));
map.Add(10, new Hely(912, 400));
map.Add(11, new Hely(114, 480));
map.Add(12, new Hely(228, 480));
map.Add(13, new Hely(342, 560));
map.Add(14, new Hely(684, 560));
map.Add(15, new Hely(0, 640));
map.Add(16, new Hely(798, 640));
int[] sorrrend = new int[map.Count];
Random rnd = new Random();
for (int i = 0; i < sorrrend.Length; i++)
{
    sorrrend[i] = i;
}
sorrrend = sorrrend.OrderBy(x => rnd.Next()).ToArray();

int varosoksz=16;
int futar = 4;
List<int> bejarhato = new List<int>();
if (varosoksz % futar != 0)
{
    decimal d = varosoksz / futar;
    int a = Convert.ToInt32(Math.Truncate(d));

    int b = futar * a;
    int c = varosoksz - b;
    for (int i = 0; i < a - c; i++)
    {
        bejarhato.Add(a);
    }
    for (int i = 0; i < c; i++)
    {
        bejarhato.Add(a + 1);
    }
}
else
{
    for (int i = 0; i < varosoksz / futar; i++)
    {

        bejarhato.Add(varosoksz / futar);
    }
}
//szomszed2(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
//tabukeres(bejarhato.ToArray(), 35, map);
int z = eredmeny(new int[] { 8,6,2,5,7,1,4,3 }, map, new int[] { 4,4 });
Console.WriteLine($"A végeredmeny elméletileg 3104 = {z}");


static int eredmeny(int[] sorrend, Dictionary<int, Hely> map, int[] bejar) {

    int osz = 0;
    int osz2 = 0;
    int szam = 0;
    Hely bazis = map[0];
    List<Hely> atmenet = new List<Hely>();
    foreach (var i in sorrend)
    {
        atmenet.Add(map[i]);
    }
    foreach (var i in bejar)
    {
        osz = tavolsag(bazis, atmenet[0]);
        for (int j = 0; j < i; j++)
        {
            if (j == i - 1)                     //utolsó pont mindig a bázis
            {
                Console.WriteLine($"i= {i}, j= {j}");
                osz += tavolsag(atmenet[0], bazis);
                atmenet.Remove(atmenet[0]);
            }
            else
            {                              //minden más pont 
                osz += tavolsag(atmenet[0], atmenet[1]);
                atmenet.Remove(atmenet[0]);
            }
        }
        Console.WriteLine($"osz {osz} bejar {i}");
        osz2 += osz;
    }
    return osz2;
}
/*static List<int[]> szomszedok(int[] mostanisor)
{

    List<int[]> szomszed = new List<int[]>();
    for (int i = 0; i < mostanisor.Length; i++)
    {
        int seged;
        for (int j = i + 1; j < mostanisor.Length; j++)
        {
            seged = mostanisor[i];
            mostanisor[i] = mostanisor[j];
            mostanisor[j] = seged;
            szomszed.Add(mostanisor.ToArray());
            seged = mostanisor[i];
            mostanisor[i] = mostanisor[j];
            mostanisor[j] = seged;
        }
    }
    return szomszed;
}*/

static int[,] szomszed2(int[] v) { 

int[] mostanisor = v;

int l = mostanisor.Length + 1;
if (mostanisor.Length % 2 == 0)
{

    l = (mostanisor.Length / 2) * l;
    l = l / 2;
}
else
{
    l = mostanisor.Length / 2 * l;
    l += (mostanisor.Length / 2) + 1;
    l = l / 2;
}



int[,] matrix = new int[l, mostanisor.Length];
for (int i = 0; i < matrix.GetLength(0); i++)
{
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
        matrix[i, j] = mostanisor[j];
    }
}
int seged2;
int counter1 = 0;
int counter2 = 1;
for (int i = 0; i < matrix.GetLength(0); i++)
{

    seged2 = matrix[i, counter1];
    matrix[i, counter1] = matrix[i, counter2];
    matrix[i, counter2] = seged2;
    counter2++;
    if (counter2 == matrix.GetLength(1))
    {
        counter1++;
        counter2 = counter1 + 1;
    }
}
    List<int[]> proba = new List<int[]>();
    int[] kell = new int[matrix.GetLength(1)];
    //Console.WriteLine("Ide vlami mert nem tiszta a kod");
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            kell[j] = matrix[i,j];
      //      Console.Write($"{kell[j]},");
        }
        //Console.WriteLine();
        proba.Add(kell.ToArray());
    }
    return matrix;
}

static bool keres1(int[] alap, List<int[]> tabuk)
{

    HashSet<int[]> ht = new HashSet<int[]>();
    foreach (var i in tabuk)
    {
        ht.Add(i);
    }
    return ht.Contains(alap);
}
static bool keres2(int[] alap, List<int[]> tabuk)
{

    return tabuk.Contains(alap);

    /*static List<int[]> szomszedok(int[] mostanisor) {

        List<int[]> szomszed = new List<int[]>();
        for (int i = 0; i < mostanisor.Length; i++)
        {
            int seged;
            for (int j = i+1; j < mostanisor.Length; j++)
            {
                seged = mostanisor[i];
                mostanisor[i] = mostanisor[j];
                mostanisor[j] = seged;
                szomszed.Add(mostanisor.ToArray());
                seged = mostanisor[i];
                mostanisor[i] = mostanisor[j];
                mostanisor[j] = seged;
            }
        }
        return szomszed;
    */
}


    static void tabukeres(int[] bejar, int tabumax, Dictionary<int, Hely> maping)
    {
        int iteracio = 100;
        List<int[]> tabu = new List<int[]>();
        //int tabulim = 10;
        int[] mostanisor = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        for (int i = 0; i < iteracio; i++)
        {
            List<int[]> ar = new List<int[]>();
            int[,] szomszedokm = szomszed2(mostanisor);
            for (int k = 0; k < szomszedokm.GetLength(0); k++)
            {
                int[] szomszed = new int[szomszedokm.GetLength(1)];
                for (int f = 0; f < szomszedokm.GetLength(1); f++)
                {
                    szomszed[f] = szomszedokm[k, f];
                }

                for (int j = 0; j < szomszedokm.GetLength(0); j++)
                {
                    int max = eredmeny(mostanisor, maping, bejar);
                    if (keres1(szomszed, tabu))
                    {
                        int a = eredmeny(szomszed, maping, bejar);
                        if (a < max)
                        {
                            max = a;
                            mostanisor = szomszed;
                        }
                    }
                }
            }

            tabu.Add(mostanisor.ToArray());
            if (tabu.Count > tabumax)
            {
                for (int j = 0; j > tabumax; j++)
                {
                    tabu.Remove(tabu[0]);
                }
            }
        }

    }


/*static void tabu(int iteracio, int[] sorrrend, Dictionary<int, Hely> map, int[] hatarid, int[] bejar)
{


List<int[]> tabu = new List<int[]>();
    List<int[]> list = new List<int[]>();
int tabumax = 35;
for (int i = 0; i < iteracio; i++)
{
    List<int[]> szomszed = new List<int[]>();
    int[] mostanisor = sorrrend;
    list = szomszedok(mostanisor);
    for (int j = 0; j < szomszed.Count; j++)
    {
        if (keres1(szomszed[j], tabu))
        {

            szomszed.Remove(szomszed[j]);
        }
    }
    int max = eredmeny(mostanisor,map, bejar );
    foreach (var j in szomszed)
    {
        int a = eredmeny(j, map, bejar);
        if (a < max)
        {
            max = a;
            mostanisor = j;
        }
    }
    tabu.Add(mostanisor.ToArray());
    if (tabu.Count > tabumax)
    {
        int a = tabu.Count-tabumax;
        for (int j = 0; j < a; j++)
        {
            tabu.Remove(tabu[0]);
        }
    }

}
}*/



struct Hely
{
    public int x;
    public int y;

    public Hely(int x, int y)
    {
        this.y = y;
        this.x = x;
    }
    public string stringger()
    {
        return $"{this.x.ToString()},{this.y.ToString()}";

    }

}

