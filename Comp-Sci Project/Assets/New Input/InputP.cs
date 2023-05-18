using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputP : MonoBehaviour
{
    private StandardControls controls;
    public static InputP singleton;
    void Awake()
    {
        if (singleton == null) singleton = this;
        controls = new StandardControls();
        inputs = new Inputs();
    }

    public static Inputs inputs;
    void Update()
    {
        inputs.space = controls.Player.Jump.WasPressedThisFrame();
        inputs.spaceDown = controls.Player.Jump.WasPerformedThisFrame();

        inputs.mousePos = controls.Player.Mouse.ReadValue<Vector2>();
        inputs.mouseDelta = controls.Player.MouseDelta.ReadValue<Vector2>();
        inputs.wasd = controls.Player.Movement.ReadValue<Vector2>();

        inputs.run = controls.Player.Run.WasPressedThisFrame();

        inputs.control = controls.Player.Crouch.WasPerformedThisFrame();
        inputs.controlThisFrame = controls.Player.Crouch.WasPressedThisFrame();
    }

    private void OnEnable()
    {
        if(controls == null) controls = new StandardControls();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

public struct Inputs
{
    public Vector2 mousePos;
    public Vector2 mouseDelta;
    public Vector2 wasd;
    public bool space;
    public bool spaceDown;
    public bool run;
    public bool control;
    public bool controlThisFrame;
}
