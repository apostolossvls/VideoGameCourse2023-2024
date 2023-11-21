using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArmatureLookCamera : MonoBehaviour
{
    public Transform cam;
    public Transform[] pivots;
    [Range(0f, 360f)] public float maxAngleY = 90f;
    [Range(0f, 1f)] public float amount = 1;
    [Range(0.1f, 10f)] public float snapSpeed = 1;

    private void LateUpdate()
    {
        float anglePerPivot = maxAngleY / pivots.Length;

        for (int i = 0; i < pivots.Length; i++)
        {
            Quaternion rot = Quaternion.Lerp(pivots[i].localRotation, cam.rotation, anglePerPivot * amount);
            Vector3 rotEuler = rot.eulerAngles;
            Debug.Log(Quaternion.Dot(pivots[i].rotation, cam.rotation));
            float dot = Quaternion.Dot(pivots[i].rotation, cam.rotation);
            if (1 - dot > maxAngleY / 360f)
            {
                rotEuler.y = pivots[i].localRotation.eulerAngles.y;
            }

            pivots[i].localRotation = Quaternion.Euler(rotEuler);
            //pivots[i].localRotation *= Quaternion.Lerp(pivots[i].localRotation, Quaternion.Euler(rotEuler), Time.deltaTime / snapSpeed);
        }

    }
}
