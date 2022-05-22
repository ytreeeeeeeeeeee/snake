using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class HighScoreControl : MonoBehaviour
{
    string secretKey = "mySecretKey";
    public string addScoreURL = "http://f0671489.xsph.ru/addscore.php?";
    public string highscoreURL = "http://f0671489.xsph.ru/display.php";
    [SerializeField] Text nameTextInput;
    [SerializeField] Text scoreTextInput;
    [SerializeField] Text nameResultText;
    [SerializeField] Text scoreResultText;
    public void GetScoreBtn()
    {
        nameResultText.text = "Player: \n \n";
        scoreResultText.text = "Score: \n \n";
        StartCoroutine(GetScores());
    }
    public void SendScoreBtn()
    {
        StartCoroutine(PostScores(nameTextInput.text, Convert.ToInt32(scoreTextInput.text)));
        nameTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
        scoreTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
    }
    IEnumerator GetScores()
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            Debug.LogError("There was an error getting the high score: " + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                print(splitData);
                for (int i = 0; i < mc.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        print(splitData[i]);
                        nameResultText.text += splitData[i];
                    }
                    else
                        scoreResultText.text += splitData[i];
                }
            }
        }
    }
    IEnumerator PostScores(string name, int score)
    {
        string hash = HashInput(name + score + secretKey);
        string post_url = addScoreURL + "name=" + UnityWebRequest.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.LogError("There was an error posting the high score: " + hs_post.error);
    }
    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue = hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert = BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
}
