using UnityEngine;
using UnityEngine.AI;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Invector.CharacterController
{
    public class EnemyAI : MonoBehaviour
    {
        #region variables

        protected EnemyController ec;                // access the  component       
        protected NavMeshAgent agent;
        protected Transform tr_Player;
        #endregion

        protected virtual void Start()
        {
            tr_Player = GameObject.FindGameObjectWithTag("Player").transform;
            CharacterInit();
        }

        protected virtual void CharacterInit()
        {
            ec = GetComponent<EnemyController>();
            agent = GetComponent<NavMeshAgent>();
            if (ec != null)
                ec.Init();

        }

        protected virtual void LateUpdate()
        {
            if (ec == null) return;             // returns if didn't find the controller		

            ec.input.y = agent.speed;
            //this.transform.LookAt(tr_Player);
            agent.SetDestination(tr_Player.position);
            

        }

        protected virtual void FixedUpdate()
        {
         
        }

        protected virtual void Update()
        {
            ec.UpdateMotor();                   // call ThirdPersonMotor methods               
            ec.UpdateAnimator();                // call ThirdPersonAnimator methods		               
        }








    }
}