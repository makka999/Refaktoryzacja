using io2;
using System.Diagnostics;
using System.Xml.Linq;

float priceSum;

Product fasola = new();
fasola.Name = "Fasola";
fasola.Category = "Warzywo";
fasola.Price = 20.123f;
fasola.Weight = 10;

Product banan = new();
banan.Name = "Banan";
banan.Category = "Owoc";
banan.Price = 20.99f;
banan.Weight = 20;

Product kiwi = new();
kiwi.Name = "Kiwi";
kiwi.Category = "Owoc";
kiwi.Price = 123;
kiwi.Weight = 10;

UserCart FranekKaczakCart = new();
FranekKaczakCart.FirstName = "Framek";
FranekKaczakCart.LastName = "Kączak";
FranekKaczakCart.CartProduct[0] = fasola;
FranekKaczakCart.CartProduct[1] = banan;
FranekKaczakCart.CartProduct[2] = kiwi;

SumPriceProductWithRule(FranekKaczakCart);
SumPriceProductWithRule(FranekKaczakCart, true, 200, true, 123, true);
SumPriceProductWithRule(FranekKaczakCart, true, 200, true, 123, false);

float DiscountsPriceByPriceSum()
{
    if (priceSum > 5000) priceSum = (float)(0.7f * priceSum); 
    if (priceSum > 1000) priceSum = (float)(0.8f * priceSum); 
    if (priceSum > 500) priceSum = (float)(0.85f * priceSum);
    if (priceSum > 100) priceSum = (float)(0.9f * priceSum);
    return priceSum;
}

float DiscountsPriceByRole(UserCart user)
{
    if ((user.Rola == "VIP") || (user.Rola == "Admin")) priceSum = (int)(0.9f * priceSum);
    return priceSum;
}

int SumWeightProduct(UserCart user)
{
    int WagaSuma = user.CartProduct[0].Weight;
    if (user.CartProduct[1] != null) WagaSuma = WagaSuma + user.CartProduct[1].Weight;
    if (user.CartProduct[1] != null) WagaSuma = WagaSuma + user.CartProduct[2].Weight;
    return WagaSuma;
}

bool SameCategory(UserCart user, string categoty)
{
    return (user.CartProduct[0].Category.ToLower() == categoty.ToLower())
        && (user.CartProduct[1] == null || (user.CartProduct[1] != null
        && user.CartProduct[1].Category.ToLower() == categoty))
        && (user.CartProduct[2] == null || (user.CartProduct[2] != null
        && user.CartProduct[2].Category.ToLower() == categoty.ToLower()));
}

float ProductWeightMatters(UserCart user)
{
    if ((SumWeightProduct(user) > 1000) && (SameCategory(user, "Owoc"))) priceSum += 45;
    if ((SumWeightProduct(user) > 1000) && (SameCategory(user, "Warzywo"))) priceSum += 35;
    if (SumWeightProduct(user) > 1000) priceSum += 25; 
    if (SumWeightProduct(user) > 500) priceSum += 15;
    if (SumWeightProduct(user) > 100) priceSum += 10;
    if (SumWeightProduct(user) > 50) priceSum += 5;
    if (SumWeightProduct(user) > 15) priceSum += 2;
    return priceSum;
}

float SumPriceProductWithRule(UserCart user, bool ifExtraPrice = false, int extraPriceMeter = 0, bool ifMargin = false, int priceMargin = 0, bool ifProductWeightMatters = true)
{
    if (user != null)
    {
        if (user.CartProduct[0] == null) return 0;
        else
        {
            priceSum = user.CartProduct[0].Price;
            if (user.CartProduct[1] != null) priceSum = user.CartProduct[0].Price + user.CartProduct[1].Price;
            if (user.CartProduct[1] != null) priceSum = priceSum + user.CartProduct[1].Price + user.CartProduct[2].Price;
            if ((user.CartProduct[0].Price - priceSum) < float.Epsilon) priceSum = float.Epsilon;
            Console.WriteLine("Cena produktów:" + priceSum);
            DiscountsPriceByPriceSum();
            Console.WriteLine("Cena produktów po zniznkach liczonych od wielkosci zamowienia " + priceSum);
            DiscountsPriceByRole(user);
            Console.WriteLine("Cena produktów po znizkach ze wzgledu na role " + priceSum);
            if (ifProductWeightMatters) ProductWeightMatters(user);
            Console.WriteLine("Cena produktów po wpływie wagi " + priceSum);
            if (ifExtraPrice) priceSum += extraPriceMeter;
            Console.WriteLine("Cena produktów po doliczeniu dopłaty " + priceSum);
            if (ifMargin)
            {
                priceSum += priceMargin;
                if (priceMargin > 100) Console.WriteLine("Brawo, podwyżka się należy!");
            }
            Console.WriteLine("Cena produktów końcowa " + priceSum);
            return priceSum;
        }
    }
    else return 0;
}





