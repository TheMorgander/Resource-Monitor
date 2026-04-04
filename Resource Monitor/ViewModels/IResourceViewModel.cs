namespace ResourceMonitor.ViewModels
{
    public interface IResourceViewModel
    {
        #region Public Methods
        void Refresh();

        void Apply();
        #endregion
    }
}