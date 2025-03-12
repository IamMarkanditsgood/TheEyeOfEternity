using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JSG.WordPalace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelPack", menuName = "CustomObjects/LevelPack", order = 1)]
    public class LevelPack : ScriptableObject
    {
        public string m_Title;
        public Level[] m_Levels;
        public int m_LastLevel;
        public bool m_Unlocked;
    }
}
