using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace CreateDeviceIdentity
{
    class Program
    {

        static RegistryManager registryManager;
        static string connectionString = "HostName=RPI3hubkijciwkrzyz.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Ayfc0fqkkNEiI/GfymP+vRGSZs59hfOf9fB8JmKEgLw=";

        private static async Task AddDeviceAsync()
        {
            string deviceId = "TheChosenOne";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }
    }
}
