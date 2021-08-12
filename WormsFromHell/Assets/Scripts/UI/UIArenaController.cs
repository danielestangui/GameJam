using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIArenaController : MonoBehaviour
{
    public Text _scoreText;

    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = playerController.GetScore().ToString();

        if (Input.GetKeyDown(KeyCode.F)) {
            RestartLvl();
        }
    }

    private void RestartLvl() {
        SceneManager.LoadScene("ArenaScene");
    }
}
