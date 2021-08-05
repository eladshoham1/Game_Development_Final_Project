using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamBehavior : MonoBehaviour
{
    public GameObject statusCanvas;
    public GameObject gameOverCanvas;
    public GameObject winnerStatus;
    public MouseLook cameraLook;

    private GameObject[] npcs;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        npcs = new GameObject[this.transform.childCount];
        delay = 0f;

        for (int i = 0; i < this.transform.childCount; i++)
            npcs[i] = this.transform.GetChild(i).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if (CheckGameOver())
            delay += Time.deltaTime;

        if (delay >= 2f)
        {
            if (this.name == "PlayerTeam")
                winnerStatus.GetComponent<TextMeshProUGUI>().text = "You Lost";
            else
                winnerStatus.GetComponent<TextMeshProUGUI>().text = "You Win";

            statusCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            cameraLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

    private bool CheckGameOver()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (!npcs[i].GetComponent<Stats>().IsDead())
                return false;
        }

        return true;
    }
}
