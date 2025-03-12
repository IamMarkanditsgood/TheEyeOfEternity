using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSG.WordPalace.ScriptableObjects;
namespace JSG.WordPalace
{
    public class DataStorageLoader : MonoBehaviour
    {
        [SerializeField]
        private DataStorage m_DataStorage;


        // Start is called before the first frame update
        void Start()
        {
            m_DataStorage.LoadData();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}