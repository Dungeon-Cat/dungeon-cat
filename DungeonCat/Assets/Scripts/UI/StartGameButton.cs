using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Scripts
{
    public class StartGameButton : MonoBehaviour
    {
        public int rootScene;
        public int startRoom;
        
        public void StartGame()
        {
            SceneManager.LoadScene(startRoom);
            SceneManager.LoadScene(rootScene, LoadSceneMode.Additive);
        }
    }
}
