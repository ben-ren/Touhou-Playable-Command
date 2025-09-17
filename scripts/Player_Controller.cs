using Godot;
using System;

public partial class Player_Controller : CharacterBody3D
{
    private float forwardSpeed = 20.0f;
    private float strafeSpeed = 10.0f;
    private Vector3 velocity = Vector3.Zero;

    public override void _PhysicsProcess(double delta)
    {
        // Constant forward movement (negative Z is forward)
        velocity.Z = -forwardSpeed;

        // Left/right strafing input
        float inputX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        velocity.X = inputX * strafeSpeed;

        // No gravity, so vertical velocity stays zero
        velocity.Y = 0;

        // Move the character
        Velocity = velocity;
        MoveAndSlide();
    }
}