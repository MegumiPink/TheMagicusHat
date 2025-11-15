using UnityEngine;

public static class GlobalHelper 
{
   public static string GenerateUniqueID(GameObject obj)
   {
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}"; // Unique ID based on scene name and position like ChestID_3_4
    }
}
