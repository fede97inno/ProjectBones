using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
        enum StateEnum { WAIT, PLAY, SHOOT, LAST}
    abstract class State
    {
        //protected StateMachine stateMachine;     //owner state machine of the state
        //public virtual void OnEnter() { }
        //public abstract void Update();  //every state class MUST HAVE an Update method
        //public virtual void OnExit() { }
        //public void SetStateMachine(StateMachine fsm) { this.stateMachine = fsm; }
    }
}
