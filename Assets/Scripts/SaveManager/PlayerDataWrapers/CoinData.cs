using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu(fileName = "CoinData", menuName = "Configs/new CoinData")]
    public class CoinData : ScriptableObject
    {
        [SerializeField] private CoinDataWrapper _coinDataWrapper;
        public CoinDataWrapper CoinDataWrapper => _coinDataWrapper;
    }

    [System.Serializable]
    public class CoinDataWrapper
    {
        [SerializeField] private int _coinCount;
        public int CoinCount => _coinCount;

        

    }
}