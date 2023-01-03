using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public class PlayerInputEvents
    {
        public delegate void MoveEvent(Vector2 pos);
        public delegate void ButtonEvent();
        public delegate void SprintEvent(bool isSprinting);

        public ButtonEvent OnPlayerShoot;
        public ButtonEvent OnPlayerMine;
        public MoveEvent OnPlayerMove;

        public SprintEvent OnPlayerSprint;
    }
}

