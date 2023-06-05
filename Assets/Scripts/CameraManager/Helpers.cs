using UnityEngine;

namespace Helpers
{
    public class StaticObjects
    {
        private static Camera _mainCamera;
        public static Camera MainCamera => _mainCamera == null ? _mainCamera = Camera.main : _mainCamera;
    }
}