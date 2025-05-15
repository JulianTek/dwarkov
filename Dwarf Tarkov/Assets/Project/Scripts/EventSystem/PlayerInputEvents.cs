using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public class PlayerInputEvents
    {
        public delegate void MoveEvent(Vector2 pos);
        public delegate void ButtonEvent();
        public delegate void BoolEvent(bool boolean);
        public delegate void ItemEvent(Item item);

        public MoveEvent OnPlayerAim;
        public ButtonEvent OnPlayerShootStarted;
        public ButtonEvent OnPlayerShootFinished;
        public ButtonEvent OnPlayerReload;
        public ButtonEvent OnPlayerSelectPrimaryWeapon;
        public ButtonEvent OnPlayerSelectSecondaryWeapon;

        public ButtonEvent OnPlayerMine;
        public MoveEvent OnPlayerMove;

        public BoolEvent OnPlayerSprint;

        public ItemEvent OnItemAddedToInventory;
        public ItemEvent OnItemRemovedFromInventory;

        public ButtonEvent OnInventoryOpened;
        public ButtonEvent OnInventoryClosed;
        public BoolEvent OnToggleStackSplit;

        public ButtonEvent OnInteract;

        public ButtonEvent OnEnableHUDControls;
        public ButtonEvent OnDisableHUDControls;

        public ButtonEvent OnToggleAmmoTypes;

        public ButtonEvent OnPlayerPauses;

        public BoolEvent OnSetMovement;
    }
}

