using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecallObject : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = transform.GetChild(2).GetComponent<Image>();
    }

    private void Update()
    {
        if (image.fillAmount < 1)
        {
            image.fillAmount += (1 / 4.5f)*Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
