using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class testingForPhone : MonoBehaviour
{
    [SerializeField] private Button greenButton;
    [SerializeField] private Button redButton;
    [SerializeField] private TextMeshProUGUI testText;

    private void Awake()
    {
        greenButton.onClick.AddListener(() =>
        {
            testText.text = "Press on red";
        });

        redButton.onClick.AddListener(() =>
        {
            testText.text = "Press on green";
        });
    }

}
