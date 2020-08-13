using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tuong : MonoBehaviour
{

    public virtual void Chieu1() { }
    public virtual void Chieu2() { }
    public virtual void Chieu3() { }
    public virtual void Chieu4() { }
}

public class ControlPlayer
{
    Tuong player;
    public void ChonTuong(string name)
    {
        switch (name)
        {
            case "ya":
                player = (Resources.Load("") as GameObject).GetComponent<Yasou>();
                break;
            case "":
                player = (Resources.Load("") as GameObject).GetComponent<MOf>();
                break;
            default:
                //player = (Resources.Load("") as GameObject).GetComponent<YasouX>();
                break;
        }
    }
    public void NhanQ()
    {
        player.Chieu1();
    }
}
public class MOf : Tuong
{
    public override void Chieu1()
    {

    }
}
public class Ashe : Tuong
{

}
public class YasouX
{

}
public class Yasou : Tuong
{
    public override void Chieu1()
    {
    }
    public override void Chieu3()
    {
    }
}