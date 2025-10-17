using Godot;
using System;
using System.Numerics;

public partial class PlayerController : CharacterBody3D
{
    [Export] Node3D focusPoint;
    [Export] Camera3D camera;
    [Export] double lerpSpeed;
    [Export] float MoveSpeed;
    [Export] public float X_Axis_Min_Range;
    [Export] public float X_Axis_Max_Range;
    [Export] public float Y_Axis_Min_Range;
    [Export] public float Y_Axis_Max_Range;

    private Godot.Vector3 velocity = Godot.Vector3.Zero;


    public override void _PhysicsProcess(double delta)
    {
        MoveTowards(focusPoint, delta);
        InputControls(delta);
        PlayerRangeLimit();

        // Detect collisions using MoveAndCollide if you want to check collisions manually
        KinematicCollision3D collision = MoveAndCollide(Godot.Vector3.Zero);
        if (collision != null)
        {
            OnCollision(collision);
        }
    }

    public void MoveTowards(Node3D target, double delta)
    {
        Godot.Vector3 projectedTarget = CalculatePlayerPosition(target, camera, Position.Z);

        float t = (float)(lerpSpeed * delta);

        Position = new Godot.Vector3(
            Mathf.Lerp(Position.X, projectedTarget.X, t),
            Mathf.Lerp(Position.Y, projectedTarget.Y, t),
            Position.Z
        );
    }

    public void InputControls(double delta)
    {
        velocity = new Godot.Vector3(
            InputAxis("ui_right", "ui_left"),
            InputAxis("ui_up", "ui_down"),
            0
            );
        Position += velocity * MoveSpeed;
    }

    /*
    *  Calculates the InputAxis as a range from -1 to 1
    */
    public float InputAxis(String positive_input, String negative_input)
    {
        return Godot.Input.GetActionRawStrength(positive_input)
        - Godot.Input.GetActionRawStrength(negative_input);
    }

    /*
    * Set's Player movement range based on Range values
    * (TODO) Set range values based on Camera viewport
    */
    public void PlayerRangeLimit()
    {
        Position = new Godot.Vector3(
            Godot.Mathf.Clamp(Position.X, X_Axis_Min_Range, X_Axis_Max_Range),
            Godot.Mathf.Clamp(Position.Y, Y_Axis_Min_Range, Y_Axis_Max_Range),
            Position.Z
        );
    }

    public Godot.Vector3 CalculatePlayerPosition(Node3D target, Node3D camera, float playerZ)
    {
        float deltaZ = target.Position.Z - camera.Position.Z;

        // Avoid division by zero if camera and target Z are the same
        if (Mathf.IsEqualApprox(deltaZ, 0f))
            return new Godot.Vector3(target.Position.X, target.Position.Y, playerZ);

        // Fraction along the line from target back toward camera at the desired Z
        float factor = (target.Position.Z - playerZ) / deltaZ;

        // Interpolate X and Y along the line
        float x = target.Position.X - (target.Position.X - camera.Position.X) * factor;
        float y = target.Position.Y - (target.Position.Y - camera.Position.Y) * factor;

        return new Godot.Vector3(x, y, playerZ);
    }

    private void OnCollision(KinematicCollision3D collision)
    {
        Node3D collider = collision.GetCollider() as Node3D;
        if (collider != null)
        {
            GD.Print("Collided with: " + collider.Name);
        }
    }
}