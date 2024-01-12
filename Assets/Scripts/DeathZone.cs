using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ColorSwitch
{
    public class DeathZone : MonoBehaviour
{
    [SerializeField] private Transform top;
    [SerializeField] private float _offsetY;

    private void Update()
    {
        if (top.position.y >= Camera.main.transform.position.y)
        {

            transform.position = new Vector3(0, top.position.y - _offsetY, 0);
        }
    }
}
}