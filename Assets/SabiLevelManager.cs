using UnityEngine;
using UnityEngine.SceneManagement;

public class SabiLevelManager : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}