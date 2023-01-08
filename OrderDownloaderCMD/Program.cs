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
        Console.ReadLine();
    }
}

   

