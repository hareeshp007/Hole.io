using Holeio.essentials;
using Holeio.UI;
using System;
using UnityEngine;

namespace Holeio
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        public GameObject UImanager;
        [SerializeField]
        private UIManager uiManager;

        public void UpdateScore(int score)
        {
            if(uiManager!= null)
            {
                uiManager.UpdateScore(score);
            }
            
        }
        public void SetUIManager(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
    }
}

