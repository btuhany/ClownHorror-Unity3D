using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcInput //: IInput
{
    public float VerticalAxis => Input.GetAxis("Vertical");
    public float HorizontalAxis => Input.GetAxis("Horizontal");
    public bool Jump => Input.GetButtonDown("Jump");
    public bool Sprint => Input.GetKey(KeyCode.LeftShift);
    public bool Flashlight => Input.GetKeyDown(KeyCode.F);
}
