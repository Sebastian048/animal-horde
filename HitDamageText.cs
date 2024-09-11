using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HitDamageText : MonoBehaviour
{
    public TextMeshPro textL;
    public TextMeshPro textR;
    public GameObject parentDamageTextR;
    public GameObject parentDamageTextL;
    
    public Options options;

    public GameObject pos;

    public Camera fpsCam;
    public GameObject camHolder;
    public Camera hitNumberCam;

    public float randomNumber;

    void Start () {
        hitNumberCam.gameObject.SetActive(true);

    }

    public void SpawnDamageText (float damageNum) {
        if (options.damageNumbersNum == 1) {
            GetTextPosition();
            randomNumber = Random.Range (1, 3);
        if (randomNumber == 1) {
            textR.SetText (damageNum.ToString());
            GameObject hitTextGameObjectR = Instantiate (parentDamageTextR);
            hitTextGameObjectR.transform.localScale = pos.transform.localScale;
            hitTextGameObjectR.transform.position = pos.transform.position;
            hitTextGameObjectR.transform.rotation = fpsCam.transform.rotation;
            hitTextGameObjectR.SetActive(true);
            Destroy(hitTextGameObjectR, 1f);
        } else {
            textL.SetText (damageNum.ToString());
            GameObject hitTextGameObjectL = Instantiate (parentDamageTextL);
            hitTextGameObjectL.transform.localScale = pos.transform.localScale;
            hitTextGameObjectL.transform.position = pos.transform.position;
            pos.transform.rotation = Quaternion.Euler (fpsCam.transform.rotation.x, camHolder.transform.rotation.y, 0);
            hitTextGameObjectL.transform.rotation = fpsCam.transform.rotation;
            hitTextGameObjectL.SetActive(true);
            Destroy(hitTextGameObjectL, 1f);
        }
        }
    }

    public void GetTextPosition () {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit)){
            pos.transform.position = hit.point;
            pos.transform.localScale = new Vector3 (hit.distance / 13, hit.distance / 13, 1);
        }
    }
}
