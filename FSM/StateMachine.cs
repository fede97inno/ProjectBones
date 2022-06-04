using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class StateMachine
    {
        //private Dictionary<StateEnum, State> states;    //when i create a StateMachine it has states that i save in a disctionary where keys are the StateEnum and the valuers are the States themselves
        //private State currentState;                     //the current active state of the machine
        //public Player Owner { get; protected set; }
        //public StateMachine(Player owner)
        //{
        //    states = new Dictionary<StateEnum, State>();
        //    currentState = null;                        //for now the dictionary is empty and so the currentState will be null
        //    Owner = owner;
        //}

        //public void AddState(StateEnum key, State value)
        //{
        //    states[key] = value;
        //    value.SetStateMachine(this);                //I add the new state in the dictionary and i say to the State that this is its StateMachine
        //}

        //public void GoTo(StateEnum key)
        //{
        //    if (currentState != null)                   //if currentState it s not null i use the OnExit Method on the actual state before change into the new one
        //    {
        //        currentState.OnExit();                  
        //    }

        //    currentState = states[key];                 //i set the new state
        //    currentState.OnEnter();                     //i set the beginning phase of the current state
        //}

        //public void Update()                            //stateMachine ref to use the currentState Update
        //{
        //    if (currentState != null)
        //    {
        //        currentState.Update();

        //    }        
        //}
    }
}
