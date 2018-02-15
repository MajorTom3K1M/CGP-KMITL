﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PuzzleBobble
{
    class Singleton
    {
        //Global variable initializing

        public const int BOBBLE_SIZE = 50;

        public const int GAMESCREEN_WIDTH = BOBBLE_SIZE * 8;
        public const int GAMESCREEN_HEIGHT = BOBBLE_SIZE * 12;

        public const int MAINSCREEN_WIDTH = GAMESCREEN_WIDTH * 2;
        public const int MAINSCREEN_HEIGHT = GAMESCREEN_HEIGHT;

        //For Scene Changing
        public enum GameScene
        {
            TitleScene,     //Title
            MenuScene,      //Menu
            OptionScene,    //Option like difficulty
            GameScene,      //Main Game Scene
            HistoryScene   //Show Player Statistic on each play
        }

        public GameScene currentGameScene;

        //For Game Scene State
        public enum GameSceneState
        {
            None,           //Not on GameScene
            Tutorial,       //Show Tutorial Box (Skip or no; can be adjusted in Option menu)
            Start,          //First State on Game after Tutorial Box
            Playing,        //Playing State
            End             //Game Over with showing Player Score and otherwise
        }

        public GameSceneState currentGameState;

        //For Player Status on Game Scene State
        public enum PlayerStatus
        {
            None,           //Not on End GameSceneState
            Won,            //Showing if Player Won
            Lost            //Showing if Player Lost
        }

        public PlayerStatus currentPlayerStatus;

        private static Singleton instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}