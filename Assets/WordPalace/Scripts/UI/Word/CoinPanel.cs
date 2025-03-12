using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSG.WordPalace.ScriptableObjects;

namespace JSG.WordPalace.UI
{
    public class CoinPanel : MonoBehaviour
    {
        [SerializeField]
        private DataStorage m_DataStorage;

        public Text m_CoinCount;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_CoinCount.text = m_DataStorage.Coin.ToString();
        }
        public void ShowCoinShopUI()
        {
            UISystem.ShowUI("CoinShopUI");
        }

    }
}