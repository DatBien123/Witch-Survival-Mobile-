
public interface IAbility
{

    #region [Game Actions]
    void Activate(Character causer, Character target, Effects effects);
    void Cancel();
    void Deactivate();
    #endregion

    #region [Actions]
    void Unlock(Character target = null);
    void Upgrade(Character target = null);
    #endregion

}
