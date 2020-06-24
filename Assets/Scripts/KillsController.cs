using TMPro;
using UnityEngine;

public class KillsController : MonoBehaviour
{
    TextMeshProUGUI tMPro;

    private int value;

    private void Start()
    {
        tMPro = GetComponent<TextMeshProUGUI>();
    }

    public void AddKill()
    {
        value++;
        tMPro.text = value.ToString();
    }
}