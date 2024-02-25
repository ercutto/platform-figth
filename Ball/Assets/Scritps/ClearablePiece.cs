using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace hyperCasual
{
    public class ClearablePiece : MonoBehaviour
    {
        public AnimationClip clearAnimation;
        private bool isBeenCleared = false;
        public bool IsBeenCleared
        {
            get { return isBeenCleared; }
        }
        protected GamePiece piece;
        private void Awake()
        {
           piece= GetComponent<GamePiece>();
            
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Clear()
        {
            isBeenCleared = true;
            StartCoroutine(nameof(ClearCororutine));
        }
        private IEnumerator ClearCororutine()
        {
            Animator animator= GetComponent<Animator>();
            if (animator)
            {
                animator.Play(clearAnimation.name);
                yield return new WaitForSeconds(clearAnimation.length);
                Destroy(gameObject);
            }
        }
    }
}

