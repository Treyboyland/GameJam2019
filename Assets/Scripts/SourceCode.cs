using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SourceCode : MonoBehaviour
{
    [SerializeField]
    float secondsBetweenSwaps;

    [SerializeField]
    List<TextAsset> codeFiles;

    [SerializeField]
    TextMeshProUGUI textBox;

    List<string> codeStrings;



    void Start()
    {
        ConvertCode();
        StartCoroutine(SwapCodes());
    }

    void ConvertCode()
    {
        codeStrings = new List<string>(codeFiles.Count);

        foreach(TextAsset codeFile in codeFiles)
        {
            codeStrings.Add(codeFile.text);
        }
    }



    IEnumerator SwapCodes()
    {
        int index = 0;
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        while(true)
        {
            while(timer.Elapsed.TotalSeconds < secondsBetweenSwaps)
            {
                yield return null;
            }
            timer.Stop();
            timer.Reset();
            timer.Start();

            textBox.text = codeStrings[index];

            index = (index + 1) % codeStrings.Count;
        }
    }


}
