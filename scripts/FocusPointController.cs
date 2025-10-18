using Godot;
using System;

public partial class FocusPointController : Node3D
{
    [Export] Camera3D camera;

    public float depth = 0f;
    Godot.Vector3 ViewportOrigin;
    Godot.Vector3 ViewportSize;

    Godot.Vector3 mouse;
    Vector3 offset;

    public override void _Ready()
    {
        depth = camera.Position.Z - this.Position.Z;
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        ViewportOrigin = camera.ProjectPosition(new Vector2(0, 0), depth);
        ViewportSize = camera.ProjectPosition(screenSize, depth);
    }

    public override void _PhysicsProcess(double delta)
    {
        Position = MousePosition();
        //RangeLimit();
    }

    public Vector3 MousePosition()
    {
        Vector2 mouse = GetViewport().GetMousePosition();
        Vector3 worldPos = camera.ProjectPosition(mouse, depth / 2);
        GD.Print(worldPos);
        return new Vector3(worldPos.X, worldPos.Y - 70, depth);
        
    }
    
    /*
    * Limits movement range based on Range values
    */
    public void RangeLimit()
    {
        float minX = Mathf.Min(ViewportOrigin.X, ViewportSize.X);
        float maxX = Mathf.Max(ViewportOrigin.X, ViewportSize.X);
        float minY = Mathf.Min(ViewportOrigin.Y, ViewportSize.Y) - 70f;
        float maxY = Mathf.Max(ViewportOrigin.Y, ViewportSize.Y) - 70f;

        Position = new Godot.Vector3(
            Mathf.Clamp(Position.X, minX, maxX),
            Mathf.Clamp(Position.Y, minY, maxY),
            Position.Z
        );
        //GD.Print("ViewportOrigin: ", ViewportOrigin, " | ViewportSize: ", ViewportSize, "|", depth);
    }
}
