using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColorSwitch
{
    public class LevelManager : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}