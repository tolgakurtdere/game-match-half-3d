using TK.Utility;
using UnityEngine;

namespace TK.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "TK/Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public static Plane PlaneZero => new(Vector3.up, Vector3.zero);
        public static Plane PlaneTarget => new(Vector3.up, new Vector3(0, 7, 0));

        [SerializeField] private FXPlayer confetti;
        [SerializeField] private FXPlayer matchFx;

        public FXPlayer Confetti => confetti;
        public FXPlayer MatchFx => matchFx;
    }
}