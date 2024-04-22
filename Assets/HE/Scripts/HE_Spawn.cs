using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HE_Spawn : MonoBehaviour
{
    public float Delay = 1;
    public float stopSpawn = 15;

    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Boss;
    [SerializeField] Transform Bosspos;
    [SerializeField] GameObject textWarning; //워닝 텍스트    
    bool swi = true;

    void Start()
    {
        StartCoroutine("SpawnEnemy1");
        Invoke("Stop1", stopSpawn);
    }
    IEnumerator SpawnEnemy1()
    {
        WaitForSeconds waitTime = new WaitForSeconds(Delay);
        while (swi)
        {
            yield return waitTime;
            float randomX = Random.Range(-4.61f, 4.61f);
            Vector3 vec = new Vector3(randomX, transform.position.y, 0f);
            Instantiate(Enemy1, vec, Quaternion.identity);
        }
    }

    void Stop1()
    {
        swi = false;
        StopCoroutine("SpawnEnemy1");
        /* boss warning */
        Invoke("showWarningBar", 2f);  
        Invoke("CreateBoss", 3f);

 

    }
    void showWarningBar()
    {
        textWarning.SetActive(true);
        StartCoroutine(DisableTextWarning()); //텍스트 비활성화     
    }
    void CreateBoss()
    {
        Instantiate(Boss, Bosspos.position, Quaternion.identity);
    }

    IEnumerator DisableTextWarning()
    {
        yield return new WaitForSeconds(1.0f);
        textWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
