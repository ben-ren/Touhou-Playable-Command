using Godot;
using System;

public partial class Player_Controller : Node3D
{
    private float forwardSpeed = 20.0f;
    private float strafeSpeed = 10.0f;
    private Vector3 velocity = Vector3.Forward;
    float forceX, forceY, forceZ;

    public override void _PhysicsProcess(double delta)
    {
        Controls();
        AppliedForces(forceX, forceY, forceZ);
    }

    public void Controls()
    {
        forceX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        forceY = Input.GetActionStrength("ui_up") - Input.GetActionStrength("ui_down");
    }

    public void AppliedForces(float X, float Y, float Z)
    {
        Position += new Vector3(X, Y, Z-1f);
    }
}