using NetworkMonitor.ViewModels;
using NetworkMonitor.ViewModels.Resources;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkMonitor.Models
{
    public class ResourceManager
    {
        #region Instance
        private static ResourceManager instance = null;
        public static ResourceManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ResourceManager();
            }

            return instance;
        }
        #endregion

        #region Fields
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        
        private SettingsManager SettingsManager = SettingsManager.GetInstance();

        private List<IResource> resources = new List<IResource>();
        #endregion

        #region Properties
        public GeneralViewModel General { get; } = GeneralViewModel.GetInstance();

        public NetworkViewModel Network { get; } = NetworkViewModel.GetInstance();
        #endregion

        #region Public Methods
        public async void Start()
        {
            try
            {
                resources.Clear();
                resources.Add(Network);

                await Task.Run(() =>
                {
                    while (true)
                    {
                        foreach (IResource resource in resources)
                        {
                            resource.Update();
                        }

                        Task.Run(() =>
                        { 
                            HardwareManager.Update(); 
                        });

                        Thread.Sleep(Math.Max(General.GeneralRefreshFrequency, 100));
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Start();
            }
        }
        #endregion
    }
}
