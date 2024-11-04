using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AFG
{
    public class BooksCoinController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinCount;
        [SerializeField] private TMP_Text _booksCount;

        private PlayerDataWrapper PlayerData { get; set; }

        void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            PlayerData = GameController.Instance.SaveManager.PlayerData;
            SetPlayerCoins();
            SetPlayerBoks();
        }

        private void SetPlayerBoks()
        {
            _booksCount.text = PlayerData.BooksData.BooksDataWrapper.BooksCount.ToString();
        }

        private void SetPlayerCoins()
        {
            _coinCount.text = PlayerData.CoinData.CoinDataWrapper.CoinCount.ToString();
        }
    }
}

