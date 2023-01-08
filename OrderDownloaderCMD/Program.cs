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




class OrderDownloader
{
    static void Main(string[] args)
    {
        ApiConnector connected = new ApiConnector("2001423-2006490-YUNDJTP4BBH14NRYKTOFU7VOFRVQQBHUUA7ZWBB6UCG0KVHAXAE13LLOPHDC4IMZ");
        Console.WriteLine(connected._Token);

    }

}





class ApiConnector
{
    public string _token;
    public ApiConnector(string _token)
    {
        
        this._token = _token;
    }
    public string _Token
    {
        get { return _token; }
        set { _token = value;  }
       
    }
    
}
