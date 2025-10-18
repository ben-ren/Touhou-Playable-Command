using Godot;
using System;

public partial class CameraController : Camera3D
{
    [Export] Node3D target;
    //[Export] Node3D focusPoint;
    [Export] public float rotationSpeed = 90f; // degrees per second

    public override void _PhysicsProcess(double delta)
    {
        RotateTowards(target, delta);
    }
    
    // Call this function to rotate toward a target node
    // Call this function to rotate toward a target node
    public void RotateTowards(Node3D target, double delta)
    {
        if (target == null) return;

        // Direction to target
        Vector3 direction = (target.GlobalPosition - GlobalPosition).Normalized();

        // Get desired rotation basis (static call)
        Basis targetBasis = Basis.LookingAt(direction, Vector3.Up);

        // Smoothly interpolate current rotation toward target
        GlobalTransform = new Transform3D(
            GlobalTransform.Basis.Slerp(targetBasis, (float)(rotationSpeed * delta)),
            GlobalTransform.Origin
        );
    }
}
