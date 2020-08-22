using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    public GameObject gTarget;

    [SerializeField]
    float offsetY;

    private void FixedUpdate()
    {
        Vector2 p = Camera.main.WorldToScreenPoint(gTarget.transform.position);
        p.y += offsetY;

        GetComponent<RectTransform>().position = p;
    }
}
