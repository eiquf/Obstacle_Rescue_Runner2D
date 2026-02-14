using UnityEngine.SceneManagement;

public class Reload : IButtonAction
{
    public void Execute() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}