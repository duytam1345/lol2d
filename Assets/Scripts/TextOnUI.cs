using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnUI : MonoBehaviour
{
    public Vector2 pos;

    RectTransform rect;

    private void Start()
    {
        pos = Camera.main.ScreenToWorldPoint(pos);
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.transform.position = Camera.main.WorldToScreenPoint(pos);
    }
}
