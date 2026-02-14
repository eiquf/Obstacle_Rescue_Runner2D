using UnityEngine;

public abstract class MainCamera
{
    protected Camera _camera;
    public MainCamera(Camera camera) { _camera = camera; }
    public abstract void Execute(bool logic);
}