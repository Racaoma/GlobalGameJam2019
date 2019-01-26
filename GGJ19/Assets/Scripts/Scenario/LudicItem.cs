using UnityEngine;

public abstract class LudicItem : MonoBehaviour {

    void OnEnable(){
        LudicController.UpdateLudicMeter += UpdateLudicMeter;
    }

    void OnDisable(){
        LudicController.UpdateLudicMeter -= UpdateLudicMeter;
    }
    void Start(){
        SetupItem();
    }

    protected abstract void UpdateLudicMeter(float meter);
    protected abstract void SetupItem();
}
