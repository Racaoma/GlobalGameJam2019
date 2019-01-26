using UnityEngine;

public class SingletonAwakePersistent<T> : MonoBehaviour where T : Component{
    public static T Instance;

    protected virtual void Awake(){
        if(Instance!=null)
        {
            DestroyImmediate(this);
            return;
        } else {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
    }
}