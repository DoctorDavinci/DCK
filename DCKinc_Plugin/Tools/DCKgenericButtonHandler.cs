﻿using UnityEngine;

/// <summary>
/// For single buttons in a part, like switches.
/// Assign this to a collider with isTrigger.
/// Activates the function assigned to mouseDownFunction.
/// </summary>
class DCKgenericButtonHandler : MonoBehaviour
{
    public delegate void MouseDownFunction();
    /// <summary>
    /// A function in the form buttonClick()
    /// </summary>
    public MouseDownFunction mouseDownFunction;
    public void OnMouseDown()
    {
        mouseDownFunction();
    }
}

