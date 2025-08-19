using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;

    private float _timeToRefreshText = 0.7f;
    private float _currentTimeToRefresh = 0f;
    private bool _isFirstTextActivate = true;

    public bool IsReady { get; private set; }

    private void Awake()
    {
        _slider.maxValue = GameStaticData.LoadingScreenLoadingTime;
        _slider.value = 0;
        _text.gameObject.SetActive(false);
    }

    private void Update()
    {
        _slider.value += Time.deltaTime;

        if (_slider.value == _slider.maxValue)
        {
            IsReady = true;

            if (_text.IsActive() == false && _isFirstTextActivate)
            {
                _isFirstTextActivate = false;
                _text.gameObject.SetActive(true);
            }

            _currentTimeToRefresh += Time.deltaTime;

            if (_currentTimeToRefresh >= _timeToRefreshText)
            {
                if (_text.IsActive() == false)
                {
                    _text.gameObject.SetActive(true);
                }
                else if (_text.IsActive() == true)
                {
                    _text.gameObject.SetActive(false);
                }

                _currentTimeToRefresh = 0;
            }
        }
    }
}
