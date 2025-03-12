using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSG.WordPalace.ScriptableObjects;
//using BazaarPlugin;
using System;
using UnityEngine.UI;
namespace JSG.WordPalace.UI
{
    public class CoinShopUI : MonoBehaviour
    {
        [SerializeField, Space]
        private Contents m_Contents;
        [SerializeField, Space]
        private DataStorage m_DataStorage;
        [SerializeField, Space]
        private GameplayData m_GameplayData;
        [SerializeField, Space]
        private UIGraphicContents m_UIGraphicContents;
        [SerializeField, Space]
        private UITextContents m_UITextContentsContents;

        private (string, int)[] m_CoinProduct;

        public static CoinShopUI m_Current;
        void Awake()
        {
            m_Current = this;
        }
        void Start()
        {

            m_CoinProduct = new (string, int)[6];
            m_CoinProduct[0] = ("", 40);
            m_CoinProduct[1] = ("", 300);
            m_CoinProduct[2] = ("", 500);
            m_CoinProduct[3] = ("", 1000);
            m_CoinProduct[4] = ("", 1500);
            m_CoinProduct[5] = ("", 3000);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void BtnCoinPack(int num)
        {

            if (Application.platform != RuntimePlatform.Android)
            {
                m_DataStorage.Coin += m_CoinProduct[num].Item2;
                UISystem.ShowCoinReward(m_CoinProduct[num].Item2);
            }

        }

        public void BtnBack()
        {
            UISystem.RemoveUI(gameObject);

        }


    }
}
