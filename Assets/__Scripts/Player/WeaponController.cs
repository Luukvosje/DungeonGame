using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponRenderer;

    private Vector3 oldPos, newPos, oldRot, newRot;



    public void ChangeWeaponPos(int newLook)
    {
        oldPos = weaponRenderer.transform.localPosition;
        oldRot = weaponRenderer.transform.eulerAngles;
        switch (newLook)
        {
            case 0:

                //Left
                newPos = new Vector3(-1, 0);
                newRot = new Vector3(0, 0, 90);
                break;
            case 1:
                //Right
                newPos = new Vector3(1, 0);
                newRot = new Vector3(0, 0, -90);
                break;
            case 2:
                //front
                newPos = new Vector3(0, 1);
                newRot = new Vector3(0, 0, 0);
                break;
            case 3:
                //back
                newPos = new Vector3(0, -1);
                newRot = new Vector3(0, 0, 180);
                break;
        }
        StartCoroutine(ChangePos());
    }

    private IEnumerator ChangePos()
    {
        float duration = 0.1f;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            weaponRenderer.transform.localPosition = Vector2.Lerp(oldPos, newPos, currentTime / duration);
            weaponRenderer.transform.eulerAngles = Vector3.Lerp(oldRot, newRot, currentTime / duration);
            yield return null;
        }
    }
}
