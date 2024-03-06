using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class LoadGameButton : MonoBehaviour
    {
        private void Awake()
        {
            if (!SaveLoadManager.HasSaveToLoad(out _))
            {
                GetComponent<Button>().interactable = false;
            }
        }

        public void LoadGame()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(GameState.RootScene);
        }
        
        // This is an unintuitive way to do this, but it avoids ever having multiple InputSystems, Main Cameras, Audio Listeners, etc at the same time
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SaveLoadManager.Load();   
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}