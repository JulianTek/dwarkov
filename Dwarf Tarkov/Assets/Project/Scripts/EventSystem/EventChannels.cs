using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public static class EventChannels
    {
        public static PlayerInputEvents PlayerInputEvents = new PlayerInputEvents();
        public static WorldGenerationEvents WorldGenerationEvents = new WorldGenerationEvents();
        public static WeaponEvents WeaponEvents = new WeaponEvents();
        public static ItemEvents ItemEvents = new ItemEvents();
        public static ExtractionEvents ExtractionEvents = new ExtractionEvents();
        public static EnemyEvents EnemyEvents = new EnemyEvents();
        public static UIEvents UIEvents = new UIEvents();
        public static OutpostEvents OutpostEvents = new OutpostEvents();
        public static NPCEvents NPCEvents = new NPCEvents();
        public static BarteringEvents BarteringEvents = new BarteringEvents();
        public static PlayerEvents PlayerEvents = new PlayerEvents();
        public static GameplayEvents GameplayEvents = new GameplayEvents();

        public delegate void IntEvent(int index);

        public static IntEvent OnLoadScene;
    }
}

