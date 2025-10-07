using Godot;
using System;

public partial class MainCamera : Camera3D
{
    [Export] public Node3D target;
    [Export] public float ZoomLerpSpeed = 3f;
    [Export] public float minZoom = 1f;
    [Export] public float maxZoom = 1.5f;
    [Export] public float maxSpeed = 400f;
    [Export] public Vector3 offset;

    private Vector3 initialOffset = Vector3.Zero;
    private float initialY = 0;
    private Vector3 defaultZoom = Vector3.One;

    public override void _Ready()
    {
        base._Ready();
        initialOffset = target.Position + offset;
        Position = initialOffset;
        initialY = Position.Y;
    }

    public override void _Process(double _delta)
    {
        if (target == null)
        {
            return;
        }
        FollowTarget();
    }

    public void FollowTarget()
    {
        Position = new Vector3(offset.X, target.Position.Y, target.Position.Z + offset.Z);
    }
}
