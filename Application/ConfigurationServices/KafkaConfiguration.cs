using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConfigurationServices;

public class KafkaConfiguration
{
    public string BootstrapServer { get; init; }
    public string PermissionsTopic { get; init; }

    public KafkaConfiguration(string bootstrapServer, string permissionsTopic)
    {
        BootstrapServer = bootstrapServer ?? throw new ArgumentNullException(nameof(bootstrapServer));
        PermissionsTopic = permissionsTopic ?? throw new ArgumentNullException(nameof(permissionsTopic));
    }
}