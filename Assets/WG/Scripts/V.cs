using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WG_SoundManager.instance.audioSource_Shot.volume = 0.2f;
        PlayerControlManager.Instance.GetPlayer().GetComponent<BoxCollider2D>().enabled = true;

    }
}
