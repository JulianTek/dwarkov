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
        public delegate void ItemEvent(Item item);

        public MoveEvent OnPlayerAim;
        public ButtonEvent OnPlayerShootStarted;
        public ButtonEvent OnPlayerShootFinished;
        public ButtonEvent OnPlayerReload;

        public ButtonEvent OnPlayerMine;
        public MoveEvent OnPlayerMove;

        public SprintEvent OnPlayerSprint;

        public ItemEvent OnItemAddedToInventory;
        public ItemEvent OnItemRemovedFromInventory;

        public ButtonEvent OnInventoryOpened;
        public ButtonEvent OnInventoryClosed;

        public ButtonEvent OnInteract;

        public ButtonEvent OnEnableHUDControls;
        public ButtonEvent OnDisableHUDControls;
    }
}

