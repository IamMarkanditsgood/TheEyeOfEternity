using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JSG.WordsTable
{
    public class AnimObjectControl : MonoBehaviour
    {
        [HideInInspector]
        public GameObject m_CurrentObj;

        public static AnimObjectControl m_Main;

        void Awake()
        {
            m_Main = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateObject(GameObject obj)
        {
            if (m_CurrentObj != null)
            {
                Destroy(m_CurrentObj);
            }

            m_CurrentObj = Instantiate(obj);
            m_CurrentObj.transform.SetParent(transform);
            m_CurrentObj.transform.localPosition = Vector3.zero;

        }
    }
}