using System;
using UnityEngine;

namespace Utils
{
    public class Utils
    {

        public static String GetPlayerDir(Vector2 movement)
        {
            if (movement.x > 0 && movement.y == 0) { return "E"; }
            if (movement.x < 0 && movement.y == 0) { return "W"; }
            if (movement.x == 0 && movement.y > 0) { return "N"; }
            if (movement.x == 0 && movement.y < 0) { return "S"; }
            if (movement.x > 0 && movement.y > 0) { return "NE"; }
            if (movement.x > 0 && movement.y < 0) { return "SE"; }
            if (movement.x < 0 && movement.y > 0) { return "NW"; }
            if (movement.x < 0 && movement.y < 0) { return "SW"; }
            else { return null; }
        }
    }
}
