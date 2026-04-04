using ResourceMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceMonitor.Models
{
    public class MonitoringService
    {
        #region Instance
        private static MonitoringService instance = null;

        public static MonitoringService GetInstance()
        {
            if (instance == null)
            {
                instance = new MonitoringService();
            }

            return instance;
        }
        #endregion

        #region Fields
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();

        private readonly List<IResourceViewModel> resources = new List<IResourceViewModel>();

        private CancellationTokenSource cancellationTokenSource = null;

        private Task updateTask = null;
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
        public void Start()
        {
            if (updateTask != null && updateTask.IsCompleted == false)
            {
                return;
            }

            resources.Clear();
            resources.Add(CPU);
            resources.Add(GPU);
            resources.Add(RAM);
            resources.Add(Disk);
            resources.Add(Network);

            cancellationTokenSource = new CancellationTokenSource();
            updateTask = Task.Run(() => UpdateLoop(cancellationTokenSource.Token));
        }

        public void Stop()
        {
            if (cancellationTokenSource == null)
            {
                return;
            }

            cancellationTokenSource.Cancel();

            if (updateTask != null)
            {
                try
                {
                    updateTask.Wait(1000);
                }
                catch (AggregateException ex)
                {
                    foreach (Exception innerException in ex.InnerExceptions)
                    {
                        if (innerException is OperationCanceledException == false)
                        {
                            throw;
                        }
                    }
                }
            }

            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
            updateTask = null;
        }
        #endregion

        #region Private Methods
        private async Task UpdateLoop(CancellationToken cancellationToken)
        {
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    try
                    {
                        hardwareScanner.Update();

                        foreach (IResourceViewModel resource in resources)
                        {
                            resource.Refresh();
                        }

                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            foreach (IResourceViewModel resource in resources)
                            {
                                resource.Apply();
                            }
                        });
                    }
                    catch (Exception)
                    {
                    }

                    await Task.Delay(Math.Max(General.GeneralRefreshFrequency, 100), cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
        #endregion
    }
}