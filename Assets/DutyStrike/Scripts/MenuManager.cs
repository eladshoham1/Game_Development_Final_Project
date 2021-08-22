using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject menuPanel;
    public GameObject playButton;
    public GameObject resetButton;
    public GameObject gameOverCanvas;
    public GameObject statusCanvas;
    public TextMeshProUGUI winnerStatus;
    public MouseLook cameraLook;
    public AudioMixer am;
    public Dropdown dropdownQuality;
    public Dropdown dropdownResolution;
    public GameObject playerTeam;
    public GameObject enemyTeam;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject playerDeadCamera;


    private Resolution[] rsl;
    private List<string> resolutions;
    private bool isGameStarted;
    private bool isMenuPaused;
    private bool isFullScreen;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        isMenuPaused = true;
        isFullScreen = false;
        FullScreenToggle();
        dropdownQuality.value = 2;
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveMenu();
        CheckGameOver();
    }

    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }

        dropdownResolution.ClearOptions();
        dropdownResolution.AddOptions(resolutions);
        dropdownResolution.value = resolutions.FindIndex(a => a.Contains(Screen.width + "x" + Screen.height));
    }

    void ActiveMenu()
    {
        if (player.GetComponent<Stats>().IsDead())
        {
            playerCamera.SetActive(false);
            playerDeadCamera.SetActive(true);
        }

        if (isGameStarted && Input.GetKeyDown(KeyCode.Escape))
            isMenuPaused = !isMenuPaused;

        menuCanvas.SetActive(isMenuPaused);
        statusCanvas.SetActive(!isMenuPaused);
        cameraLook.enabled = !isMenuPaused;
        Cursor.lockState = isMenuPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isMenuPaused ? 0f : 1f;
    }

    public void Play()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        isGameStarted = true;
        isMenuPaused = false;
    }

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }

    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }

    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }

    public void PlayAgain()
    {
        gameOverCanvas.SetActive(false);
        menuPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void CheckGameOver()
    {
        if (IsTeamDeads(playerTeam))
            GameOver("You Lost");
        else if (IsTeamDeads(enemyTeam))
            GameOver("You Win");
    }

    private bool IsTeamDeads(GameObject team)
    {
        for (int i = 0; i < team.transform.childCount; i++)
        {
            if (!team.transform.GetChild(i).GetComponent<Stats>().IsDead())
                return false;
        }

        return true;
    }

    private void GameOver(string message)
    {
        delay += Time.deltaTime;
        if (delay < 2f)
            return;

        winnerStatus.GetComponent<TextMeshProUGUI>().text = message;
        statusCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        cameraLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
}