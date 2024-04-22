using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMBossAP : MonoBehaviour
{
    float count = 0;
    bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one * 0.1f;
        StartCoroutine(AP());
        //Invoke("SP", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AP()
    {
        while (transform.localScale.x < 1.5f)
        {
            transform.position = new Vector3(0f, count + 1f, 0f);
            count += 0.1f;
            transform.localScale = Vector3.one * (0.1f * count + 1);
            yield return new WaitForSeconds(0.03f);

        }
        yield return null;

    }
    void SP()
    {
        StopCoroutine(AP());
        stop = true;

    }
}
