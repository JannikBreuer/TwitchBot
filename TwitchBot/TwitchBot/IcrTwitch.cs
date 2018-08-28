using System;
using System.Net.Sockets;
using System.IO;

namespace TwitchBot
{

    public delegate EventHandler OwnEventHandler(object sender, EventArgs e);

    public class IcrTwitch
    {
        private TcpClient _tcpClient;
        private StreamReader _inputStream;
        private StreamWriter _outputStream;

        private string _userName;
        private string _channelName;

        public IcrTwitch(string ip, int port, string userName, string password, string channelName)
        {
            try
            {
                _tcpClient = new TcpClient(ip, port);
                _inputStream = new StreamReader(_tcpClient.GetStream());
                _outputStream = new StreamWriter(_tcpClient.GetStream());

                _channelName = channelName;
                _userName = userName;

                _outputStream.Write("PASS " + password);
                _outputStream.Write("USER " + userName);
                _outputStream.Write("JOIN #" + channelName);


                _outputStream.Flush();  //Send stream to the Server


            }
            catch (Exception e)
            {
                Console.WriteLine("Ein Fehler ist bei dem Verbindungsaufbau aufgetreten: \n" + e.Message);
            }
        }
        public void SendMessage(string message)
        {
            if (_outputStream != null)
            {
                try
                {
                    _outputStream.Write(":" + _userName + "!" + _userName + "@" + _userName +
                ".tmi.twitch.tv PRIVMSG #" + _channelName + " :" + message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler beim senden einer Naricht.\n Exception: " + e.Message);
                }
            }
        }
    }
}
