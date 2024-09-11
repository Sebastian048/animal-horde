using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGameModes : MonoBehaviour
{
    public GameObject woodWall;
    public GameObject[] targets;
    
    int chosenTarget;

    public void TargetDown (float targetDowned) {
        chosenTarget = Random.Range(0, targets.Length);
        while (targets[chosenTarget].activeSelf == true) {
            chosenTarget = Random.Range(0, targets.Length);
        }
        while (chosenTarget == targetDowned) {
            chosenTarget = Random.Range(0, targets.Length);
        }
        targets[chosenTarget].transform.GetChild(0).GetComponent<Enemy>().health = 1;
        targets[chosenTarget].SetActive(true);
    }
}