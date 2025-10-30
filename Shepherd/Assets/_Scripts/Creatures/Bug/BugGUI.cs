using UnityEngine;

namespace Creatures
{
    public class BugGUI : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        
        public void PlayAnim(string state) => anim.SetTrigger(state);
    }
}
