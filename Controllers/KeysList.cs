using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    enum KeyName { UP, DOWN, RIGHT, LEFT, FIRE, JUMP,LAST }
 
    struct KeysList
    {
        private KeyCode[] keycodes;

        public KeysList(List<KeyCode> keys)
        {
            keycodes = new KeyCode[(int)KeyName.LAST];

            for (int i = 0; i < keys.Count; i++)        //uso la lista in modo da poter evitare di inserire tutti i tasti
            {
                keycodes[i] = keys[i];
            }
        }

        public void SetKey(KeyName name, KeyCode key)
        {
            keycodes[(int)name] = key;
        }

        public KeyCode GetKey(KeyName name)
        {
            return keycodes[(int)name];
        }
    }
}
