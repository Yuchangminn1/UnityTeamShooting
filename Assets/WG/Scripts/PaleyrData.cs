using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaleyrData : MonoBehaviour
{
    [SerializeField] float PowerLevel = 1f;
    [SerializeField] float ATK = 0f;
    public float Power_p {  get { return PowerLevel; } set { PowerLevel = value; } }
    public float ATK_p {  get { return ATK; }}
    private void Update()
    {
        if (PowerLevel >= 5) PowerLevel = 5;
        ATK = PowerLevel * 10;
    }
}
