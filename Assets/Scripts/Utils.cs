using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class PlayerUtils
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


		public static Vector2 GetPlayerDirVector(String playerDirCardinal)
		// Return a unit vector in the direction the player is facing
		{
			Vector2 vec = new Vector2(0, 0);

			switch (playerDirCardinal)
			{
				case "E":
					vec.Set(1, 0);
					break;
				case "N":
					vec.Set(0, 1);
					break;
				case "W":
					vec.Set(-1, 0);
					break;
				case "S":
					vec.Set(0, -1);
					break;
				case "NE":
					vec.Set(1, 1);
					break;
				case "NW":
					vec.Set(-1, 1);
					break;
				case "SW":
					vec.Set(-1, -1);
					break;
				case "SE":
					vec.Set(1, -1);
					break;
			}

			vec = vec.normalized; // Normalize so diagonal directions are scaled
			return vec;
		}
	}
}
