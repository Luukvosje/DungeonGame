using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponRenderer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeaponPos();
    }

    void ChangeWeaponPos()
    {
        switch (PlayerMovement.lookingDir)
        {
            case 0:

                //Left
                weaponRenderer.transform.localPosition = new Vector3(-1, 0);
                weaponRenderer.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case 1:
                //Right
                weaponRenderer.transform.localPosition = new Vector3(1, 0);
                weaponRenderer.transform.eulerAngles = new Vector3(0, 0, -90);
                break;
            case 2:
                //Back
                weaponRenderer.transform.localPosition = new Vector3(0, -1);
                weaponRenderer.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case 3:
                //Front
                weaponRenderer.transform.localPosition = new Vector3(0, 1);
                weaponRenderer.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }
}
