
public interface IAbility
{

    #region [Game Actions]
    void Activate(Character target = null);
    void StartCooldown();
    void Cancel();
    void Deactivate();
    #endregion

    #region [Actions]
    void Unlock(Character target = null);
    void Upgrade(Character target = null);
    #endregion

}
