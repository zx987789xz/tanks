using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        static MainMenu instance;
        private GameObject m_ui;
        private Button m_joinGameButtom;

        void Awake()
        {
            if(instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = this;

            m_ui = transform.FindAnyChild<Transform>("UI").gameObject;
            m_joinGameButtom = transform.FindAnyChild<Button>("JoinGameButton");

            m_ui.SetActive(true);
            m_joinGameButtom.interactable = false;
        }
        public override void OnEnable()
        {
            // Always call the base to add callbacks
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnConnectedToMaster()
        {
            m_joinGameButtom.interactable = true;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            m_ui.SetActive(!PhotonNetwork.InRoom);
        }
    }
}
