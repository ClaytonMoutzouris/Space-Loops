using TMPro;
using UnityEngine;

public class LogNodeUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetEntry(string logEntry)
    {
        text.text = logEntry;
    }

}
