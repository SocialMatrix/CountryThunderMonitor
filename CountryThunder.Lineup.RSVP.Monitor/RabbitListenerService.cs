using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CountryThunder.Lineup.RSVP.Monitor.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using Steeltoe.Messaging.RabbitMQ.Attributes;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class RabbitListenerService
{
    public const string RECEIVE_AND_CONVERT_QUEUE = "steeltoe_message_queue";
    private ILogger _logger;
    
    public RabbitListenerService(ILogger<RabbitListenerService> logger)
    {
        _logger = logger;
    }

    [RabbitListener(RECEIVE_AND_CONVERT_QUEUE)]
    public void ListenForAMessage(string msg)
    {
        _logger.LogInformation($"Received the message '{msg}' from the queue.");
        
        RestClient client = new RestClient("http://localhost:55020/");

        var request = new RestRequest("api/RSVP", Method.POST);

        request.RequestFormat = DataFormat.Json;

        request.AddJsonBody(msg);

        client.Execute(request);
        
    }
    
}