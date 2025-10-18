using Godot;
using System;

public partial class HomeNode : Node3D
{
    [Export] public float speed = 5f;
    private Vector3 velocity = Vector3.Zero;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {
        //MoveForward(delta);
    }

    public void MoveForward(double delta)
    {
        // Move along the node's *local forward direction* (-Z)
        Vector3 forward = Transform.Basis.Z;

        // Update position relative to current rotation
        Position += forward * speed * (float)delta;
    }
}
