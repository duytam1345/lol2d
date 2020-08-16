using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnItem : MonoBehaviour
{
    public static System.Action<SlotInventory> KhiTuiDoClick;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(
        ()=>{
            KhiTuiDoClick?.Invoke(GetComponent<SlotInventory>());
        });
    }
}
