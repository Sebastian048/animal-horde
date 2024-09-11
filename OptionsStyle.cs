using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsStyle : MonoBehaviour
{
    public Button[] optionsButtons;

    public GameObject[] gameModeButtons;
    public Material[] textMatGM;
    public int selectedGM;

    public GameObject[] seletSquare;
    public GameObject[] selectPic;
    public GameObject[] selectText;
    public int selectedMap;

    public bool menu;

    public GameObject[] GMType;

    public DifficultySelection dif;
    public GameObject[] difficultyImages;
    public int d;

    public Sprite orangeButton;
    public Sprite blackButton;

    void Start () {
        if (menu == true) {
            selectedGM = PlayerPrefs.GetInt("GM");
            selectedMap = PlayerPrefs.GetInt("Map");
            
            if (selectedMap == 4) {
                SelectMap(0);
            } else {
                SelectMap(selectedMap);
            }

            if (selectedMap == 4) {
                GMType[0].SetActive(true);
                GMType[1].SetActive(false);
                SelectGameModeButton(0);
            } else {
                GMType[0].SetActive(false);
                GMType[1].SetActive(true);
                SelectGameModeButton(1);
            }
        }
    }

    public void ChangeButtonSelected (float select) {
        if (select == 0) {
            optionsButtons[0].GetComponent<Image>().sprite = orangeButton;
            optionsButtons[1].GetComponent<Image>().sprite = blackButton;
            optionsButtons[2].GetComponent<Image>().sprite = blackButton;
        } else if (select == 1) {
            optionsButtons[1].GetComponent<Image>().sprite = orangeButton;
            optionsButtons[0].GetComponent<Image>().sprite = blackButton;
            optionsButtons[2].GetComponent<Image>().sprite = blackButton;
        } else if (select == 2) {
            optionsButtons[0].GetComponent<Image>().sprite = orangeButton;
            optionsButtons[1].GetComponent<Image>().sprite = blackButton;
            optionsButtons[2].GetComponent<Image>().sprite = blackButton;
        }
    }

    public void SelectGameModeButton (int select) {
        selectedGM = select;
        PlayerPrefs.SetInt("GM", selectedGM);
        if (selectedGM == 0) {
            gameModeButtons[0].GetComponent<Image>().sprite = orangeButton;
            gameModeButtons[2].GetComponent<Text>().material = textMatGM[0];
            gameModeButtons[1].GetComponent<Image>().sprite = blackButton;
            gameModeButtons[3].GetComponent<Text>().material = textMatGM[1];
        } else if (selectedGM == 1) {
            gameModeButtons[1].GetComponent<Image>().sprite = orangeButton;
            gameModeButtons[3].GetComponent<Text>().material = textMatGM[0];
            gameModeButtons[0].GetComponent<Image>().sprite = blackButton;
            gameModeButtons[2].GetComponent<Text>().material = textMatGM[1];
        }
    }

    public void SelectMap (int select) {
        selectedMap = select;
        PlayerPrefs.SetInt("Map", selectedMap);
        for (int i = 0; i < seletSquare.Length; i++) {
            if (i == selectedMap) {
                seletSquare[selectedMap].SetActive(true);
                selectPic[selectedMap].SetActive(true);
                selectText[selectedMap].SetActive(true);

                if (i == 0) {
                    if (dif.difficulty == "easy") {
                        d = 0;
                    } else if (dif.difficulty == "medium") {
                        d = 1;
                    } else if (dif.difficulty == "hard") {
                        d = 2;
                    }

                    /*for (int p = 0; p < difficultyImages.Length; p++) {
                        if (p == d) {
                            difficultyImages[p].SetActive(true);
                        } else {
                            difficultyImages[p].SetActive(false);
                        }
                    }*/
                } else {
                    /*for (int p = 0; p < difficultyImages.Length; p++) {
                        difficultyImages[p].SetActive(false);
                    }*/
                }
            } else {
                seletSquare[i].SetActive(false);
                selectPic[i].SetActive(false);
                selectText[i].SetActive(false);
            }
        }
        if (selectedMap == 0) {
            selectedMap = 4;
            PlayerPrefs.SetInt("Map", selectedMap);
        }
    }
}
