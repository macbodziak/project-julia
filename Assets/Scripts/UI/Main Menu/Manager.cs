using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loadingText;




        private void Start()
        {
            Debug.Assert(loadingText);
        }



        public void NewGame()
        {
            loadingText.enabled = true;
            GameManagement.GameManager.NewGame();
        }



        public void ExitGame()
        {
            GameManagement.GameManager.ExitGame();
        }
    }
}