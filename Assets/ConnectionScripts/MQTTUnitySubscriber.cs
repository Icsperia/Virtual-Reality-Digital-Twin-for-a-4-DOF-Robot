using UnityEngine;
using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;
using System.Text;
using Mono.Cecil.Cil;


public class MQTTUnitySubscriber : MonoBehaviour
{
   public string brokerIp = "10.104.183.112";
   public int brokerPort = 1883;

async void Start()
    {
        await Subscriber();

    }
async Task Subscriber()
    {
        var MqttFactory = new MqttFactory();
        var mqttClient = MqttFactory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
        .WithTcpServer(brokerIp,brokerPort)
        .WithClientId("MaxArmPub")
        .Build();


        mqttClient.ApplicationMessageReceivedAsync +=e =>
        {
            var buffer  = e.ApplicationMessage.PayloadSegment;
            var payload = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Debug.Log("Received message " + payload);
            return Task.CompletedTask;
        };

        await mqttClient.ConnectAsync(options);
        Debug.Log("Subscriber Connected");

        await mqttClient.SubscribeAsync("feedback/topic");
        Debug.Log("Subscribed to feedback/topic");

        await mqttClient.SubscribeAsync("reset/topic");
        Debug.Log("reset/topic");

        
        await mqttClient.SubscribeAsync("f_noozle/topic");
        Debug.Log("feedback/topic");


    }

}
