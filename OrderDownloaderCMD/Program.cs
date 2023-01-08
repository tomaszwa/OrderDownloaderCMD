using BaseLinkerApi;
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

namespace BaseLinkerOrderDownloader;


class OrderDownloader
{
    static void Main(string[] args)
    {
        var httpClient = new HttpClient();
        var connected = new BaseLinkerApiClient(httpClient, "2001423-2006490-YUNDJTP4BBH14NRYKTOFU7VOFRVQQBHUUA7ZWBB6UCG0KVHAXAE13LLOPHDC4IMZ");
        var request = new GetOrders()
        {
            OrderId = 58292809
        };
        var result = connected.SendAsync(request).GetAwaiter().GetResult().Orders;
    }
}


class SubiektConnector
{
    [STAThread]
    static void Main(string[] args)
    {
        try
        {
            InsERT.GT gt = new InsERT.GT();
            gt.Produkt = InsERT.ProduktEnum.gtaProduktSubiekt;
            gt.Serwer = "DESKTOP-PDDM77M\\INSERTGT";
            gt.Baza = "Testowy";
            gt.Autentykacja - InsERT.AutentykacjaEnum.gtaAutentykacjaMieszana;
            gt.Uzytkownik = "sa";
            gt.Uzytkownik = "";
            gt.Operator = "Szef";
            gt.OperatorHaslo = "";

            InsERT.Subiekt subiekt = (InsERT.Subiekt)gt.Uruchom((Int32))InsERT.UruchomDopasujEnum.gtaUruchomDopasuj, (Int32)InsERT.UruchomEnum.gtaUruchomNieArchiwizujPrzyZamykaniu);
            subiekt.Okno.Widoczne = true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
        }
    }
}
   

