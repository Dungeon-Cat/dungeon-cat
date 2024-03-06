using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class StartGameButton : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(GameState.RootScene);
            SceneManager.LoadScene(GameState.DefaultRoom, LoadSceneMode.Additive);
        }
    }
}
