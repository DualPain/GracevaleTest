using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Data;
using GracevaleTest.Logic.Player;
using GracevaleTest.Logic.UI;

namespace GracevaleTest.Logic.Game
{
    public class GameController
    {
        private Data.Data _data;
        private MainWindowController _mainWindowController;
        private MarkersWindowController _markersWindowController;
        private PlayerView _leftPlayerView, _rightPlayerView;

        private PlayerController _leftPlayerController, _rightPlayerController;

        public GameController(Data.Data data, MainWindowController mainWindowController, MarkersWindowController markersWindowController, PlayerView leftPlayerView, PlayerView rightPlayerView)
        {
            _data = data;
            _mainWindowController = mainWindowController;
            _markersWindowController = markersWindowController;

            _mainWindowController.RestartGame += _mainWindowController_RestartGame;

            _leftPlayerView = leftPlayerView;
            _rightPlayerView = rightPlayerView;
        }

        public void StartGame(bool useBuffs)
        {
            var leftPlayer = CreatePlayer();
            var rightPlayer = CreatePlayer();            

            if (useBuffs)
            {
                GenerateRandomBuffs(leftPlayer);
                GenerateRandomBuffs(rightPlayer);
            }

            leftPlayer.Target = rightPlayer;
            rightPlayer.Target = leftPlayer;

            var mainWindowModel = new MainWindowModel() { LeftPlayer = leftPlayer, RightPlayer = rightPlayer };
            _mainWindowController.LoadModel(mainWindowModel);

            leftPlayer.ConnectView(_leftPlayerView);
            rightPlayer.ConnectView(_rightPlayerView);

            var markersWindowModel = new MarkersWindowModel() { Players = new[] { leftPlayer, rightPlayer } };
            _markersWindowController.LoadModel(markersWindowModel);

            _leftPlayerController = leftPlayer;
            _rightPlayerController = rightPlayer;
        }

        private PlayerController CreatePlayer()
        {
            var playerState = new PlayerState();

            foreach (var stat in _data.stats)
            {
                playerState.Stats.Add(stat.id, stat.value);                
            }

            var hp = playerState.Stats[StatsId.LIFE_ID];
            playerState.Stats[StatsId.MAX_HEALTH] = hp;

            var playerController = new PlayerController();
            playerController.LoadModel(playerState);            

            return playerController;
        }

        private void GenerateRandomBuffs(PlayerController player)
        {
            var count = Random.Range(_data.settings.buffCountMin, _data.settings.buffCountMax);
            var buffs = GenerateBuffs(_data.buffs, count, _data.settings.allowDuplicateBuffs);

            foreach (var buff in buffs)
            {
                BuffApplicator.AddBuff(player.Model, buff);
            }
        }

        private IReadOnlyCollection<Buff> GenerateBuffs(Buff[] buffs, int count, bool allowDuplicate)
        {
            var resultBuffs = new List<Buff>();

            var availableBuffs = new List<Buff>(buffs);

            for (var index = 0; index < count && availableBuffs.Count > 0; index++)
            {
                var randomBuffIndex = Random.Range(0, availableBuffs.Count);

                resultBuffs.Add(availableBuffs[randomBuffIndex]);

                if (!allowDuplicate)
                {
                    availableBuffs.RemoveAt(randomBuffIndex);
                }
            }

            return resultBuffs;
        }

        private void _mainWindowController_RestartGame(object sender, bool useBuffs)
        {
            _mainWindowController.UnloadModel();
            _markersWindowController.UnloadModel();

            _leftPlayerController.Reset();
            _rightPlayerController.Reset();

            _leftPlayerView.Reset();
            _rightPlayerView.Reset();

            StartGame(useBuffs);
        }
    }
}