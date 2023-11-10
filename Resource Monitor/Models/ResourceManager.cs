using ResourceMonitor.ViewModels;
using ResourceMonitor.ViewModels.Resources;
using ResourceMonitor.ViewModels.Resources.CPU;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
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

        public CPUViewModel CPU { get; } = CPUViewModel.GetInstance();

        public GPUViewModel GPU { get; } = GPUViewModel.GetInstance();

        public RAMViewModel RAM { get; } = RAMViewModel.GetInstance();

        public DiskViewModel Disk { get; } = DiskViewModel.GetInstance();

        public NetworkViewModel Network { get; } = NetworkViewModel.GetInstance();
        #endregion

        #region Public Methods
        public async void Start()
        {
            resources.Add(CPU);
            resources.Add(GPU);
            resources.Add(RAM);
            resources.Add(Disk);
            resources.Add(Network);

            await Task.Run(() => 
            {
                while (true)
                {
                    HardwareManager.Update();

                    foreach (IResource resource in resources)
                    {
                        resource.Update();
                    }

                    Thread.Sleep(Math.Max(General.GeneralRefreshFrequency, 100));
                }
            }); 
        }
        #endregion
    }
}
