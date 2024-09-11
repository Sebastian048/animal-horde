using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarkerManager : MonoBehaviour
{
    public GameObject hitMarker;
    public GameObject canvas;
    public Vector3 pos;

    public bool canHit;

    void Start () {
        pos = canvas.GetComponent<RectTransform>().position;
    }


    public void HitMarkerSpawn () {
        GameObject hitGameObject = Instantiate (hitMarker);
        hitGameObject.transform.SetParent (canvas.transform);
        hitGameObject.GetComponent<RectTransform>().sizeDelta = hitMarker.GetComponent<RectTransform>().sizeDelta;
        hitGameObject.GetComponent<RectTransform>().localScale = hitMarker.GetComponent<RectTransform>().localScale;
        hitGameObject.GetComponent<RectTransform>().position = new Vector3 (Random.Range(pos.x - 5, pos.x + 5), Random.Range(pos.y - 5, pos.y + 5), 0);
        hitGameObject.gameObject.SetActive(true);
        Destroy(hitGameObject, 0.5f);
    }
}
