using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    [SerializeField]
    private float _startingTime = 30.0f;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    private float _timeRemaining;

    public float TimeRemaining { get { return _timeRemaining; } }

    private void Start()
    {
        _timeRemaining = _startingTime;
        if (_timerText)
        {
            _timerText.text = _timeRemaining.ToString("0.0");
        }
    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        _timeRemaining = Mathf.Clamp(_timeRemaining, 0, _startingTime);

        if (_timerText)
        {
            _timerText.text = _timeRemaining.ToString("0.0");
        }
    }
}
