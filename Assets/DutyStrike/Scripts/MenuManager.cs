using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject statusCanvas;
    public MouseLook cameraLook;
    public AudioMixer am;
    public Dropdown dropdownQuality;
    public Dropdown dropdownResolution;

    private bool isMenuPaused;
    private bool isFullScreen;

    private Resolution[] rsl;
    private List<string> resolutions;

    // Start is called before the first frame update
    void Start()
    {
        isMenuPaused = true;
        isFullScreen = false;
        dropdownQuality.value = 2;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveMenu();
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
        if (Input.GetKeyDown(KeyCode.Escape))
            isMenuPaused = !isMenuPaused;

        if (isMenuPaused)
        {
            menuCanvas.SetActive(true);
            statusCanvas.SetActive(false);
            cameraLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            menuCanvas.SetActive(false);
            statusCanvas.SetActive(true);
            cameraLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }

    public void Play()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}