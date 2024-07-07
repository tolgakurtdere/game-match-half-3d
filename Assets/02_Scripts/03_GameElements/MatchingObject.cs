using System;
using DG.Tweening;
using TK.Manager;
using TK.Settings;
using TK.Utility;
using UnityEngine;
using UnityEngine.Rendering;

namespace MatchHalf3D
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [DisallowMultipleComponent]
    public class MatchingObject : MonoBehaviour
    {
        public static event Action<MatchingObject> OnAnyClicked;
        private const float MAX_DRAG_VELOCITY_MAGNITUDE = 40;

        private Rigidbody _rigidbody;
        private Collider _collider;
        private Outline _outline;
        private Renderer _renderer;
        private Vector3 _offset;
        private Vector3? _targetPosition;
        public bool IsHolding { get; private set; }
        public bool IsPut { get; private set; }
        public bool IsMatched { get; private set; }
        public int ID { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _outline = GetComponent<Outline>();
            _renderer = GetComponent<Renderer>();

            ID = transform.parent.GetInstanceID();
            _outline.enabled = false;
            _outline.OutlineWidth = 10f;
            _outline.OutlineColor = Color.green;
        }

        private void OnMouseDown()
        {
            if (!GameManager.IsPlaying) return;
            if (!Camera.main) return;
            //if (IsPut) return;
            if (!Helpers.GetPlanePos(GameSettings.PlaneTarget, out var planePos)) return;
            if (!Helpers.GetPlanePos(GameSettings.PlaneZero, out var planePosZero)) return;

            _outline.enabled = true;
            _rigidbody.isKinematic = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            _rigidbody.velocity = Vector3.zero;

            var pos = transform.position;

            if (!IsPut)
            {
                var offsetZero = pos - planePosZero;
                offsetZero.y = 0;
                pos = planePos + offsetZero;
            }

            transform.position = pos;
            _offset = pos - planePos;

            IsHolding = true;
            IsPut = true;
            _renderer.shadowCastingMode = ShadowCastingMode.Off;

            OnAnyClicked?.Invoke(this);
        }

        private void OnMouseDrag()
        {
            if (!GameManager.IsPlaying) return;
            if (!Camera.main) return;
            if (IsMatched) return;
            if (!Helpers.GetPlanePos(GameSettings.PlaneTarget, out var planePos)) return;

            _targetPosition = planePos + _offset;
        }

        private void OnMouseUp()
        {
            if (!GameManager.IsPlaying) return;

            IsHolding = false;
            _targetPosition = null;
            _rigidbody.isKinematic = true;
        }

        private void FixedUpdate()
        {
            // simulate position change by rigidbody in fixed update

            if (_targetPosition == null) return;

            var direction = _targetPosition.Value - transform.position;
            var velocity = direction / Time.fixedDeltaTime;
            if (velocity.magnitude > MAX_DRAG_VELOCITY_MAGNITUDE)
            {
                velocity = velocity.normalized * MAX_DRAG_VELOCITY_MAGNITUDE;
            }

            _rigidbody.velocity = velocity;
        }

        private void Update()
        {
            if (IsPut && !IsMatched)
            {
                transform.Rotate(0, Time.deltaTime * 60, 0);
            }
        }

        public void Release()
        {
            IsPut = false;

            _renderer.shadowCastingMode = ShadowCastingMode.On;
            _outline.enabled = false;
            _rigidbody.isKinematic = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
        }

        public void Match(Action<Vector3> onComplete = null)
        {
            IsMatched = true;

            _rigidbody.isKinematic = true;
            _collider.enabled = false;

            var seq = DOTween.Sequence();
            seq.Join(transform.DOMove(new Vector3(0, 7, 0), 0.2f))
                .Join(transform.DORotate(new Vector3(-90, 180, 0), 0.2f))
                .Append(transform.DOScale(transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo))
                .OnComplete(() =>
                {
                    onComplete?.Invoke(transform.position);
                    gameObject.SetActive(false);
                });
        }
    }
}