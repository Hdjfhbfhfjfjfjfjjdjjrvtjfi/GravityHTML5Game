using Godot;
using static Godot.GD;
using System.Text;

namespace Main
{
    public class HttpClient : HTTPRequest
    {
        [Signal]
        public delegate void DataRecieved(string[,] scores);
        [Export]
        private string IPAdress { get; set; }
        private void OnGameEnded(string name, int score)
        {
            SetScore(name, score);
        }
        public void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
        {
            string json = JSON.Parse(Encoding.UTF8.GetString(body)).Result.ToString();
            if (json == "[1]")
            {
                Print("Data Saved");
            }
            else
            {
                EmitSignal(nameof(DataRecieved), JsonConvert(json));
                Print("Data Recieved");
            }
        }
        private void SetScore(string name, int score)
        {
            Request($"{IPAdress}/set/{name}/{score}", method: HTTPClient.Method.Get);
        }
        public void GetScore()
        {
            Request($"{IPAdress}/get", method: HTTPClient.Method.Get);
        }
        private string[,] JsonConvert(string json)
        {
            string[,] DataArray = new string[5, 3] 
            { 
                { null, null, null }, 
                { null, null, null },
                { null, null, null },
                { null, null, null }, 
                { null, null, null } 
            };
            json = json.Substring(1, json.Length - 2);
            for (int i = 0; i < 5; i++)
            {
                int startIndex = json.IndexOf("[");
                int count = json.IndexOf("]") - json.IndexOf("[");
                string str = json.Substr(startIndex, count + 1);
                str = str.Replace('"'.ToString(), "").Replace("[", "").Replace("]", "");
                json = json.Remove(startIndex, count + 1);
                string[] strData = str.Split(",");
                for (int j = 0; j < 3; j++)
                {
                    DataArray[i, j] = strData[j];
                }
            }
            return DataArray;
        }
    }
}