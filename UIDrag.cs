using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDrag : MonoBehaviour
{
    public Options options;
    bool startDrag;

    public GameObject[] buttons;

    public GameObject[] realButtons;

    public Vector3[] posButton;
    public Vector3[] saveWhenEnter;
    public Vector3[] originalPos;

    public int buttonSelected;

    public float[] posX;
    public float[] posY;

    public float[] sizeX;
    public float[] sizeY;

    public Vector2[] startSize;
    public Vector2[] saveSize;
    public Vector2[] sizeSaveWhenEnter;

    public int enterFirst;

    public Slider sizeSlider;
    public Text sizeText;

    public RectTransform[] joystickSize;

    public GameObject[] dragButtons;
    public int dragFloat;

    void Start()
    {
        enterFirst = PlayerPrefs.GetInt("UIFirst");

        for (int i = 0; i < originalPos.Length; i++) {
            originalPos[i] = realButtons[i].transform.position;
            startSize[i] = realSizeButton[i].sizeDelta;
        }

        if (enterFirst == 1) {

        posX[0] = PlayerPrefs.GetFloat("x");
        posY[0] = PlayerPrefs.GetFloat("y");
        posX[1] = PlayerPrefs.GetFloat("x1");
        posY[1] = PlayerPrefs.GetFloat("y1");
        posX[2] = PlayerPrefs.GetFloat("x2");
        posY[2] = PlayerPrefs.GetFloat("y2");
        posX[3] = PlayerPrefs.GetFloat("x3");
        posY[3] = PlayerPrefs.GetFloat("y3");
        posX[4] = PlayerPrefs.GetFloat("x4");
        posY[4] = PlayerPrefs.GetFloat("y4");
        posX[5] = PlayerPrefs.GetFloat("x5");
        posY[5] = PlayerPrefs.GetFloat("y5");
        posX[6] = PlayerPrefs.GetFloat("x6");
        posY[6] = PlayerPrefs.GetFloat("y6");
        posX[7] = PlayerPrefs.GetFloat("x7");
        posY[7] = PlayerPrefs.GetFloat("y7");
        posX[8] = PlayerPrefs.GetFloat("x8");
        posY[8] = PlayerPrefs.GetFloat("y8");
        posX[9] = PlayerPrefs.GetFloat("x9");
        posY[9] = PlayerPrefs.GetFloat("y9");
        posX[10] = PlayerPrefs.GetFloat("x10");
        posY[10] = PlayerPrefs.GetFloat("y10");

        sizeX[0] = PlayerPrefs.GetFloat("sx");
        sizeY[0] = PlayerPrefs.GetFloat("sy");
        sizeX[1] = PlayerPrefs.GetFloat("sx1");
        sizeY[1] = PlayerPrefs.GetFloat("sy1");
        sizeX[2] = PlayerPrefs.GetFloat("sx2");
        sizeY[2] = PlayerPrefs.GetFloat("sy2");
        sizeX[3] = PlayerPrefs.GetFloat("sx3");
        sizeY[3] = PlayerPrefs.GetFloat("sy3");
        sizeX[4] = PlayerPrefs.GetFloat("sx4");
        sizeY[4] = PlayerPrefs.GetFloat("sy4");
        sizeX[5] = PlayerPrefs.GetFloat("sx5");
        sizeY[5] = PlayerPrefs.GetFloat("sy5");
        sizeX[6] = PlayerPrefs.GetFloat("sx6");
        sizeY[6] = PlayerPrefs.GetFloat("sy6");
        sizeX[7] = PlayerPrefs.GetFloat("sx7");
        sizeY[7] = PlayerPrefs.GetFloat("sy7");
        sizeX[8] = PlayerPrefs.GetFloat("sx8");
        sizeY[8] = PlayerPrefs.GetFloat("sy8");
        sizeX[9] = PlayerPrefs.GetFloat("sx9");
        sizeY[9] = PlayerPrefs.GetFloat("sy9");
        sizeX[10] = PlayerPrefs.GetFloat("sx10");
        sizeY[10] = PlayerPrefs.GetFloat("sy10");


        for (int i = 0; i < posX.Length; i++) {
            posButton[i] = new Vector3 (posX[i], posY[i], 0);
            realButtons[i].transform.position = posButton[i];
            buttons[i].transform.position = posButton[i];
            saveSize[i] = new Vector2 (sizeX[i], sizeY[i]);
            sizeButton[i].sizeDelta = saveSize[i];
            realSizeButton[i].sizeDelta = sizeButton[i].sizeDelta;
        }

        }
         
        SelectedButton(0);
    }

    void Update()
    {
        if (options.paused == true) {
        joystickSize[0].sizeDelta = sizeButton[6].sizeDelta * 3;
        joystickSize[1].sizeDelta = sizeButton[6].sizeDelta * 3;
        joystickSize[2].sizeDelta = sizeButton[8].sizeDelta * 2;
        joystickSize[3].sizeDelta = sizeButton[8].sizeDelta * 2;
        joystickSize[4].sizeDelta = sizeButton[6].sizeDelta;
        joystickSize[5].sizeDelta = sizeButton[6].sizeDelta;
        joystickSize[6].sizeDelta = sizeButton[8].sizeDelta * 1.5f;
        joystickSize[7].sizeDelta = sizeButton[8].sizeDelta * 1.5f;

        sizeText.text = Mathf.Round(sizeSlider.value).ToString();
        PlayerPrefs.SetInt("UIFirst", enterFirst);
        if (startDrag == true) {
            dragButtons[dragFloat].transform.position = Input.mousePosition;
        }  
        }
    }

    public void StartDragUI (int button) {
        dragFloat = button;
        startDrag = true;
    }

    public void StopDragUI () {
        startDrag = false;
    }

    public void SavePosition () {
        enterFirst = 1;

        for (int i = 0; i < buttons.Length; i++) {
            posX[i] = buttons[i].transform.position.x;
            posY[i] = buttons[i].transform.position.y;

            sizeX[i] = sizeButton[i].sizeDelta.x;
            sizeY[i] = sizeButton[i].sizeDelta.y;

            realButtons[i].transform.position = buttons[i].transform.position;
            realSizeButton[i].sizeDelta = sizeButton[i].sizeDelta;
        }
        PlayerPrefs.SetFloat("x", posX[0]);
        PlayerPrefs.SetFloat("y", posY[0]);
        PlayerPrefs.SetFloat("x1", posX[1]);
        PlayerPrefs.SetFloat("y1", posY[1]);
        PlayerPrefs.SetFloat("x2", posX[2]);
        PlayerPrefs.SetFloat("y2", posY[2]);
        PlayerPrefs.SetFloat("x3", posX[3]);
        PlayerPrefs.SetFloat("y3", posY[3]);
        PlayerPrefs.SetFloat("x4", posX[4]);
        PlayerPrefs.SetFloat("y4", posY[4]);
        PlayerPrefs.SetFloat("x5", posX[5]);
        PlayerPrefs.SetFloat("y5", posY[5]);
        PlayerPrefs.SetFloat("x6", posX[6]);
        PlayerPrefs.SetFloat("y6", posY[6]);
        PlayerPrefs.SetFloat("x7", posX[7]);
        PlayerPrefs.SetFloat("y7", posY[7]);
        PlayerPrefs.SetFloat("x8", posX[8]);
        PlayerPrefs.SetFloat("y8", posY[8]); 
        PlayerPrefs.SetFloat("x9", posX[9]);
        PlayerPrefs.SetFloat("y9", posY[9]);
        PlayerPrefs.SetFloat("x10", posX[10]);
        PlayerPrefs.SetFloat("y10", posY[10]); 

        PlayerPrefs.SetFloat("sx", sizeX[0]);
        PlayerPrefs.SetFloat("sy", sizeY[0]);
        PlayerPrefs.SetFloat("sx1", sizeX[1]);
        PlayerPrefs.SetFloat("sy1", sizeY[1]);
        PlayerPrefs.SetFloat("sx2", sizeX[2]);
        PlayerPrefs.SetFloat("sy2", sizeY[2]);
        PlayerPrefs.SetFloat("sx3", sizeX[3]);
        PlayerPrefs.SetFloat("sy3", sizeY[3]);
        PlayerPrefs.SetFloat("sx4", sizeX[4]);
        PlayerPrefs.SetFloat("sy4", sizeY[4]);
        PlayerPrefs.SetFloat("sx5", sizeX[5]);
        PlayerPrefs.SetFloat("sy5", sizeY[5]);
        PlayerPrefs.SetFloat("sx6", sizeX[6]);
        PlayerPrefs.SetFloat("sy6", sizeY[6]);
        PlayerPrefs.SetFloat("sx7", sizeX[7]);
        PlayerPrefs.SetFloat("sy7", sizeY[7]);
        PlayerPrefs.SetFloat("sx8", sizeX[8]);
        PlayerPrefs.SetFloat("sy8", sizeY[8]); 
        PlayerPrefs.SetFloat("sx9", sizeX[9]);
        PlayerPrefs.SetFloat("sy9", sizeY[9]);
        PlayerPrefs.SetFloat("sx10", sizeX[10]);
        PlayerPrefs.SetFloat("sy10", sizeY[10]); 
    }

    public void EnterSavePos () {
        for (int i = 0; i < buttons.Length; i++) {
            saveWhenEnter[i] = realButtons[i].transform.position;
        }

        for (int i = 0; i < sizeButton.Length; i++) {
            sizeSaveWhenEnter[i] = sizeButton[i].sizeDelta; 
        }
    }

    public void CancelPositionSave () {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].transform.position = saveWhenEnter[i];
        }
        
        for (int i = 0; i < sizeButton.Length; i++) {
            sizeButton[i].sizeDelta = sizeSaveWhenEnter[i];
        }
        sizeSlider.value = startSize[buttonSelected].x + startSize[buttonSelected].y;
    }

    public void OriginalPostion () {
        for (int i = 0; i < originalPos.Length; i++) {
            buttons[i].transform.position = originalPos[i];
        }
        for (int i = 0; i < sizeButton.Length; i++) {
            sizeButton[i].sizeDelta = startSize[i];
        }
        sizeSlider.value = startSize[buttonSelected].x + startSize[buttonSelected].y;
    }

    public void SelectedButton (int buttonSelected) {
        selectedButton = buttonSelected;

        if (buttonSelected == 9 || buttonSelected == 10) {
            sizeSlider.gameObject.SetActive(false);
            sizeText.gameObject.SetActive(false);
        } else {
            sizeSlider.gameObject.SetActive(true);
            sizeText.gameObject.SetActive(true);
        }
        
        if (buttonSelected == 6 || buttonSelected == 8) {
            sizeSlider.minValue = 100;
        } else {
            sizeSlider.minValue = startSize[buttonSelected].x - startSize[buttonSelected].y * 0.6f;
        }

        sizeSlider.maxValue = startSize[buttonSelected].x + startSize[buttonSelected].y * 3;

        if (startSize[buttonSelected].y == sizeButton[buttonSelected].sizeDelta.y) {
            sizeSlider.value = startSize[buttonSelected].x + startSize[buttonSelected].y;
        } else {
            sizeSlider.value = sizeButton[buttonSelected].sizeDelta.x + sizeButton[buttonSelected].sizeDelta.y;
        }
    }

    public void ChangeSize () {
        sizeButton[selectedButton].sizeDelta = new Vector2(sizeSlider.value / 2, sizeSlider.value / 2);
    }

    public RectTransform[] sizeButton;
    public RectTransform[] realSizeButton;
    public int selectedButton;
}
