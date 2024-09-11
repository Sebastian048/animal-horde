using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarRotation : MonoBehaviour
{
    [SerializeField] private float _reduceSpeed = 2;
    private float _target = 1;
    private Transform _cam;

    void Start () {
        _cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void FixedUpdate () {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}
