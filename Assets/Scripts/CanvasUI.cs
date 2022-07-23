using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private UnityEvent menuEvents;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private TextMeshProUGUI recordPauseText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI playText;

    private int lastRecord;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        InvokeRepeating("TextBounce", 1.3f, 1.5f);

        if (PlayerPrefs.HasKey("firstTimePlay") == false)
            PlayerPrefs.SetInt("firstTimePlay", 1);
        if (PlayerPrefs.HasKey("record") == false)
            PlayerPrefs.SetInt("record", 0);

        lastRecord = PlayerPrefs.GetInt("record");
        distanceText.text = "";

        if (PlayerPrefs.GetInt("firstTimePlay") == 1)
            recordText.text = "";
        else
            recordText.text = "record " + PlayerPrefs.GetInt("record").ToString() + " m";
    }

    private void Update()
    {
        if (player.transform.position.y > 0)
            distanceText.text = (int)player.transform.position.y + " m";

        if (lastRecord < (int)player.transform.position.y)
        {
            lastRecord = (int)player.transform.position.y;
            PlayerPrefs.SetInt("record", lastRecord);
        }
    }

    public void PlayGame()
    {
        menuEvents.Invoke();
        PlayerPrefs.SetInt("firstTimePlay", 0);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        recordPauseText.text = "record " + PlayerPrefs.GetInt("record").ToString() + " m";
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void ExitMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    private void TextBounce()
    {
        StartCoroutine("TextBounceDelay");
    }
    private IEnumerator TextBounceDelay()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.01f);
            playText.fontSize += .2f;
        }
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.01f);
            playText.fontSize -= .2f;
        }
    }
}
