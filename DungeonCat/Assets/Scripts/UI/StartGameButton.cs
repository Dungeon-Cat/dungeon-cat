using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    /// <summary>
    /// UI Button that saves game state when pressed
    /// </summary>
    public class StartGameButton : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(GameState.RootScene);
            SceneManager.LoadScene(GameState.DefaultRoom, LoadSceneMode.Additive);
        }
    }
}
