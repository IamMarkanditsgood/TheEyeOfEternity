using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSG.WordPalace.ScriptableObjects;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JSG.WordPalace.UI
{

    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField, Space]
        private DataStorage m_DataStorage;
        [SerializeField, Space]

        private UIGraphicContents m_UIGraphicContents;
        [SerializeField, Space]
        private UITextContents m_UITextContentsContents;
        void Start()
        {

            GetComponent<AlphaAnim>().StartFadeIn(true);
        }

        // Update is called once per frame
        void Update()
        {
        }






        public bool ShowCoinShop()
        {
            GameObject obj = UISystem.ShowUI("CoinShopUI");
            obj.GetComponentInChildren<UITabControl>().SelectTab(0);
            UISystem.RemoveUI("MainMenuUI");
            return true;
        }
        public bool ShowStore()
        {
            // BtnStore(0);
            return true;
        }

        public void BtnExit()
        {
            Application.Quit();
        }



        public bool ExitRate_No()
        {
            Application.Quit();
            return true;
        }

        public void BtnStore()
        {
            GameObject obj = UISystem.ShowUI("CoinShopUI");
            obj.GetComponentInChildren<UITabControl>().SelectTab(2);
            UISystem.RemoveUI("MainMenuUI");
        }
        public void BtnStartGame()
        {
            UISystem.RemoveUI("MainMenuUI");
            UISystem.ShowUI("LevelsUI");
        }

    }
}
