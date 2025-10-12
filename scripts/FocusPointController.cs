using Godot;
using System;

public partial class FocusPointController : Node3D
{
    [Export] Camera3D camera;
    private float depth = 0f;

    public override void _Ready()
    {
        depth = camera.Position.Z - this.Position.Z;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        Godot.Vector2 mousePos = GetViewport().GetMousePosition();
        Godot.Vector3 worldPos = camera.ProjectPosition(mousePos, depth);
        Position = new Vector3(worldPos.X, worldPos.Y-70, this.Position.Z);
    }
}
