using Godot;
using System;

public partial class HomeNode : Node3D
{
    [Export] public float speed = 5f;
    private Vector3 velocity = Vector3.Zero;

    public override void _Ready()
    {
        velocity = new Vector3(0,0,-speed);
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += velocity * (float)delta;
    }
}
