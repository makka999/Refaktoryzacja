
Product p1 = new Product();
p1.Na = "Fasola";
p1.sCtegoria = "Warzywo";
p1.Price = 20.123f;
p1.Waga = 10;

Product p2 = new Product();
p2.Na = "Banan";
p2.sCtegoria = "Owoc";
p2.Price = 20.99f;
p2.Waga = 20;

float priceSum = Sum(p1, p2);

bool czyP1JestOwocem = p1.sCtegoria == "Owoc";
bool czyCenaJestWiększaOd10000 = priceSum > 1000;
//bool czyOwoceSąTakieSame = p1 == p2; Nie działałało, więc porównam wszytkiepola
bool czyOwoceSąTakieSame =
    (p1.Na == p2.Na)
    && (p2.sCtegoria == p1.sCtegoria)
    && (p1.Waga == p2.Waga);

Product[] productArray = new Product[2];
productArray[0] = p1;
productArray[1] = p2;   

float Sum(Product p, Product p2)
{
    return p.Price+ p2.Price;
}

U user1 = new U();
user1.FirstName = "Framek";
user1.LastName = "Kączak";
user1.products[0] = p1;
user1.products[1] = p2;
user1.products[2] = new Product();
user1.products[2].Na = "Kiwi";
user1.products[2].sCtegoria = "Owoc";
user1.products[2].Price = 123;
user1.products[2].Waga = 10;

float sumaCenyProduktówDlaUsera = LiczSumeProduktówZKoszyka(user1);
float sumaCenyProduktówDlaUsera1 = LiczSumeProduktówZKoszyka(user1, true, 200, true, 123, true);
float sumaCenyProduktówDlaUsera2 = LiczSumeProduktówZKoszyka(user1, true, 200, true, 123, false);

float LiczSumeProduktówZKoszyka(U user, bool czyDoliczyćDodatkoweKoszty = false, int DodatkoweKoszty = 0, 
    bool czyDoliczyćMarże = false, int Marża = 0, bool czyWagaProduktówMaZnaczenieNaCene = true)
{
    if (user != null)
    {
        if (user.products[0] == null)
            return 0;
        else
        {
            float priceSum = user.products[0].Price;
            if (user.products[1] != null)
                priceSum = Sum(user.products[0], user.products[1]);
            if (user.products[1] != null)
                priceSum = priceSum + Sum(user.products[1], user.products[2]);

            if ((user.products[0].Price - priceSum) < float.Epsilon)
                priceSum = float.Epsilon;

            Console.WriteLine("Cena produktów #1 " + priceSum);

            // zniżki 
            if 
                (priceSum > 5000) priceSum = (float)(0.7f * priceSum); // dla wiekszych od 5000 30%
            else 
            if (priceSum > 1000)
                priceSum = (float)(0.8f * priceSum); // dla wiekszych od 5000 20%
            else 
            if (priceSum > 500)
                priceSum = (float)(0.85f * priceSum); // dla wiekszych od 5000 15%
            else 
            if (priceSum > 100)
                priceSum = (float)(0.9f * priceSum); // dla wiekszych od 5000 10%

            Console.WriteLine("Cena produktów #2 " + priceSum);

            if ((user.Rola == "VIP") || (user.Rola == "Admin")) // dodatkowa zniżka dla adminów lub VIP
                priceSum = (int)(0.9f * priceSum);

            Console.WriteLine("Cena produktów #3 " + priceSum);

            int WagaSuma = user.products[0].Waga;
            if (user.products[1] != null)
                WagaSuma = WagaSuma + user.products[1].Waga;
            if (user.products[1] != null)
                WagaSuma = WagaSuma + user.products[2].Waga;

            Console.WriteLine("Cena produktów #4 " + priceSum);

            if (czyWagaProduktówMaZnaczenieNaCene)
            {
                // Dodatkowe cany dla większej wagi jeżeli tru dla znaczenie ceny na wagę
                if ((WagaSuma > 1000) 
                    && (user.products[0].sCtegoria.ToLower() == "Owoc".ToLower()) 
                    && (user.products[1] == null || (user.products[1] != null 
                        && user.products[1].sCtegoria.ToLower() == "Owoc"))
                    && (user.products[2] == null || (user.products[2] != null && user.products[2].sCtegoria.ToLower() == "Owoc".ToLower()))
                    )
                    priceSum += 45; // jeżeli to są tylko owoce to 45 dodatkowo,
                else 
                if ((WagaSuma > 1000)
                    && (user.products[0].sCtegoria.ToLower() == "Wrzywo".ToLower())
                    && (user.products[1] == null || (user.products[1] != null && user.products[1].sCtegoria.ToLower() == "Wrzywo"))
                    && (user.products[2] == null || (user.products[2] != null && user.products[2].sCtegoria.ToLower() == "Wrzywo".ToLower()))
                    )
                    priceSum += 35; // jeżeli to są tylko warzywa to 35 dodatkowo,
                else 
                if (WagaSuma > 1000)
                    priceSum += 25; // jeżeli to są tylko warzywa to 20 dodatkowo,
                else 
                if (WagaSuma > 500)
                    priceSum += 15;
                else 
                if (WagaSuma > 100)
                    priceSum += 10;
                else 
                if (WagaSuma > 50)
                    priceSum += 5;
                else 
                if (WagaSuma > 15)
                    priceSum += 2;

                Console.WriteLine("Cena produktów #5 " + priceSum);
            }

            if (czyDoliczyćDodatkoweKoszty)
            {
                priceSum += DodatkoweKoszty;
            }
            Console.WriteLine("Cena produktów #6 " + priceSum);

            if (czyDoliczyćMarże)
            {
                priceSum += Marża;
                if (Marża > 100)
                    Console.WriteLine("Brawo, podwyżka się należy!");
            }
            Console.WriteLine("Cena produktów #7 " + priceSum);

            return priceSum;
        }
    }
    else
        return 0;
}

class Product
{
    public string Na; // Nazwa
    public string sCtegoria; // Kategoria - Warzywo lub Owoc (resztę nie ogarniamy)
    public float Price; // Cena
    public int Waga; // Waga
}

class U
{
    public string FirstName;
    public string LastName;
    public string Email;
    public string Rola; // Admin, Normalny, VIP oraz Janek (ten z OwoceMorza.pl)
    public Product[] products = new Product[3]; // User może miec tylko 3 produkty, takie ustalenia z biznesem.
}

