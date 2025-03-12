using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JSG.WordPalace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "CustomObjects/Level", order = 1)]
    public class Level : ScriptableObject
    {
        public Sprite m_Icon;
        [Space]

        public int m_TableSize = 3;
        public string[] m_Words;
        public string m_Letters;

        public m_LevelTheme m_Theme;

        [Space]
        [Header("Data")]
        public int m_StarCount = 0;
        public int m_LevelCoinAmount = 0;

    }

    public enum m_LevelTheme
    {
        Color,
        Animal,
        Food,
        Objects,
        Fruit,
        City,
        Celebrity,
        Flower,
        Country
    }
}
