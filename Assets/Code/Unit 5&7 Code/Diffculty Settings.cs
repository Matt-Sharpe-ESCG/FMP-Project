using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffcultySettings : MonoBehaviour
{
    public Slider difficultySlider;

    public GameObject easyText;
    public GameObject normalText;
    public GameObject hardText;

    public int difficultyGrade;

    public void Update()
    {
        difficultySlider.value = difficultyGrade;

        if (difficultyGrade == 1)
        {
            easyText.SetActive(true);
            normalText.SetActive(false);
            hardText.SetActive(false);
        }
        else if (difficultyGrade == 2)
        {
            easyText.SetActive(false);
            normalText.SetActive(true);
            hardText.SetActive(false);
        }
        else if (difficultyGrade == 3)
        {
            easyText.SetActive(false);
            normalText.SetActive(false);
            hardText.SetActive(true);
        }
    }
}
