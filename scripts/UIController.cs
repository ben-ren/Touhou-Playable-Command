using Godot;
using System;

public partial class UIController : CanvasLayer
{
    [Export] public Camera3D Camera;
    [Export] public Node3D FocusPoint;

    private TextureRect _crosshair;

    public override void _Ready()
    {
        _crosshair = GetNode<TextureRect>("Crosshair/BoxContainer/TextureRect");
    }

    public override void _Process(double delta)
    {
        if (Camera == null || FocusPoint == null || _crosshair == null)
            return;

        // Project the FocusPoint 3D position to screen space
        Vector2 screenPos = Camera.UnprojectPosition(FocusPoint.GlobalPosition);

        // Center the crosshair
        _crosshair.Position = screenPos - _crosshair.Size / 2;
    }
}
