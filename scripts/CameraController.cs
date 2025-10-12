using Godot;
using System;

public partial class CameraController : Camera3D
{
    [Export] Node3D target;
    [Export] Node3D focusPoint;
}
