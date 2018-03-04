using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour  where T : MonoBehaviour
{
  private static T current;

  private static object lockObj = new object();

  public static T Current
  {
    get
    {
      lock ( lockObj )
      {
        if ( current == null )
        {
          // Attempt to find an existing instance in the scene and make it current
          Object[] gos = FindObjectsOfType( typeof( T ) );
          if ( gos.Length > 1 )
          {
            Debug.LogError( string.Format( "Error: detected multiple instances of type {0}, which is a singleton.", typeof( T ).ToString() ) );
            return current;
          }
          else if ( gos.Length == 1 )
          {
            current = ( T ) gos[ 0 ];
            return current;
          }
          else
          {
            // No instance exists. Let's be nice, and create one.
            GameObject singleton = new GameObject();
            current = singleton.AddComponent<T>();
            singleton.name = "(singleton) " + typeof( T ).ToString();
            // We may want to make this care about don't destroy on load but for now I don't think we want that.
            //DontDestroyOnLoad( singleton );
            Debug.LogWarning( string.Format( "No singleton for type {0} exists, creating one for you.", typeof( T ).ToString() ) );
            return current;
          }
        }

        return current;
      }
    }
  }

}
