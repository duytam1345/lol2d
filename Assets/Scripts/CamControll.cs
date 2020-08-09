using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControll : MonoBehaviour
{
    [SerializeField]
    float offset = 20f;
    [SerializeField]
    Transform character;
    [SerializeField]
    Transform cam;
    [SerializeField]
    float speed;
    [SerializeField]
    float speedMoveByMouse;
    enum eFollow
    {
        Player,
        Mouse
    }
    enum eDirMove
    {
        None,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }
    [SerializeField]
    eFollow follow = eFollow.Mouse;
    [SerializeField]
    eDirMove dirMove = eDirMove.None;
    void Update()
    {
        if (InputManager.SwiftCamMode)
        {
            SwitchFllow();
        }
        if (follow == eFollow.Player)
        {
            FollowPlayer();
        }
        else
        {
            FollowMouse();
        }
    }
    private void SwitchFllow()
    {
        if (follow == eFollow.Mouse)
        {
            follow = eFollow.Player;
            Vector3 pos = character.position;
            pos.z = -10;
            cam.transform.position = pos;
        }
        else
        {
            follow = eFollow.Mouse;
        }
    }
    private void FollowPlayer()
    {
        Vector3 pos = Vector2.MoveTowards(cam.position, character.position, speed * Time.deltaTime);
        pos.z = -10;
        cam.transform.position = pos;
    }
    private void FollowMouse()
    {
        var mousePos = Input.mousePosition;
        dirMove = eDirMove.None;
        if (mousePos.y > Screen.height - offset)
        {
            dirMove = eDirMove.MoveUp;
        }
        else
        {
            if (mousePos.y < offset)
            {
                dirMove = eDirMove.MoveDown;
            }
            else
            {
                if (mousePos.x < offset)
                {
                    dirMove = eDirMove.MoveLeft;
                }
                else
                {
                    if (mousePos.x > Screen.width - offset)
                    {
                        dirMove = eDirMove.MoveRight;
                    }
                }
            }
        }
        if (dirMove != eDirMove.None)
        {
            Vector3 moveAdd = Vector3.zero;
            switch (dirMove)
            {
                case eDirMove.MoveUp:
                    moveAdd = Vector3.up;
                    break;
                case eDirMove.MoveDown:
                    moveAdd = -Vector3.up;
                    break;
                case eDirMove.MoveLeft:
                    moveAdd = -Vector3.right;
                    break;
                case eDirMove.MoveRight:
                    moveAdd = Vector3.right;
                    break;
            }
            cam.position += moveAdd * speedMoveByMouse * Time.deltaTime;
        }
    }
}
