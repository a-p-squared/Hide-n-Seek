using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public Camera mainMenuCamera, playerCamera;

    public Canvas mainMenuCanvas;
    public Canvas gameOverCanvas;
    public Canvas winGameCanvas;

    public Text taggedText;
    public Text scoreText;
    int tagged;
    int score;
    public GameObject Hands;
    public Text waitText;
    float timeToWait;
    bool isWaiting;

    public GameObject player;
    public GameObject[] AIs;
    public GameObject[] HidingSpots;
    public GameObject spawnPoint;

    public bool isPlaying;

    public AudioSource soundEffects;
    public AudioClip PlayClip;
    public AudioClip WinClip;
    public AudioClip LoseClip;

    // Start is called before the first frame update
    void Start()
    {
        timeToWait = 10.0f;
        tagged = 0;
        score = 0;
        isWaiting = false;
        isPlaying = false;
        AIs = GameObject.FindGameObjectsWithTag("AI");
        HidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying && !isWaiting)
        {
            player.GetComponent<PlayerControllerScript>().playerMovement();
            player.GetComponent<PlayerControllerScript>().checkPlayers();
        }
        if (isWaiting)
        {
            waiting();
        }
    }
    public void play()
    {
        playerCamera.gameObject.SetActive(true);
        mainMenuCamera.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(false);
        isPlaying = true;
        startWaiting();
        for (int i = 0; i < AIs.Length; i++)
        {
            AIs[i].GetComponent<AIScript>().hide(false);
        }

        soundEffects.clip = PlayClip;
        soundEffects.Play();

    }

    void startWaiting()
    {

        waitText.gameObject.SetActive(true);
        Hands.SetActive(true);
        isWaiting = true;
    }

    void waiting()
    {
        waitText.text = "Time To Wait : " + (int)timeToWait + " s";
        timeToWait -= Time.deltaTime;

        Debug.Log("WaitStart");
        if (timeToWait <= 0.0f)
        {
            isWaiting = false;
            waitText.gameObject.SetActive(false);
            Hands.SetActive(false);
            player.GetComponent<PlayerControllerScript>().canTag = true;
            scoreText.gameObject.SetActive(true);
            taggedText.gameObject.SetActive(true);
        }
    }

    public void updateTagged()
    {
        tagged++;
        taggedText.text = "Tagged : " + tagged;
    }

    public void updateScore()
    {
        score += tagged;
        tagged = 0;
        scoreText.text = "Score : " + score;
        taggedText.text = "Tagged : " + tagged;
        if(score == AIs.Length)
        {
            winGame();
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void restart()
    {
        timeToWait = 10.0f;
        tagged = 0;
        score = 0;
        isWaiting = false;
        isPlaying = false;
        // SceneManager.LoadScene(0, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync(1);
    }

    public void gameOver()
    {
        Time.timeScale = 0.0f;
        gameOverCanvas.gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        soundEffects.clip = LoseClip;
        soundEffects.Play();
    }

    public void winGame()
    {
        scoreText.gameObject.SetActive(false);
        taggedText.gameObject.SetActive(false);
        Time.timeScale = 0.0f;
        winGameCanvas.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        soundEffects.clip = WinClip;
        soundEffects.Play();
    }
}
