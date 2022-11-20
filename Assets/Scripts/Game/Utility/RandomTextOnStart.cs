using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTextOnStart : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    public List<string> randomText = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        textUI.text = randomText[Random.Range(0, randomText.Count)];
    }

}
