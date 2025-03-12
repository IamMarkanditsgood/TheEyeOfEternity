using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JSG.WordPalace.ScriptableObjects
{

    [CreateAssetMenu(fileName = "GameplayData", menuName = "CustomObjects/GameplayData", order = 1)]
    public class GameplayData : ScriptableObject
    {

        public bool m_GameEnded = false;
        public int m_LevelPackNumber;
        public int m_LevelNumber;

        [Space]
        public bool m_CheckReward = false;
        public bool m_DoubleCoinReceive = false;

    }


}