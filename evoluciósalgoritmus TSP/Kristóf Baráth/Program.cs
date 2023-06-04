Console.WriteLine("Adja meg a városok számát :");
int varosoksz =Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Adja mega a futárok számát");
int futar = Convert.ToInt32(Console.ReadLine());



var map = new Dictionary<int, Hely>();
for (int i = 0; i < varosoksz+1; i++)
{
    Random r = new Random();
    map.Add(i, new Hely(r.Next(0, 1000), r.Next(0, 1000)));
}
int[] kezdetisor = new int[map.Count];
Random rnd = new Random();
for (int i = 1; i < kezdetisor.Length; i++)
{
    kezdetisor[i] = i;
}
kezdetisor = kezdetisor.OrderBy(x => rnd.Next()).ToArray();
List<int> felosztas = new List<int>();
if (varosoksz % futar != 0)
{
    decimal d = varosoksz / futar;
    int a = Convert.ToInt32(Math.Truncate(d));

    int b = futar * a;
    int c = varosoksz - b;
    for (int i = 0; i < a - c; i++)
    {
        felosztas.Add(a);
    }
    for (int i = 0; i < c; i++)
    {
        felosztas.Add(a + 1);
    }
}
else
{
    for (int i = 0; i < varosoksz / futar; i++)
    {

        felosztas.Add(varosoksz / futar);
    }
}
static int tavolsag(Hely c, Hely d)
{
    int a = Math.Abs(c.x - d.x);
    int b = Math.Abs(c.y - d.y);
    return a + b;

}
static int eredmeny(int[] kezdetisor, Dictionary<int, Hely> map, int[] felosztas)
{

    int osz = 0;
    int osz2 = 0;
    Hely bazis = map[0];
    List<Hely> atmenet = new List<Hely>();
    foreach (var i in kezdetisor)
    {
        atmenet.Add(map[i]);
    }
    foreach (var i in felosztas)
    {
        osz = tavolsag(bazis, atmenet[0]);
        for (int j = 0; j < i; j++)
        {
            if (j == i - 1)                     //utolsó pont mindig a bázis
            {
                osz += tavolsag(atmenet[0], bazis);
                atmenet.Remove(atmenet[0]);
            }
            else
            {                              //minden más pont 
                osz += tavolsag(atmenet[0], atmenet[1]);
                atmenet.Remove(atmenet[0]);
            }
        }
        osz2 += osz;
    }
    return osz2;
}
static int[,] szomszed(int[] v)
{

    int[] mostanisor = v;
    int osz = 0;
    for (int i = 1; i < mostanisor.Length; i++)
    {
        osz += i;

    }
    int[,] matrix = new int[osz, mostanisor.Length];
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
    return matrix;
}
static void szimhut(int[] beosztas, Dictionary<int, Hely> map, int[] kezdetisor)
{

    int iter = 500; // ezen növelhj ha nagyobb menyiségű városhoz kell a számitás
    Random rnd = new Random();
    int[,] matrix;
    int[] sor = kezdetisor;
    int vegered = 0;
    for (int i = iter; i > 1; i--)
    {
        const double k = 1.3807e-16; //BOLTZMAN
        matrix = szomszed(sor);
        int[] kell = new int[matrix.GetLength(1)];
        int rand = rnd.Next(0, matrix.GetLength(0));
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            kell[j] = matrix[rand, j];
        }
        int ered1 = eredmeny(kell, map, beosztas);
        int ered2 = eredmeny(sor, map, beosztas);
        int osz = ered1 - ered2;
        if (osz < 0)
        {
            sor = kell;
            vegered = ered1;

        }
        else
        {
            double z;
            double prob;
            z = -1 * (osz / (k * i));
            prob = Math.Exp(z);
            prob = prob * 100;
            int a = Convert.ToInt32(Math.Truncate(prob));
            if (rnd.Next(0, 100) < a)
            {
                sor = kell;
                vegered = ered1;
            }

        }
    }
    Console.WriteLine("Megoldás");
    foreach (var i in sor)
    {
        Console.Write($"{i+1},");
    }
    Console.WriteLine($"Az eredmény a sorhotz összesitve : {vegered}");
}

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