using Godot;
using System;
using System.Numerics;

public partial class PlayerController : CharacterBody3D
{
    [Export] Node3D focusPoint;
    [Export] Camera3D camera;
    [Export] double lerpSpeed;
    [Export] float MoveSpeed;

    private Godot.Vector3 velocity = Godot.Vector3.Zero;


    public override void _PhysicsProcess(double delta)
    {
        MoveTowards(focusPoint, delta);
        InputControls(delta);

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
    * calculates a target position at a set Z value between two points. 
    * Used to calculate the target position for the MoveToward function.
    */
    public Godot.Vector3 CalculatePlayerPosition(Node3D target, Node3D camera, float distance_along_line)
    {
        float deltaZ = target.Position.Z - camera.Position.Z;

        // Avoid division by zero if camera and target Z are the same
        if (Mathf.IsEqualApprox(deltaZ, 0f))
            return new Godot.Vector3(target.Position.X, target.Position.Y, distance_along_line);

        // Fraction along the line from target back toward camera at the desired Z
        float factor = (target.Position.Z - distance_along_line) / deltaZ;

        // Interpolate X and Y along the line
        float x = target.Position.X - (target.Position.X - camera.Position.X) * factor;
        float y = target.Position.Y - (target.Position.Y - camera.Position.Y) * factor;

        return new Godot.Vector3(x, y, distance_along_line);
    }

    /*
    * Collision Logic handler
    */
    private void OnCollision(KinematicCollision3D collision)
    {
        Node3D collider = collision.GetCollider() as Node3D;
        if (collider != null)
        {
            GD.Print("Collided with: " + collider.Name);
        }
    }
}