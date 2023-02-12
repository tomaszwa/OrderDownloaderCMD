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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using static BaseLinkerApi.Requests.ProductCatalog.GetInventoryProductsPrices;
using static BaseLinkerApi.Requests.Orders.GetOrderStatusList;
using OrderDownloaderCMD;

namespace BaseLinkerOrderDownloader;


class OrderDownloader
{
    [STAThread]
    public static void Main(string[] args)
    {   
        var subiekt = SgtConnector();
        var orders = ApiConnector();
        if (orders != null && orders.Any())
        {
            ZKAdder(subiekt, orders);
            Console.WriteLine("Wysłano do sfery, naura.");
            
        }
        else
        {
            Console.WriteLine("Nie ma takiego status i/lub zamówienia elo.");
        }
        Console.ReadKey();
    }

    public static IEnumerable<Order> ApiConnector()
    {
        var httpClient = new HttpClient();
        var connected = new BaseLinkerApiClient(httpClient, "2001423-2006490-YUNDJTP4BBH14NRYKTOFU7VOFRVQQBHUUA7ZWBB6UCG0KVHAXAE13LLOPHDC4IMZ");
        Console.WriteLine("Podaj id statusu z którego pobrać zamówienia:\n");
        var input = Console.ReadLine();
        if (int.TryParse(input, out int statusId))
        {
            var request = new GetOrders()
            {
                StatusId = statusId
            };
            var result = connected.SendAsync(request).GetAwaiter().GetResult().Orders
                .Select(o => new { Order = o, o.Products })
                .Select(x => new Order(x.Order.OrderId, x.Order.DeliveryFullname, x.Order.DeliveryAddress, x.Order.DeliveryCity,
                    x.Products.Select(p => new Product(p.Name, p.PriceBrutto, p.TaxRate, p.Quantity))));

            return result;
        }

        return null;
    }


    public class Order
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string DeliveryAdress { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public Order(int orderId, string name, string adress, string deliveryCity,IEnumerable<Product> products)
        {
            OrderId = orderId;
            Name = name;
            Adress = adress;
            DeliveryAdress = deliveryCity;            
            Products = products;

        }

    }

    public class Product
    {
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductTax { get; set; }
        public int ProductQuantity { get; set; }
        public Product(string productName, double productPrice, double productTax, int productQuantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductTax = productTax;
            ProductQuantity = productQuantity;
        }
    }



    public static Subiekt SgtConnector()
    {

        var configPath = File.ReadAllText("./Config.json");
        var config = JsonSerializer.Deserialize<ERPConnectionConfig>(configPath, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
        var gt = new GT()
        {
            Produkt = ProduktEnum.gtaProduktSubiekt,
            Serwer = config.Server,
            Baza = config.Database,
            Autentykacja = AutentykacjaEnum.gtaAutentykacjaWindows,
            Uzytkownik = config.User,
            UzytkownikHaslo = config.UserPassword,
            Operator = config.Operator,
            OperatorHaslo = config.OperatorPassword,
        };
        Subiekt sgt;
        sgt = (Subiekt)gt.Uruchom((int)UruchomDopasujEnum.gtaUruchomDopasuj, (int)UruchomEnum.gtaUruchomNieArchiwizujPrzyZamykaniu);
        sgt.Okno.Widoczne = true;

        return sgt;
    }

    public static Kontrahent KontrahentAdder(Subiekt sgt, Order order)
    {   

        Kontrahent kh;
        bool check = sgt.KontrahenciManager.Istnieje(order.Name);
        if (!check)
        {
            kh = sgt.KontrahenciManager.DodajKontrahenta();
            kh.Symbol = order.Name;
            kh.Nazwa = order.Name;
            kh.NazwaPelna = order.Name;
            kh.Ulica = order.Adress;
            kh.Miejscowosc = order.Name;

            kh.Zapisz();

            return kh;
        }       

        kh = sgt.KontrahenciManager.WczytajKontrahenta(order.Name);
        return kh; 
    }

    public static void ZKAdder(Subiekt sgt, IEnumerable<Order> orders)
    {
        foreach (var order in orders)
        {
            SuDokument dokument = sgt.Dokumenty.Dodaj(SubiektDokumentEnum.gtaSubiektDokumentZK);
            dokument.KontrahentId = KontrahentAdder(sgt, order);
            dokument.NumerOryginalny = order.OrderId;
            foreach (var product in order.Products)
            {
                SuPozycja tr = dokument.Pozycje.DodajUslugeJednorazowa();
                tr.UslJednNazwa = product.ProductName;
                tr.CenaBruttoPrzedRabatem = product.ProductPrice;
                tr.VatId = product.ProductTax switch
                {
                    23 => VatRate.Standard23,
                    8 => VatRate.Standard8,
                    4 => VatRate.Standard0,
                    7 => VatRate.Standard7,
                    _ => throw new NotSupportedException()
                };
                tr.IloscJm = product.ProductQuantity;
                tr.Jm = "szt.";
                
                dokument.Zapisz();
            }            
            dokument.Zamknij();
        }
    }


    public enum VatRate
    {
        Standard7 = 2,
        Standard0 = 4,
        Standard23 = 100001,
        Standard8 = 100002,
        
    }
    
    

}










