using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace TwitchBot
{
    public class TwitchMessage
    {

        private StreamReader _inputStream;
        private StreamWriter _streamWriter;
        private Thread lissenMessageThread;

        public TwitchMessage()
        {

        }
        public TwitchMessage(StreamWriter streamWriter, StreamReader streamReader)
        {
            _inputStream = streamReader;
            _streamWriter = streamWriter;
        }

        public void StartLissenForMessage()
        {
            lissenMessageThread = new Thread(StartLissenForMessage);
            lissenMessageThread.Start();
        }
        public void StopLissenForMessage()
        {
            lissenMessageThread.Abort();
        }
        public void DisplayMessageOnScreen(string userName, string message, string date)
        { 
            TwitchBotWin.winRef.AddNewMessageToStackPanel(userName, message, date);
        }
        private void SplitUpMessage(string message)
        {

        }
        private string ReadMessage()
        {
            if (_inputStream != null)
            {
                try
                {
                    string message = _inputStream.Read().ToString();
                    return message;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler beim lesen einer Naricht.\nExeption: " + e.Message);
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Fehler beim lesen einer Naricht. Inputstream is NULL");
                return null;
            }
        }
        private void LissenForMessage()
        {
            while(true)
            {
                ReadMessage();
            }
        }
    }
}
