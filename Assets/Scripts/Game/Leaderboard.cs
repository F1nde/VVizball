using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

    public int showedTimes;
    public string submitTimeUrl = "https://unity-leaderboard.herokuapp.com/submit/";
    public string getTimesUrl = "https://unity-leaderboard.herokuapp.com/times";

    public IEnumerator PostTime(string player, int level, int time)
    {
        string postUrl = submitTimeUrl + level;
        WWWForm form = new WWWForm();
        form.AddField("name", player);
        form.AddField("time", time);

        WWW response = new WWW(postUrl, form.data);
        yield return response;

        if (response.error != null)
        {
            Debug.Log("Error while sending time to the server: " + response.error);
        }
        else
        {
            Debug.Log("Time submitted to highscores!");
        }
    }

    public IEnumerator GetTimes(int level, Text textToUpdate)
    {
        string getUrl = "https://unity-leaderboard.herokuapp.com/times/" + level;
        Debug.Log(getUrl);

        WWW response = new WWW(getUrl);
        yield return response;

        if (response.error != null)
        {
            Debug.Log("Error while receiving times from the server: " + response.error);
        }
        else
        {
            string jsonData = response.text;
            JSONObject json = new JSONObject(jsonData);
            string result = "";
            int rank = 1;

            foreach (JSONObject j in json.list) {
                string line = rank.ToString() + ". " + j["name"] + "     " + j["time"] + "\n";
                result += line;

                if (rank >= showedTimes)
                    break;
                ++rank;
            }

            textToUpdate.text = result;
        }
    }

}
