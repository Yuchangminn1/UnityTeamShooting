using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    Vector3 CameraStartPosition;
    [Range(0.01f, 100f)] public float ShakeRange = 1f;
    float ShakeRange_Mirror;
    [Range(0.01f, 100f)] public float ShakeSpeed = 50f;
    [Range(0.01f, 1000f)] public float Duration = 1f;
    public bool ShakeEnd = true;
    float timer;
    void Start()
    {
        MainCamera.transform.position = new Vector3(0, 0, -10);
        CameraStartPosition = MainCamera.transform.position;
        timer += Time.deltaTime;
        ShakeRange_Mirror = ShakeRange;
    }
    public IEnumerator Shaking()
    {
        ShakeRange = ShakeRange_Mirror;
        //�ܺο��� while ���� Ű�� �������� 
        while (!ShakeEnd)
        {
            float reductionFactor = 1f - (timer / Duration);
            ShakeRange *= reductionFactor;
            //Random.value = 0~1 ���� float��
            float CameraPosition_X = Random.value * ShakeRange * 2 - ShakeRange;
            float CameraPosition_Y = Random.value * ShakeRange * 2 - ShakeRange;

            Vector3 shakeOffset = new Vector3(CameraPosition_X, CameraPosition_Y, MainCamera.transform.position.z);

            MainCamera.transform.position = shakeOffset;
            //WaitForSceods ������ �ڷ�ƾ ����ϴ� ����� Stop�Ȱɾ ��
            yield return new WaitForSeconds(1 / ShakeSpeed);
        }

    }
    public void StartShaking_CorRoutine()
    {
        ShakeEnd = false;
        StartCoroutine(Shaking());
    }
    public void StopShaking()
    {
        ShakeEnd = true;
        MainCamera.transform.position = CameraStartPosition;
    }
}
