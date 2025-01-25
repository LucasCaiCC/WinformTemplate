using SKY_SoilTestDataProcessingSystem.Common.Model;
using System.Reflection;
using System.Management;
using SKY_SoilTestDataProcessingSystem.Common.ViewModel;

namespace SKY_SoilTestDataProcessingSystem.Common.Command;

/// <summary>
/// SysInfoLoadCommand 系统信息加载模块
/// </summary>
public class SysInfoLoadCommand : ICommand<SysInfoLoadCommand>
{
    private SysInfoModel? _model;

    public SysInfoLoadCommand Execute(object? input)
    {
        _model ??= new SysInfoModel();

        _model.ComputerName = Environment.MachineName;
        _model.SystemName = GetOSName();
        _model.OSVersion = GetOSVersion();
        _model.UUID = GetSystemUUID();
        _model.ApplicationID = GetApplicationID();

        return this;
    }

    /// <summary>
    /// 获取Model
    /// </summary>
    /// <returns></returns>
    public SysInfoModel GetModel()
    {
        _model ??= new SysInfoModel();
        return _model;
    }

    /// <summary>
    /// 获取ViewModel
    /// </summary>
    /// <returns></returns>
    public SysInfoViewModel GetViewMode()
    {
        var viewModel = new SysInfoViewModel
        {
            ComputerName = _model?.ComputerName,
            SystemName = _model?.SystemName,
            OSVersion = _model?.OSVersion,
            UUID = _model?.UUID,
            ApplicationID = _model?.ApplicationID
        };
        return viewModel;
    }

    // 方法：获取系统名称
    private static string GetOSName()
    {
        try
        {
            // 使用 WMI 查询系统名称
            var osName = "";
            var searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (var os in searcher.Get())
            {
                osName = os["Caption"].ToString();
                break;
            }
            return osName ?? string.Empty;
        }
        catch (Exception ex)
        {
            return "无法获取系统名称: " + ex.Message;
        }
    }

    // 方法：获取系统版本
    private static string GetOSVersion()
    {
        try
        {
            // 从 Environment.OSVersion 获取版本信息
            var version = Environment.OSVersion.Version;
            return version.ToString();
        }
        catch (Exception ex)
        {
            return "无法获取系统版本: " + ex.Message;
        }
    }

    // 方法：获取系统UUID
    private static string GetSystemUUID()
    {
        try
        {
            var uuid = "";
            var mc = new ManagementClass("Win32_ComputerSystemProduct");
            var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                uuid = mo.Properties["UUID"].Value.ToString();
                break;
            }
            return uuid ?? string.Empty;
        }
        catch (Exception ex)
        {
            return "无法获取系统UUID: " + ex.Message;
        }
    }

    // 方法：获取应用ID（这里使用程序集的GUID）
    private static string GetApplicationID()
    {
        try
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), true);
            if (attributes.Length > 0)
            {
                return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
            }
            else
            {
                return "没有找到程序集的GUID属性";
            }
        }
        catch (Exception ex)
        {
            return "无法获取应用ID: " + ex.Message;
        }
    }
}