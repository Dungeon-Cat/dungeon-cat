using System.Collections;
using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    /// <summary>
    /// UI Button that loads game state when pressed
    /// </summary>
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
            StartCoroutine(nameof(LoadGameAsync));
        }

        private IEnumerator LoadGameAsync()
        {
            yield return SceneManager.LoadSceneAsync(GameState.RootScene, LoadSceneMode.Additive);

            yield return null;

            SaveLoadManager.Load();

            yield return SceneManager.UnloadSceneAsync("MainMenu");
        }
    }
}