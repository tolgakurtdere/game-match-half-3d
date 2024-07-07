using UnityEngine;

namespace TK.Utility
{
    public static class Helpers
    {
        public static bool GetPlanePos(Plane plane, out Vector3 planePos)
        {
            if (Camera.main)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out var distance))
                {
                    planePos = ray.GetPoint(distance);
                    return true;
                }
            }

            planePos = Vector3.zero;
            return false;
        }
    }
}