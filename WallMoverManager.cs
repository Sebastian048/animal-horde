using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoverManager : MonoBehaviour
{
    public GameObject woodWall;

    public float distanceToMove;
    public float fixedDistance;

    void FixedUpdate () {
        if (distanceToMove <= fixedDistance) {
            distanceToMove = distanceToMove + 15 * Time.deltaTime;
        }

        if (distanceToMove >= fixedDistance) {
            distanceToMove = distanceToMove - 15 * Time.deltaTime;
        }

        woodWall.transform.localPosition = new Vector3 (0, 0, distanceToMove + 15);
    }

    public void MoveWall (float distance) {
        fixedDistance = distance;
    }
}
