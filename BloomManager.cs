using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomManager : MonoBehaviour
{
    public RectTransform[] lines;

    public GunManager gun;

    public ArGun1 gun0;
    public LmgGun1 gun1;
    public ShotgunGun1 gun2;
    public SniperGun1 gun3;
    public SmgGun1 gun4;
    public PistolGun1 gun5;

    void FixedUpdate()
    {
        if (gun.gun == 0) {
            lines[0].anchoredPosition = new Vector2 (gun0.bloomNum, 0);
            lines[1].anchoredPosition = new Vector2 (0, -gun0.bloomNum);
            lines[2].anchoredPosition = new Vector2 (-gun0.bloomNum, 0);
            lines[3].anchoredPosition = new Vector2 (0, gun0.bloomNum);
        } else if (gun.gun == 1) {
            lines[0].anchoredPosition = new Vector2 (gun1.bloomNum, 0);
            lines[1].anchoredPosition = new Vector2 (0, -gun1.bloomNum);
            lines[2].anchoredPosition = new Vector2 (-gun1.bloomNum, 0);
            lines[3].anchoredPosition = new Vector2 (0, gun1.bloomNum);
        } else if (gun.gun == 2) {
            lines[0].anchoredPosition = new Vector2 (gun2.bloomNum, 0);
            lines[1].anchoredPosition = new Vector2 (0, -gun2.bloomNum);
            lines[2].anchoredPosition = new Vector2 (-gun2.bloomNum, 0);
            lines[3].anchoredPosition = new Vector2 (0, gun2.bloomNum);
        }  else if (gun.gun == 3) {
            lines[0].anchoredPosition = new Vector2 (gun3.bloomNum, 0);
            lines[1].anchoredPosition = new Vector2 (0, -gun3.bloomNum);
            lines[2].anchoredPosition = new Vector2 (-gun3.bloomNum, 0);
            lines[3].anchoredPosition = new Vector2 (0, gun3.bloomNum);
        } else if (gun.gun == 4) {
            lines[0].anchoredPosition = new Vector2 (gun4.bloomNum, 0);
            lines[1].anchoredPosition = new Vector2 (0, -gun4.bloomNum);
            lines[2].anchoredPosition = new Vector2 (-gun4.bloomNum, 0);
            lines[3].anchoredPosition = new Vector2 (0, gun4.bloomNum);
        } else if (gun.gun == 5) {
            lines[0].anchoredPosition = new Vector2 (gun5.bloomNum, 0);
            lines[1].anchoredPosition = new Vector2 (0, -gun5.bloomNum);
            lines[2].anchoredPosition = new Vector2 (-gun5.bloomNum, 0);
            lines[3].anchoredPosition = new Vector2 (0, gun5.bloomNum);
        }
    }
}
