using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
        public void LoadPlayScene()
        {
            SceneManager.LoadScene("Scenes/MainGame");
        }
        public void LoadConsumablesScene()
        {
            SceneManager.LoadScene("Scenes/Consumables");
        }
        public void LoadSkinsScene()
        {
            SceneManager.LoadScene("Scenes/SkinsMarket");
        }
}
