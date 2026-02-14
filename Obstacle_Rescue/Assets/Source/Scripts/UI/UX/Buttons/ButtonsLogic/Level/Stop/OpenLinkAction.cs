using UnityEngine;

public class OpenLinkAction : IButtonAction
{
    private string _url;
    public OpenLinkAction(string url) => _url = url;
    public void Execute() => Application.OpenURL(_url);
}