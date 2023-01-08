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

namespace BaseLinkerOrderDownloader;


class OrderDownloader
{
    [STAThread]
    static void Main(string[] args)
    {
        InsERT.GT gt = new InsERT.GT();
        InsERT.Subiekt sgt;
        gt.Produkt = InsERT.ProduktEnum.gtaProduktSubiekt;
        gt.Serwer = "DESKTOP-PDDM77M\\INSERTGT";
        gt.Baza = "Testowy";
        gt.Autentykacja = InsERT.AutentykacjaEnum.gtaAutentykacjaWindows;
        gt.Uzytkownik = "sa";
        gt.Uzytkownik = "";
        gt.Operator = "Szef";
        gt.OperatorHaslo = "";
        sgt = (InsERT.Subiekt)gt.Uruchom((int)InsERT.UruchomDopasujEnum.gtaUruchomDopasuj, (int)InsERT.UruchomEnum.gtaUruchomNieArchiwizujPrzyZamykaniu);
        sgt.Okno.Widoczne = true;
    }

        //var httpClient = new HttpClient();
        //var connected = new BaseLinkerApiClient(httpClient, "2001423-2006490-YUNDJTP4BBH14NRYKTOFU7VOFRVQQBHUUA7ZWBB6UCG0KVHAXAE13LLOPHDC4IMZ");
        //var request = new GetOrders()
        //{
        //    OrderId = 58292809
        //};
        //var result = connected.SendAsync(request).GetAwaiter().GetResult().Orders;


}





//public class Connector
//{
//    static int Second()
//    {
//        ADODB.Connection conDatabase = new ADODB.Connection();
//        return 0;
//    }
//    public void _Connection.Open(string ConnectionString, string UserID, string Password, int Options)





//}






