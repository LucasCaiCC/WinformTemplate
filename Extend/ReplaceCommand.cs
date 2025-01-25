using NPOI.SS.UserModel;
using SKY_SoilTestDataProcessingSystem.Tools.Calc;

namespace SKY_SoilTestDataProcessingSystem.Common.Command;

/// <summary>
/// @Command:
///     ReplaceCommand 替换表格中目标符号为自定义符号
/// </summary>
public class ReplaceCommand : ICommand<ReplaceCommand>
{
    private string? _targetSymbol;
    private string? _replaceSymbol;
    private ISheet? _sheet;

    /// <summary>
    /// ReplaceCommandContext : ReplaceCommand 上下文
    /// </summary>
    public class ReplaceCommandContext
    {
        public string? TargetSymbol { get; set; }
        public string? ReplaceSymbol { get; set; }
        public ISheet Sheet { get; set; }

        /// <summary>
        /// 替换表格中目标符号为自定义符号
        /// </summary>
        /// <param name="targetSymbol">目标符号,默认为""</param>
        /// <param name="replaceSymbol">替换符号，默认为""</param>
        /// <param name="sheet">表格</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ReplaceCommandContext(string? targetSymbol, string? replaceSymbol, ISheet sheet)
        {
            TargetSymbol = targetSymbol ?? "";
            ReplaceSymbol = replaceSymbol ?? "";
            Sheet = sheet ?? throw new System.ArgumentNullException(nameof(sheet));
        }
    }


    public ReplaceCommand Execute(object? input)
    {
        var context = input as ReplaceCommandContext ?? throw new System.Exception("命令数据无法转换为 ReplaceCommandContext");

        _targetSymbol = context.TargetSymbol;
        _replaceSymbol = context.ReplaceSymbol;
        _sheet = context.Sheet;

        // 如果目标字符串为空或者为 null，或者没有这个表格，则直接返回
        if (string.IsNullOrEmpty(_targetSymbol) || _sheet == null)
        {
            return this;
        }

        ReplaceSymbolToSheet();
        return this;
    }

    /// <summary>
    /// 获取计算结果表格
    /// </summary>
    /// <returns></returns>
    public ISheet? GetSheet()
    {
        return _sheet;
    }

    /// <summary>
    /// 表格中替换目标符号
    /// </summary>
    private void ReplaceSymbolToSheet()
    {
        for (var i = 0; i < _sheet!.LastRowNum; i++)
        {
            var row = _sheet.GetRow(i);
            if (row == null)
            {
                continue;
            }

            for(var colIndex = 0; colIndex < row.LastCellNum; colIndex++)
            {
                var cell = row.GetCell(colIndex);
                if (cell is not { CellType: CellType.String })
                {
                    continue;
                }

                // 识别具体内容
                var cellValue = cell.StringCellValue;
                if (cellValue.Contains(_targetSymbol!))
                {
                    var finalStr = Formula.RemoveSymbol(cellValue, _targetSymbol!, _replaceSymbol!);
                    cell.SetCellValue(finalStr);
                }
            }
        }
    }
}