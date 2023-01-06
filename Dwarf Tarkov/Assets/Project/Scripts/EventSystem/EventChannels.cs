using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public static class EventChannels
    {
        public static PlayerInputEvents PlayerInputEvents = new PlayerInputEvents();
        public static WorldGenerationEvents WorldGenerationEvents = new WorldGenerationEvents();
    }
}

