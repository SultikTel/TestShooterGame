using UnityEngine;

namespace Shooter.PlayerControl
{
    [CreateAssetMenu(fileName = "PlayerCameraConfig", menuName = "Configs/PlayerCameraConfig")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [Header("Sensitivity")]
        [SerializeField] private float _sensitivityX;
        public float SensitivityX => _sensitivityX;
        [SerializeField] private float _sensitivityY;
        public float SensitivityY => _sensitivityY;

        [Header("Limits")]
        [SerializeField] private float _minY;
        public float YMin => _minY;
        [SerializeField] private float _maxY;
        public float YMax => _maxY;

        [Header("Smooth")]
        [SerializeField] private float _smoothTime;
        public float SmoothTime => _smoothTime;

        [Header("Zoom")]
        [SerializeField] private float _minZoom;
        public float MinZoom => _minZoom;
        [SerializeField] private float _maxZoom;
        public float MaxZoom => _maxZoom;

        [Header("PlayerPreferences")]
        [SerializeField] private float _currentZoom;
        public float CurrentZoom => _currentZoom;
        [SerializeField] private float _currentSensitivityX;
        public float CurrentSensitivityX => _currentSensitivityX;
        [SerializeField] private float _currentSensitivityY;
        public float CurrentSensitivityY => _currentSensitivityY;
    }
}