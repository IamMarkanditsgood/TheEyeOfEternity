using System.Collections;
using System.Collections.Generic;
using JSG.WordPalace.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace JSG.WordPalace
{
    public class WinUI : MonoBehaviour
    {
        public Text m_CoinAmount;
        private int m_CoinTemp;
        public Text m_WinTxt;
        [TextAreaAttribute]
        public string[] m_WinTexts;
        [SerializeField, Space]
        private DataStorage m_DataStorage;

        public Image m_DoubleCoinBtn;

        [SerializeField, Space]
        private GameplayData m_GameplayData;
        [SerializeField, Space]
        private UIGraphicContents m_UIGraphicContents;
        [SerializeField, Space]
        private UITextContents m_UITextContentsContents;
        // Start is called before the first frame update
        void Start()
        {
            int wintextnum = Random.Range(0, m_WinTexts.Length);
            m_WinTxt.text = m_WinTexts[wintextnum];
            switch (m_GameplayData.m_LevelPackNumber)
            {
                case 0:
                    m_CoinTemp = 10;
                    m_CoinAmount.text = "+10";
                    m_DataStorage.Coin += 10;
                    m_DataStorage.SaveData();
                    break;
                case 1:
                    m_CoinTemp = 20;
                    m_CoinAmount.text = "+20";
                    m_DataStorage.Coin += 20;
                    m_DataStorage.SaveData();
                    break;
                case 2:
                    m_CoinTemp = 30;
                    m_CoinAmount.text = "+30";
                    m_DataStorage.Coin += 30;
                    m_DataStorage.SaveData();
                    break;
                case 3:
                    m_CoinTemp = 40;
                    m_CoinAmount.text = "+40";
                    m_DataStorage.Coin += 40;
                    m_DataStorage.SaveData();
                    break;
                case 4:
                    m_CoinTemp = 40;
                    m_CoinAmount.text = "+40";
                    m_DataStorage.Coin += 40;
                    m_DataStorage.SaveData();
                    break;
            }


        }


        // Update is called once per frame
        void Update()
        {

        }

        public void BtnContinue()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void BtnDoubleCoin()
        {

            HandleGetDoubleFreeCoin();

        }
        public void HandleGetDoubleFreeCoin()
        {
            m_DoubleCoinBtn.gameObject.SetActive(false);
            m_DataStorage.Coin += m_CoinTemp;
            m_DataStorage.SaveData();
            UISystem.ShowCoinReward(m_CoinTemp);
        }
        public void BtnExit()
        {
            SceneManager.LoadScene("MainMenu");

        }




    }
}
