using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBManager : MonoBehaviour
{
    [SerializeField] InputField nick;
    [SerializeField] Text nameResultText;
    [SerializeField] Text scoreResultText;
    public void reg()
    {
        StartCoroutine(Registration(nick.text));
        PlayerPrefs.SetString("Name", nick.text);
        SceneManager.LoadScene(1);
    }
    public void addScore(string nick, int score)
    {
        StartCoroutine(AddScore(nick, score));
    }
    public void show()
    {
        StartCoroutine(ShowScore());
    }
    IEnumerator Registration(string nick)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", nick);
        WWW www = new WWW("http://f0671489.xsph.ru/register.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
            yield break;
        }
        Debug.Log(www.text);
    }
    IEnumerator AddScore(string nick, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", nick);
        form.AddField("Score", score);
        WWW www = new WWW("http://f0671489.xsph.ru/add.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
            yield break;
        }
        Debug.Log(www.text);
    }
    IEnumerator ShowScore()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://f0671489.xsph.ru/show.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
            yield break;
        }
        Debug.Log(www.text);
        nameResultText.text = "  Nickname: \n \n";
        scoreResultText.text = "Score: \n \n";
        string[] mas = www.text.Split(' ');
        int num = 1;
        for (int i = 0; i < mas.Length - 1; i = i + 2)
        {
            nameResultText.text += num + " " + mas[i] + '\n';
            scoreResultText.text += mas[i + 1] + '\n';
            num++;
        }
    }
}
