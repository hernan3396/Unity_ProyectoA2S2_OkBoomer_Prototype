using UnityEngine;

namespace Utils
{
    public class Geometry : MonoBehaviour
    {
        /// <Summary>
        /// <para>Tira un rayo hacia la posicion indicada</para>
        /// <para>y devuelve true si le pega, en el caso de</para>
        /// <para>que no devuelve false</para>
        /// </Summary>
        public bool CastRay(Vector3 thisPos, Vector3 otherPos, float range, LayerMask layer)
        {
            if (Physics.Raycast(thisPos, Direction(thisPos, otherPos), out RaycastHit hit, range, layer))
                if (hit.transform.CompareTag("Player"))
                    return true;

            return false;
        }

        /// <Summary>
        /// <para>Devuelve la direccion entre 2 vectores</para>
        /// </Summary>
        public Vector3 Direction(Vector3 thisPos, Vector3 otherPos)
        {
            return otherPos - thisPos;
        }
    }

    public class Movement : MonoBehaviour
    {
        /// <Summary>
        /// <para>Devuelve una velocidad dependiendo si</para>
        /// <para>es mayor o menor al maximo</para>
        /// </Summary>
        public Vector3 LimitFallSpeed(Vector3 currentSpeed, float maxFallSpeed)
        {
            if (currentSpeed.y < maxFallSpeed)
                return new Vector3(currentSpeed.x, maxFallSpeed, currentSpeed.z);

            return currentSpeed;
        }
    }
}