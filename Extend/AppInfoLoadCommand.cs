using SKY_SoilTestDataProcessingSystem.Common.Model;
using SKY_SoilTestDataProcessingSystem.Common.ViewModel;

namespace SKY_SoilTestDataProcessingSystem.Common.Command;

/// <summary>
/// AppInfoLoadCommand App信息加载模块
/// </summary>
public class AppInfoLoadCommand : ICommand<AppInfoLoadCommand>
{
    private AppInfoModel? _mode;

    public AppInfoLoadCommand Execute(object? input)
    {
        _mode ??= new AppInfoModel();
        _mode.Version = Application.ProductVersion;

        return this;
    }

    /// <summary>
    /// 获取数据模型
    /// </summary>
    /// <returns></returns>
    public AppInfoModel GetModel()
    {
        _mode ??= new AppInfoModel();
        return _mode;
    }

    /// <summary>
    /// 获取ViewModel
    /// </summary>
    /// <returns></returns>
    public AppInfoViewModel GetViewMode()
    {
        var viewModel = new AppInfoViewModel
        {
            Version = _mode?.Version
        };
        return viewModel;
    }
}