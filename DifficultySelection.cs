using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public string difficulty;

    public int firstTimeChosing;

    void Start () {
        difficulty = PlayerPrefs.GetString("modes");

        firstTimeChosing = PlayerPrefs.GetInt("firstChoose");
        if(firstTimeChosing == 0) {
            difficulty = "easy";
        }
    }

    /*void Update () {
        PlayerPrefs.SetString ("modes", difficulty);
    }*/

    public void SelectDifficulty (string dif) {
        difficulty = dif;
        PlayerPrefs.SetString ("modes", difficulty);

        firstTimeChosing = 1;
        PlayerPrefs.SetInt("firstChoose", firstTimeChosing);
    }
}
