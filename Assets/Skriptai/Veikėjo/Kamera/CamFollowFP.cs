using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowFP : MonoBehaviour
{

    Vector3 movement;
    Vector2 mouseLook;
    Vector2 smoothV;
    float sensitivity = 5.0f;
    float smoothing = 2.0f;


    void FixedUpdate()
    {
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            gameObject.transform.parent.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, gameObject.transform.parent.transform.up);

    }
}
