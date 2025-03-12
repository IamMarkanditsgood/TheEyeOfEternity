using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSG.WordPalace.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Contents", menuName = "CustomObjects/Contents", order = 1)]
    public class Contents : ScriptableObject
    {
        public LevelPack[] m_LevelPacks;



        public string[] m_CategoryTitles;
        public Sprite[] m_TableSizeIcons;



        public AnimationCurve m_CamLerp;

    }
}
