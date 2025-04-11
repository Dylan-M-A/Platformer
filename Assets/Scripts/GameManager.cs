using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;

    [SerializeField]
    private GameObject _winTextBackground;

    [SerializeField]
    private GameObject _startTextBackground;

    public UnityEvent OnGameWin;
    public UnityEvent OnGameStart;

    private TimerSystem _player1Timer;
    private PlayerController _player1Controller;
    private Rigidbody _player1Rigidbody;

    private bool _gameWon = false;
    private bool _gameStart = false;

    private void Start()
    {
        if (_player1)
        {
            if (!_player1.TryGetComponent(out _player1Timer))
                Debug.LogError("GameManager: Could not get Player 1 Timer");
            if (!_player1.TryGetComponent(out _player1Controller))
                Debug.LogError("GameManager: Could not get Player 1 Controller");
            if (!_player1.TryGetComponent(out _player1Rigidbody))
                Debug.LogError("GameManager: Could not get Player 1 Rigidbody");
        }
        else
            Debug.LogError("GameManager: Player1 not assigned!");
        if (!_winTextBackground)
            Debug.LogWarning("GameManager: Win Text Background not assigned");
    }

    private void Update()
    {

        //if either timer is not assigned do nothing
        if (!(_player1Timer))
            return;

        if (_gameWon)
            return;

        //check if either timer is finished and win the game if so
        if (_player1Timer.TimeRemaining <= 0)
            Win("Player 1 Loses!");
    }

    private void Start(string startText)
    {
        if (_startTextBackground)
        {
            _startTextBackground.SetActive(true);
            TextMeshProUGUI text = _startTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);
            if (text)
            {
                text.text = startText;
            }
        }

        if (_player1Timer)
            _player1Timer.enabled = false;
        if (_player1Controller)
            _player1Controller.enabled = false;
        if (_player1Rigidbody)
            _player1Rigidbody.isKinematic = true;

        if (_gameStart == true)
        {
             _player1Timer.enabled = true;
             _player1Controller.enabled = true;
             _player1Rigidbody.isKinematic = false;
        }

        _gameStart = true;
        OnGameStart.Invoke();
    }

    private void Win(string winText)
    {
        //enable win screen ui and set text to wintext
        if (_winTextBackground)
        {
            _winTextBackground.SetActive(true);
            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);
            if (text)
            {
                text.text = winText;
            }
        }

        //turn off player controller and tag system and timer
        if (_player1Timer)
            _player1Timer.enabled = false;
        if (_player1Controller)
            _player1Controller.enabled = false;
        if (_player1Rigidbody)
            _player1Rigidbody.isKinematic = true;

        _gameWon = true;
        OnGameWin.Invoke();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
