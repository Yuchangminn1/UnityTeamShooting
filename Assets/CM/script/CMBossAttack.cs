using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CMBossAttack : MonoBehaviour
{
    public GameObject MBullet;      //몬스터 불렛 프리팹
    public TBoss tBoss;
    public int BulletNum = 10;           //1번 패턴 총알 수
    public int BulletNum2 = 20;     //2번 패턴 총알 수 
    public int BulletNum3 = 10;     //3번 패턴 총알 수 
    public float BulletNum4 = 1.7f;     //3번 패턴 총알 수 


    public int coroutineCount1 = 500; //1번 패턴 반복 수
    public int coroutineCount2 = 500; //2번 패턴 반복 수
    public int coroutineCount3 = 500; //2번 패턴 반복 수
    public int coroutineCount4 = 500; //2번 패턴 반복 수


    public Vector3 attPointV;
    //public float CALCX;


    public float CalcXPlus = 0.01f; //계산 범위 작아질수록 더 많이 계산

    public float Myr2 = 3; //r 반지름

    public float Myr3 = 3; //r 반지름


    public bool Shot2End = false;  //한바퀴 다 돌면 끝낼려고 추가함
    public bool Shot3End = false;  //한바퀴 다 돌면 끝낼려고 추가함


    public bool Shot2StopC = false; // 코르틴 끝내기용
    public bool Shot3StopC = false; // 코르틴 끝내기용
    public bool Shot4StopC = false; // 코르틴 끝내기용


    public int Shot1Time = 200; // 딜레이
    public int Shot2Time = 200; // 딜레이
    public int Shot3Time = 200; // 
    public int Shot4Time = 200; // 

    public float Shot1Dleay = 0.5f;
    public float Shot2Dleay = 2f; // 딜레이
    public float Shot3Dleay = 5f; // 
    public float Shot4Dleay = 5f; // 

    public float ShotStart1Dleay = 0.5f;
    public float ShotStart2Dleay = 2f; // 딜레이
    public float ShotStart3Dleay = 10f; // 
    public float ShotStart4Dleay = 5f; // 

    public float Shot1Speed = 1f;
    public float Shot2Speed = 3f;   //패턴2 총알 속도
    public float Shot3Speed = 3f;   //패턴3 총알 속도
    public float Shot4Speed = 3f;   //패턴3 총알 속도

    public int CAttackType = 0;

    public float MyRad;

    public GameObject BigBoom;

    //Shot3 용도
    //GameObject[] Q3 = { null, null, null, null };

    //Shot4 용도
    GameObject[] Q1 = { null, null, null, null };
    GameObject[] Q2 = { null, null, null, null };

    public GameObject CMYY;


    // Start is called before the first frame update
    void Start()
    {
        if (CAttackType == 1) { StartCoroutine("S1", 1f); }
        else if (CAttackType == 2) { StartCoroutine("S2", 1f); }
        else if (CAttackType == 3) { StartCoroutine("S3", 1f); }
        else if (CAttackType == 4) { StartCoroutine("S4", 1f); }


    }
    private void Update()
    {

        //if (coroutineCount1 > Shot1Time)
        //{
        //    Debug.Log("StopCoroutineS1");
        //    StopCoroutine("S1"); ;
        //    coroutineCount1 = 0;
        //}
        //if (coroutineCount2 > Shot2Time)
        //{
        //    Debug.Log("StopCoroutineS2");
        //    StopCoroutine("S2"); ;
        //    coroutineCount2 = 0;
        //}
        //if (coroutineCount3 > Shot3Time)
        //{
        //    Debug.Log("StopCoroutineS3");
        //    StopCoroutine("S3"); 
        //    coroutineCount3 = 0;
        //}
        //if (coroutineCount4 > Shot4Time)
        //{
        //    Debug.Log("StopCoroutineS4");
        //    StopCoroutine("S4");
        //    coroutineCount4 = 0;
        //}
    }
    IEnumerator S1()
    {
        yield return new WaitForSeconds(ShotStart1Dleay);
        while (true)
        {
            Shot1();
            coroutineCount1++;
            if (tBoss.dieBossNum == 0) yield return new WaitForSeconds(Shot1Dleay);

            else yield return new WaitForSeconds(Shot1Dleay / (2f * tBoss.dieBossNum));
        }
    }

    IEnumerator S2()
    {
        yield return new WaitForSeconds(ShotStart2Dleay);
        while (true)
        {
            Shot2();
            coroutineCount2++;
            yield return new WaitForSeconds(Shot2Dleay);
        }
    }
    IEnumerator S3()
    {
        yield return new WaitForSeconds(ShotStart3Dleay);
        while (true)
        {
            float Vx = UnityEngine.Random.Range(-2.0f, 2.0f);
            float Vy = UnityEngine.Random.Range(-4.0f, 0f);
            Vector3 tmp = new Vector3(transform.position.x + Vx, transform.position.y + Vy, 0f);
            Instantiate(CMYY, tmp, Quaternion.identity);
            Shot3(tmp);
            coroutineCount3++;
            if (tBoss.dieBossNum == 0) yield return new WaitForSeconds(Shot3Dleay);

            else yield return new WaitForSeconds(Shot3Dleay / (2f * tBoss.dieBossNum));
        }
    }
    IEnumerator S4()
    {
        yield return new WaitForSeconds(ShotStart4Dleay);
        while (true)
        {
            
            GameObject BigBullet = Instantiate(BigBoom, transform.position, Quaternion.identity);
            BigBullet.transform.localScale = new Vector2(3, 3);
            BigBullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5f;
            yield return new WaitForSeconds(3f);
            BigBullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log(BigBullet.transform.position);
            while (MyRad <= 1.2f)
            {
                
                Shot4(BigBullet.transform);
                yield return new WaitForSeconds(0.05f);

            }
            Destroy(BigBullet);
            if (tBoss.dieBossNum == 0) yield return new WaitForSeconds(Shot4Dleay);

            else yield return new WaitForSeconds(Shot4Dleay / (4f * tBoss.dieBossNum));
            MyRad = 0f;
            coroutineCount4++;

        }
    }
    void Shot1()
    {

        for (int i = 0; i < BulletNum; i++)
        {
            GameObject Q = Instantiate(MBullet, transform.position, Quaternion.identity);
            //Q.GetComponent<Rigidbody2D>().velocity = new Vector2((-(2f * BulletNum / 2) + 2f * i), -1.0f * Time.deltaTime * Shot1Speed);
            Q.GetComponent<Rigidbody2D>().velocity = new Vector2((-(BulletNum / 2  )  +  i), -1.0f * Time.deltaTime * Shot1Speed);

        }
    }
    void Shot2()
    {
        float Vx = UnityEngine.Random.Range(-5.0f, 5.0f);
        float Vy = UnityEngine.Random.Range(-5.0f, 5.0f);
        attPointV = new Vector3(Vx, Vy, 0);

        float CalcX = Myr2;                        //시작 x 값

        int GAnglePlus = 360 / BulletNum2;      //등차 각도 
        int GGAngle = 0;
        Shot2End = false;                   //끝내기bool
        while (CalcX > -Myr2)
        {
            double Y = Calc(CalcX, Myr2);
            double GAngle = (double)Mathf.Rad2Deg * (Math.Atan(Y / (double)CalcX));


            if ((float)GAngle != float.NaN && (int)GAngle == GGAngle) //&& (int)GAngle == GGAngle 
            {
                GameObject Q = Instantiate(MBullet, new Vector2(attPointV.x + CalcX, (float)Y + attPointV.y), Quaternion.identity);
                Q.GetComponent<Rigidbody2D>().velocity = (Q.transform.position - attPointV) * Shot2Speed;
                GameObject Q2 = Instantiate(MBullet, new Vector2(attPointV.x + CalcX, -(float)Y + attPointV.y), Quaternion.identity);
                Q2.GetComponent<Rigidbody2D>().velocity = (Q2.transform.position - attPointV) * Shot2Speed;
                GameObject Q3 = Instantiate(MBullet, new Vector2(attPointV.x - CalcX, (float)Y + attPointV.y), Quaternion.identity);
                Q3.GetComponent<Rigidbody2D>().velocity = (Q3.transform.position - attPointV) * Shot2Speed;
                GameObject Q4 = Instantiate(MBullet, new Vector2(attPointV.x - CalcX, -(float)Y + attPointV.y), Quaternion.identity);
                Q4.GetComponent<Rigidbody2D>().velocity = (Q4.transform.position - attPointV) * Shot2Speed;
                GGAngle += GAnglePlus;
            }
            else
            {
                if (GGAngle >= 90) { break; }

            }
            CalcX -= CalcXPlus;
        }

        ++coroutineCount3;
    }

    void Shot3(Vector3 tmp)
    {
        float CalcX = Myr3;                        //시작 x 값

        int GAnglePlus = 360 / BulletNum3;      //등차 각도 
        int GGAngle = 0;
        Shot3End = false;                   //끝내기bool

        while (CalcX > -Myr3)
        {
            double Y = Calc(CalcX, Myr3);
            double GAngle = (double)Mathf.Rad2Deg * (Math.Atan(Y / (double)CalcX));


            if ((float)GAngle != float.NaN && (int)GAngle == GGAngle) //&& (int)GAngle == GGAngle 
            {


                GameObject Q = Instantiate(MBullet, new Vector2(tmp.x + CalcX, (float)Y + tmp.y), Quaternion.identity);
                Q.GetComponent<Rigidbody2D>().velocity = (tmp - Q.transform.position) * Shot3Speed;
                //  Q.transform.Rotate(0f, 0f, -(float)GAngle);


                GameObject Q2 = Instantiate(MBullet, new Vector2(tmp.x + CalcX, -(float)Y + tmp.y), Quaternion.identity);
                Q2.GetComponent<Rigidbody2D>().velocity = (tmp - Q2.transform.position) * Shot3Speed;
                // Q2.transform.Rotate(0f, 0f, -(float)GAngle);


                GameObject Q3 = Instantiate(MBullet, new Vector2(-CalcX + tmp.x, (float)Y + tmp.y), Quaternion.identity);

                Q3.GetComponent<Rigidbody2D>().velocity = (tmp - Q3.transform.position) * Shot3Speed;
                // Q3.transform.Rotate(0f, 0f, -(float)GAngle);


                GameObject Q4 = Instantiate(MBullet, new Vector2(tmp.x - CalcX, -(float)Y + tmp.y), Quaternion.identity);

                Q4.GetComponent<Rigidbody2D>().velocity = (tmp - Q4.transform.position) * Shot3Speed;
                //   Q4.transform.Rotate(0f, 0f, (float)GAngle);


                GGAngle += GAnglePlus;
            }
            else
            {
                if (GGAngle >= 90) { break; }

            }
            CalcX -= CalcXPlus;
        }

        ++coroutineCount3;
    }

    void Shot4(Transform TargetTransform)
    {
        Vector3 tmp = new Vector3(math.tan(MyRad) * 0.7f, math.sin(MyRad) * 3f, 0) / BulletNum4;
        Vector3 tmp2 = new Vector3(math.sin(MyRad) * 3f, math.tan(MyRad) * 0.7f, 0) / BulletNum4;


        for (int i = 0; i < 4; i++)
        {
            Q1[i] = Instantiate(MBullet, tmp + TargetTransform.position, Quaternion.identity);
            Destroy(Q1[i], 3f);
            Q1[i].GetComponent<Rigidbody2D>().velocity = Vector2.up * Shot4Speed;
            Q2[i] = Instantiate(MBullet, tmp2 + TargetTransform.position, Quaternion.identity);
            Destroy(Q2[i], 3f);
            Q2[i].GetComponent<Rigidbody2D>().velocity = Vector2.up * Shot4Speed;
            if (i == 1) { tmp.x *= -1; tmp2.x *= -1; }
            else { tmp.y *= -1; tmp2.y *= -1; }

        }


        MyRad += 0.1f;


    }


    double Calc(float X, float Myr)
    {
        //myr = 반지름 
        //X   = 들어오는 X값 

        double Y = 0f;

        double disX = (double)X;

        double disY = (double)(Math.Pow(Myr, 2) - Math.Pow(disX, 2));

        Y = Math.Sqrt(disY);

        return Y;
    }

}
