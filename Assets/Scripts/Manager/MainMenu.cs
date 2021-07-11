using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _startButton, _optionsButton, _controlsButton, _backButton;
    [SerializeField]
    private Slider _mouseSlider;
    [SerializeField]
    private Text _controlsText, _mouseSensText;
    [SerializeField]
    private Image _controlsImage;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options() 
    {
        _startButton.gameObject.SetActive(false);
        _optionsButton.gameObject.SetActive(false);
        _controlsButton.gameObject.SetActive(false);
        _mouseSlider.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
        _mouseSensText.gameObject.SetActive(true);
    }

    public void Back()
    {
        _startButton.gameObject.SetActive(true);
        _optionsButton.gameObject.SetActive(true);
        _controlsButton.gameObject.SetActive(true);
        _mouseSlider.gameObject.SetActive(false);
        _mouseSensText.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _controlsText.gameObject.SetActive(false);
        _controlsImage.gameObject.SetActive(false);
    }

    public void UpdateMouseSens()
    {
        ScoreStats.MouseSensitivity = _mouseSlider.value;
        Debug.Log(ScoreStats.MouseSensitivity);
    }

    public void Controls()
    {
        _startButton.gameObject.SetActive(false);
        _optionsButton.gameObject.SetActive(false);
        _controlsButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(true);
        _controlsText.gameObject.SetActive(true);
        _controlsImage.gameObject.SetActive(true);
    }


}
