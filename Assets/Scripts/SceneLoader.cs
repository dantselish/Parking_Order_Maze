using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const int menuSceneId = 1;
    private const int gameSceneId = 2;


    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneId);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneId);
    }
}
