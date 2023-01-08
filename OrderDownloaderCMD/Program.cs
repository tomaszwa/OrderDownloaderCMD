using BaseLinkerApi;
using System.Text;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BaseLinkerApi.Common;
using BaseLinkerApi.Common.JsonConverters;
using RateLimiter;
using BaseLinkerApi.Requests.Orders;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using ADODB;
using System.Security.Cryptography.X509Certificates;
using InsERT;
using System.Linq;

namespace BaseLinkerOrderDownloader;


class OrderDownloader
{
    [STAThread]
    public static void Main(string[] args)
    {   
        var subiekt = SgtConnector();
        var orderJson = ApiConnector();
        var kontrhanet = KontrahentAdder(subiekt,orderJson);
        Console.WriteLine(orderJson);
    }
    public static Order ApiConnector()
    {
        var httpClient = new HttpClient();
        var connected = new BaseLinkerApiClient(httpClient, "2001423-2006490-YUNDJTP4BBH14NRYKTOFU7VOFRVQQBHUUA7ZWBB6UCG0KVHAXAE13LLOPHDC4IMZ");
        var request = new GetOrders()
        {
            OrderId = 58292809
        };
        var result = connected.SendAsync(request).GetAwaiter().GetResult().Orders
            .Select(o => new { Order = o, Product = o.Products.First() })
            .Select(x => new Order(x.Order.DeliveryFullname, x.Order.DeliveryAddress, x.Product.Name, 
                x.Product.PriceBrutto, x.Product.TaxRate, x.Product.Quantity));
        return result.First(); 
    }


    public class Order
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductTax { get; set; }
        public int ProductQuantity { get; set; }
        public Order(string name, string adress, string productName, double productPrice, double productTax, int productQuantity)
        {
            Name = name;
            Adress = adress;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductTax = productTax;
            ProductQuantity = productQuantity;
        }

    }



    public static Subiekt SgtConnector()
    {
        var gt = new GT();
        Subiekt sgt;
        gt.Produkt = ProduktEnum.gtaProduktSubiekt;
        gt.Serwer = "DESKTOP-PDDM77M\\INSERTGT";
        gt.Baza = "Testowy";
        gt.Autentykacja = AutentykacjaEnum.gtaAutentykacjaWindows;
        gt.Uzytkownik = "sa";
        gt.Uzytkownik = "";
        gt.Operator = "Szef";
        gt.OperatorHaslo = "";
        sgt = (Subiekt)gt.Uruchom((int)UruchomDopasujEnum.gtaUruchomDopasuj, (int)UruchomEnum.gtaUruchomNieArchiwizujPrzyZamykaniu);
        sgt.Okno.Widoczne = true;

        return sgt;
    }

    public static Kontrahent KontrahentAdder(Subiekt sgt, Order order)
    {
        Kontrahent kh;

        kh = sgt.Kontrahenci.Dodaj();

        kh.Symbol = order.Name;
        kh.Nazwa = order.Name;
        kh.NazwaPelna = order.Name;
        kh.Ulica = order.Adress;
        kh.Miejscowosc = order.Name;

        kh.Zapisz();
        kh.Zamknij();

        return kh;


    }

}










