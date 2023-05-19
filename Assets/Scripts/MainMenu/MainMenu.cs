using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputField boardSizeInputField;
    [SerializeField] private Button generateButton;

    void Start()
    {
        generateButton.onClick.AddListener(OnGenerateButtonClick);
    }

    private void OnGenerateButtonClick()
    {
        int size;
        if (int.TryParse(boardSizeInputField.text, out size) && size > 1)
        {
            PlayerPrefs.SetInt("boardSize", size);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Input must be an integer greater than 1!");
        }
    }
}
