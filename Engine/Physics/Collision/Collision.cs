using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProjectBones
{
    enum CollisionType { NONE, RECTSINTERSECTION, CIRCLESINTERSECTION, RECTCIRCLEINTERSECTION }

    struct Collision 
    {
        public GameObject Collider;     //other in OnCollide
        public Vector2 Delta;           //rimettere a posto gli oggetti quando collidono    //quella in cui sono entrato per prima è quella in cui sono entrato prima
        public CollisionType Type;      //tipo di collisione che si sta verificando
    }
}
