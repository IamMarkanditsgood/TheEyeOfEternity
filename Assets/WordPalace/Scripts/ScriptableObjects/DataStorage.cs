using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JSG.WordPalace.UI;
using System.Threading.Tasks;

namespace JSG.WordPalace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DataStorage", menuName = "CustomObjects/DataStorage", order = 1)]
    public class DataStorage : ScriptableObject
    {

        [SerializeField, Space]
        private Contents m_Contents;

        public int Coin;



        public int m_WinCount;




        public void SaveData()
        {



            int tempCoin = PlayerPrefs.GetInt("Coin", 0);
            if (Coin < 0)
                Coin = 0;
            if (Coin - tempCoin <= 30000)
            {
                PlayerPrefs.SetInt("Coin", Coin);
            }
            else
            {
                //cheating
                Debug.Log("CHEATING");
                Coin = tempCoin;
                if (Coin < 0)
                    Coin = 0;
                PlayerPrefs.SetInt("Coin", Coin);
            }








            for (int i = 0; i < m_Contents.m_LevelPacks.Length; i++)
            {
                if (m_Contents.m_LevelPacks[i].m_Unlocked)
                    PlayerPrefs.SetInt("m_LevelPackUnlocked" + i.ToString(), 1);
                else
                    PlayerPrefs.SetInt("m_LevelPackUnlocked" + i.ToString(), 0);

                PlayerPrefs.SetInt("m_PackLastLevel" + i.ToString(), m_Contents.m_LevelPacks[i].m_LastLevel);
            }


            PlayerPrefs.Save();
        }

        public void LoadData()
        {



            Coin = PlayerPrefs.GetInt("Coin", 0);



            m_WinCount = PlayerPrefs.GetInt("m_WinCount", 0);



            for (int i = 0; i < m_Contents.m_LevelPacks.Length; i++)
            {
                m_Contents.m_LevelPacks[i].m_Unlocked = (PlayerPrefs.GetInt("m_LevelPackUnlocked" + i.ToString(), 0) == 1);
                m_Contents.m_LevelPacks[i].m_LastLevel = PlayerPrefs.GetInt("m_PackLastLevel" + i.ToString(), 0);
            }
            m_Contents.m_LevelPacks[0].m_Unlocked = true;




        }

        public void ResetSaveData()
        {
            SaveData();
        }



        public void EarnCoin(int coinAmount)
        {
            Coin = +coinAmount;
            SaveData();
        }

        public void SpendCoin(int coinAmount)
        {
            Coin = -coinAmount;
            Coin = Mathf.Max(Coin, 0);
            SaveData();
        }



        public bool CheckInternet()
        {
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                return true;

            return false;
        }

    }
}
