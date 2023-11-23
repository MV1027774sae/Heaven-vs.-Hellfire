using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputScore;
    [SerializeField] TMP_InputField inputFieldName;

    public UnityEvent<string, int> submitScoreEvents;
    public void SubmitScore()
    {
        submitScoreEvents.Invoke(inputFieldName.text, int.Parse(inputScore.text));
    }
}
