using Godot;
using System;
using System.Numerics;

public partial class PlayerController : CharacterBody3D
{
    [Export] Node3D focusPoint;
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
    }

    public void MoveTowards(Node3D target, double delta)
    {
        float t = (float)(lerpSpeed * delta);
        Position = new Godot.Vector3(
            Mathf.Lerp(Position.X, target.Position.X, t),
            Mathf.Lerp(Position.Y, target.Position.Y, t),
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
}